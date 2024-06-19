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

using System;
using VirgisGeometry;

namespace OSGeo.OSR
{
    public static class OsrExtensions
    {
        public static AxisOrder GetAxisOrder(this SpatialReference sr) {
            int axisCount = sr.GetAxesCount();
            if (axisCount < 2 || axisCount > 3) {
                throw new Exception("Invalid Number of Axes in Spatial Reference");
            }
            AxisOrder axis = new AxisOrder();
            for (int i = 0; i < axisCount; i++)
            {
                AxisOrientation ax = sr.GetAxisOrientation("PROJCS", i);
                switch (i)
                {
                    case 0:
                        axis.Axis1 = (AxisType)Enum.ToObject(typeof(AxisType), (int)ax);
                        break;
                    case 1:
                        axis.Axis2 = (AxisType)Enum.ToObject(typeof(AxisType), (int)ax);
                        break;
                    case 2:
                        axis.Axis3 = (AxisType)Enum.ToObject(typeof(AxisType), (int)ax);
                        break;
                }
            }
            return axis;
        }

        public static bool Project(this DMesh3 dMesh, CoordinateTransformation transformer, AxisOrder target)
        {
            try
            {
                dMesh.axisOrder = target;
                for (int i = 0; i < dMesh.VertexCount; i++)
                {
                    if (dMesh.IsVertex(i))
                    {
                        Vector3d vertex = dMesh.GetVertex(i);
                        double[] dV = new double[3] { vertex.x, vertex.y, vertex.z };
                        transformer.TransformPoint(dV);
                        dMesh.SetVertex(i, new Vector3d(dV) { axisOrder = target });
                    }
                };
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static SpatialReference TextToSR(string str)
        {
            if (str.Contains("epsg:") || str.Contains("EPSG:"))
            {
                SpatialReference crs = new SpatialReference(null);
                string[] parts = str.Split(':');
                crs.ImportFromEPSG(int.Parse(parts[1]));
                return crs;
            }
            if (str.Contains("proj"))
            {
                SpatialReference crs = new SpatialReference(null);
                crs.ImportFromProj4(str);
                return crs;
            }
            return new SpatialReference(str);
        }
    }
}