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
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.Collections;
using UnityEngine.Jobs;
using Unity.Burst;
using System.Threading.Tasks;
using Unity.Jobs;
using Unity.Mathematics;

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
        /// <param name="dataset" cref="Dataset"></param>
        /// <returns cref="Texture2D"></returns>
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

        /// <summary>
        /// Copies a raster band into a new double array
        /// </summary>
        /// <param name="band" cref="Band"></param>
        /// <param name="buffer"> double[] containing the data</param>
        /// <returns>True if successful</returns>
        public static bool ToArray(this Band band, out double[] buffer)
        {
            try
            {
                // Get the width and height of the Dataset
                int X_size = band.XSize;
                int Y_size = band.YSize;

                buffer = new double[X_size * Y_size];
                band.ReadRaster(0, 0, X_size, Y_size, buffer, X_size, Y_size, 0, 0);

                return true;
            } catch(Exception e) 
            {
                Debug.Log(e.Message);
                buffer = new double[0];
                return false;
            }
        }

        /// <summary>
        /// Attempts to convert a raster band into a planar 3D mesh
        /// </summary>
        /// <param name="band" cref="Band"></param>
        /// <param name="mesh" cref="DMesh3"> returned mesh</param>
        /// <param name="maxTriCount"> indication of the maximum number of triangles that there should be in the return</param>
        /// <returns>True if successfull</returns>
        public static bool ToMesh(this Band band, out DMesh3 mesh)
        {
            // Get the width and height of the Dataset
            double[] gtRaw = new double[6];

            int X_size = band.XSize;
            int Y_size = band.YSize;

            band.GetDataset().GetGeoTransform(gtRaw);
            if (gtRaw is null || gtRaw[1] == 0)
            {
                Debug.Log("Could not get a GeoTransform");
                mesh = default;
                return false;
            }

            if (band.ToArray(out double[] buffer))
            {
                mesh = DMesh3Builder.Build<Vector3d, Index3i, Vector3d>(VerCalc(X_size, Y_size, gtRaw, buffer, band.DataType), TriCalc(X_size, Y_size));
                return true;
            }
            else
            {
                mesh = default;
                return false;
            }

        }

        private static IEnumerable<Index3i>TriCalc(int pixelCount, int rowCount)
        {
            for (int row = 0; row < rowCount - 1; row++)
                for (int pix = 0; pix < pixelCount - 1; pix++)
                {
                    yield return new Index3i(
                        pixelCount*row + pix,
                        pixelCount*(row +1) + pix + 1,
                        pixelCount*(row + 1) + pix
                    );
                    yield return new Index3i(
                        pixelCount * row + pix,
                        pixelCount * row + pix + 1,
                        pixelCount * (row + 1) + pix + 1
                    );
                }
        }

        private static IEnumerable<Vector3d> VerCalc(int pixelCount, int rowCount, double[] gt, double[] buffer,  DataType dt)
        {
            for (int row = 0; row < rowCount; row++)
                for (int pix = 0; pix < pixelCount; pix++)
                {
                    yield return new Vector3d(
                        gt[0] + pix * gt[1] + row * gt[2],
                        gt[3] + pix * gt[4] + row * gt[5],
                        buffer[pix + row * pixelCount]
                        );
                }
        }

        /// <summary>
        /// Converts a GDAL geoTransform into a Transformation Matrix
        /// </summary>
        /// <param name="gt"></param>
        /// <returns></returns>
        public static double3x3 ToTransform(this double[] gt)
        {
            if (gt.Length != 6) throw new Exception("This Array is not a geoTransform");
            return new(
                gt[1], gt[2], gt[0],
                gt[4], gt[5], gt[3],
                0, 0, 1
                );
        }

        /// <summary>
        /// Calculates the UVs for the provided Mesh that would correctly geolocate the raster image on the mesh
        /// </summary>
        /// <param name="dMesh" cref="DMesh3"></param>
        /// <param name="raster" cref="Dataset"></param>
        public static void CalculateMapUVs(this DMesh3 dMesh, Dataset raster)
        {
            
            try {
                dMesh.EnableVertexUVs(Vector2f.Zero);
                double[] gtRaw = new double[6];

                double X_size = raster.RasterXSize;
                double Y_size = raster.RasterYSize;

                raster.GetGeoTransform(gtRaw);
                if (gtRaw is null || gtRaw[1] == 0)
                {
                    throw new Exception("Could not get a GeoTransform");
                }

                IEnumerable<double3> v = dMesh.Vertices().Cast<double3>();

                NativeArray<double2> UV = new (dMesh.VertexCount, Allocator.Persistent);

                NativeArray<double3> vertices = new (v.ToArray(), Allocator.Persistent);

                MapUV uvJob = new()
                {
                    transform = math.inverse(gtRaw.ToTransform()),
                    vertices = vertices
                };

                JobHandle jh = uvJob.Schedule(vertices.Length, 10);
                jh.Complete();

                for (int i = 0; i < UV.Length; i++)
                {
                    dMesh.SetVertexUV(i, (Vector2f)uvJob.UV[i]);
                }

                UV.Dispose();
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
            public NativeArray<double3> vertices;

            [ReadOnly]
            public double3x3 transform;


            public NativeArray<double2> UV;


            public void Execute(int job)
            {
                double3 vertex = vertices[job];

                double3 flatvert = new(vertex.x, vertex.z, 1);

                double3 uv = math.mul(transform, flatvert);

                UV[job] = new double2(uv.x, uv.y);
            }
        }
    }
}