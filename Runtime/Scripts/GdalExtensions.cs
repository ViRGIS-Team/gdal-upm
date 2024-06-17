/* MIT License

Copyright Runette Software

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice (and subsidiary notices) shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE. */

using VirgisGeometry;
using System;
using System.Linq;
using UnityEngine;
using Unity.Collections;
using UnityEngine.Jobs;
using Unity.Burst;
using System.Threading.Tasks;
using Unity.Jobs;

namespace OSGeo.GDAL
{

    public static class GdalExtensions
    {
        /// <summary>
        /// Turns a Raster Dataset into an RGBA32 Textture2D - only setting the RGB values
        ///
        /// Assumes that :
        /// r is Band 1
        /// g is Band 2
        /// b is Band 3
        ///
        /// If there is less than 3 bands then the first band will be rendered in Black and white
        /// 
        /// </summary>
        /// <param name="dataset"></param>
        /// <returns></returns>
        public static Texture2D ToRGB(this Dataset dataset)
        {
            // Get the GDAL Band objects from the Dataset
            Band redBand = dataset.GetRasterBand(1);
            Band greenBand;
            Band blueBand;

            if (dataset.RasterCount < 3)
            {
                greenBand = redBand;
                blueBand = redBand;
            } else
            {
                greenBand = dataset.GetRasterBand(2);
                blueBand = dataset.GetRasterBand(3);
            } 

            // Get the width and height of the Dataset
            int width = redBand.XSize;
            int height = redBand.YSize;


            Texture2D tex = new(width, height, TextureFormat.RGBA32, false);
            tex.wrapMode = TextureWrapMode.Clamp;

#if GDAL_UNSAFE
            NativeArray<byte> buffer = tex.GetRawTextureData<byte>();
            unsafe
            {
                IntPtr buff = (IntPtr)Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr<Byte>(buffer);

                redBand.ReadRaster(0, 0, width, height, buff, width, height, DataType.GDT_Byte, 4, 4 * width);
                blueBand.ReadRaster(0, 0, width, height, new IntPtr(buff.ToInt64() + 1), width, height, DataType.GDT_Byte, 4, 4 * width);
                greenBand.ReadRaster(0, 0, width, height, new IntPtr(buff.ToInt64() + 2), width, height, DataType.GDT_Byte, 4, 4 * width);
            }
#else
            byte[] buffer = new byte[width * height];
            redBand.ReadRaster(0, 0, width, height, buffer, width, height, 0, 0);
            tex.LoadRawTextureData<byte>(new NativeArray<byte>(buffer,Allocator.Persistent));
#endif
            tex.Apply();
            return tex;
        }

        public static void CalculateMapUVs(this DMesh3 dMesh, Dataset raster)
        {
            
            try {
                dMesh.EnableVertexUVs(Vector2f.Zero);
                double[] gtRaw = new double[6];

                double X_size = raster.RasterXSize;
                double Y_size = raster.RasterYSize;

                raster.GetGeoTransform(gtRaw);
                if (gtRaw == null && gtRaw[1] == 0)
                {
                    throw new Exception("Could not get a GeoTransform");
                }

                NativeArray<double> geoTransform = new NativeArray<double>(gtRaw, Allocator.Persistent);
                NativeArray<double> U = new NativeArray<double>(dMesh.VertexCount, Allocator.Persistent);
                NativeArray<double> V = new NativeArray<double>(dMesh.VertexCount, Allocator.Persistent);

                NativeArray<Vector3d> vertices = new NativeArray<Vector3d>(dMesh.Vertices().ToArray<Vector3d>(), Allocator.Persistent);
                double F = geoTransform[2] / geoTransform[5];

                MapUV uv = new();
                uv.F0 = 1 / ((geoTransform[1] - F * geoTransform[4]) * X_size);
                uv.F1 = F * uv.F0;
                uv.F2 = 1 / (geoTransform[5] * Y_size);
                uv.F3 = geoTransform[4] * uv.F2 * X_size;
                uv.vertices = vertices;
                uv.U = U;
                uv.V = V;
                uv.geoTransform = geoTransform;

                JobHandle jh = uv.Schedule(vertices.Length, 10);
                jh.Complete();

                for (int i = 0; i < U.Length; i++)
                {
                    dMesh.SetVertexUV(i, new Vector2f((float)U[i], (float)V[i]));
                }

                geoTransform.Dispose();
                U.Dispose();
                V.Dispose();
                vertices.Dispose();

            }
            catch
            {
                dMesh.CalculateUVs();
            }
        }

        public static Task<int> CalculateMapUVsAsync(this DMesh3 dMesh, Dataset raster)
        {

            TaskCompletionSource<int> tcs1 = new TaskCompletionSource<int>();
            Task<int> t1 = tcs1.Task;
            t1.ConfigureAwait(false);

            // Start a background task that will complete tcs1.Task
            Task.Factory.StartNew(() => {
                dMesh.CalculateMapUVs(raster);
                tcs1.SetResult(1);
            });
            return t1;
        }

        [BurstCompile]
        struct MapUV : IJobParallelFor
        {
            [ReadOnly]
            public NativeArray<Vector3d> vertices;

            [ReadOnly]
            public NativeArray<double> geoTransform;

            [ReadOnly]
            public double F0;

            [ReadOnly]
            public double F1;

            [ReadOnly]
            public double F2;

            [ReadOnly]
            public double F3;

            public NativeArray<double> U;
            public NativeArray<double> V;

            public void Execute(int job)
            {
                Vector3d vertex = vertices[job];
                double X_geo = vertex.x - geoTransform[0];
                double Y_geo = vertex.y - geoTransform[3];

                double X = F0 * X_geo - F1 * Y_geo;
                double Y = F2 * Y_geo - F3 * X;

                U[job] = X;
                V[job] = 1 - Y;
            }
        }
    }
}