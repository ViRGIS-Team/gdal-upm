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
using OSGeo.OSR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OSGeo.OGR {

    public static class OgrExtensions {

        /// <summary>
        /// Creates a g3.DMesh3 from a geometry. Intended for Polygons and Surfaces
        /// </summary>
        /// <param name="geom"></param>
        /// <param name="crs"></param>
        /// <returns></returns>
        public static IEnumerable<DMesh3> ToMeshList(this Geometry geom, SpatialReference crs = null)
        {
            switch (geom.GetGeometryType())
            {
                case wkbGeometryType.wkbPolygon:
                case wkbGeometryType.wkbPolygon25D:
                case wkbGeometryType.wkbPolygonM:
                case wkbGeometryType.wkbPolygonZM:
                    yield return geom.ToPolygonMesh(crs);
                    break;
                case wkbGeometryType.wkbMultiPolygon:
                case wkbGeometryType.wkbMultiPolygon25D:
                case wkbGeometryType.wkbMultiPolygonM:
                case wkbGeometryType.wkbMultiPolygonZM:
                    int n = geom.GetGeometryCount();
                    for (int i = 0; i < n; i++)
                    {
                        Geometry poly = geom.GetGeometryRef(i);
                        yield return poly.ToPolygonMesh(crs);
                    }
                    break;
                case wkbGeometryType.wkbPolyhedralSurface:
                case wkbGeometryType.wkbPolyhedralSurfaceM:
                case wkbGeometryType.wkbPolyhedralSurfaceZ:
                case wkbGeometryType.wkbPolyhedralSurfaceZM:
                case wkbGeometryType.wkbTIN:
                case wkbGeometryType.wkbTINM:
                case wkbGeometryType.wkbTINZ:
                case wkbGeometryType.wkbTINZM:
                    IEnumerable<Vector3d> vertices = null;
                    IEnumerable<Index3i> triangles = null;
                    n = geom.GetGeometryCount();
                    for (int i = 0; i < n; i++)
                    {
                        Geometry poly = geom.GetGeometryRef(i);
                        IEnumerable<DCurve3> rings = poly.ToCurveList(crs);
                        switch (rings.Count())
                        {
                            case 0:
                                throw new Exception("Invalid Polyhedron in Surface");
                            case 1:
                                DCurve3 curve = rings.First();
                                switch (curve.VertexCount)
                                {
                                    case < 3:
                                        throw new Exception("Invalid Polyhedron in Surface");
                                    case 3:
                                        vertices = curve.Vertices;
                                        triangles = new List<Index3i>() { new Index3i(0,1,2) };
                                        break;
                                    case 4:
                                        vertices = curve.Vertices;
                                        triangles = new List<Index3i>()
                                        {
                                            new Index3i(0, 1, 2),
                                            new Index3i(0, 2, 3)
                                        };
                                        break;
                                    case 5:
                                        vertices = curve.Vertices;
                                        triangles = new List<Index3i>()
                                        {
                                            new Index3i(0, 1, 2),
                                            new Index3i(0, 2, 3),
                                            new Index3i(0, 3, 4)
                                        };
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            default:
                                break;
                        };
                        if (vertices is null)
                        {
                            // TODO commplete this
                            throw new NotImplementedException();
                        } else
                        {
                            yield return DMesh3Builder.Build<Vector3d, Index3i, Vector3d>(vertices, triangles);
                        }
                    }
                    break;
                default:
                    throw new Exception("Unknown Geometry Type");
            }
        }



        /// <summary>
        /// Creates a g3.DMesh3 from a geometry. Intended for Polygons and Surfaces
        /// </summary>
        /// <param name="geom"></param>
        /// <param name="crs"></param>
        /// <returns></returns>
        public static DMesh3 ToPolygonMesh(this Geometry geom, SpatialReference crs = null)
        {
            switch (geom.GetGeometryType())
            {
                case wkbGeometryType.wkbPolygon:
                case wkbGeometryType.wkbPolygon25D:
                case wkbGeometryType.wkbPolygonM:
                case wkbGeometryType.wkbPolygonZM:
                    IEnumerable<DCurve3> rings = geom.ToCurveList(crs);
                    if (rings.First().VertexCount < 4) throw new Exception("Polygon is invalid or is a triangle");
                    GeneralPolygon2d polygon2d = new(rings, out _, out IEnumerable<Vector3d> VerticesItr);
                    Index3i[] triangles = polygon2d.GetMesh();
                    return DMesh3Builder.Build<Vector3d, Index3i, Vector3d>(VerticesItr, triangles);
                default:
                    throw new Exception("Incorrect Geometry type");
            }
        }

        /// <summary>
        /// Creates a g3.DCurve from a geometry. Intended for LineStrings and LinearRings (of all dimensions and multitude)
        ///
        /// If the optional Spatialreference is defined, the geometries are transformed into that SR
        ///
        /// </summary>
        /// <param name="crs"> the crs to u for the DCurve3 DEFAULT map default projections or project CRS if none</param>
        /// <returns></returns>
        public static IEnumerable<DCurve3> ToCurveList(this Geometry geom, SpatialReference crs = null) {
            switch (geom.GetGeometryType())
            {
                case wkbGeometryType.wkbLineString:
                case wkbGeometryType.wkbLinearRing:
                case wkbGeometryType.wkbLineString25D:
                case wkbGeometryType.wkbLineStringM:
                case wkbGeometryType.wkbLineStringZM:
                    yield return geom.ToCurve(crs);
                    break;
                case wkbGeometryType.wkbMultiLineString:
                case wkbGeometryType.wkbMultiLineString25D:
                case wkbGeometryType.wkbMultiLineStringM:
                case wkbGeometryType.wkbMultiLineStringZM:
                    int n = geom.GetGeometryCount();
                    for (int i = 0; i < n; i++)
                    {
                        Geometry line = geom.GetGeometryRef(i);
                        yield return line.ToCurve(crs);
                    }
                    break;
                case wkbGeometryType.wkbPolygon:
                case wkbGeometryType.wkbPolygon25D:
                case wkbGeometryType.wkbPolygonM:
                case wkbGeometryType.wkbPolygonZM:
                case wkbGeometryType.wkbTriangle:
                case wkbGeometryType.wkbTriangleM:
                case wkbGeometryType.wkbTriangleZ:
                case wkbGeometryType.wkbTriangleZM:
                    n = geom.GetGeometryCount();
                    for (int i = 0; i < n; i++)
                    {
                        Geometry line = geom.GetGeometryRef(i);
                        yield return line.ToCurve(crs);
                    }
                    break;
                default:
                    throw new Exception("Unknown Geometry Type");
            }
        }

        public static DCurve3 ToCurve(this Geometry geom, SpatialReference crs = null)
        {
            switch (geom.GetGeometryType())
            {
                // Note that counter intuitively - OGR should NOT report a type of LinearRing in the API!
                case wkbGeometryType.wkbLineString:
                case wkbGeometryType.wkbLineString25D:
                case wkbGeometryType.wkbLineStringM:
                case wkbGeometryType.wkbLineStringZM:
                    return new(geom.ToVector3d(crs).ToList<Vector3d>());
                default:
                    throw new Exception("Incorrect Geometry Type"); 
            }
        }

        /// <summary>
        /// Convert Geometry to IEnumerable<Vector3d>
        ///
        /// If the optional <paramref name="crs"/> is defined, the geometries are transformed into that SR
        ///
        /// NOTE - the CRS transformation is applied BEFORE the transformation matrix
        ///
        /// </summary>
        /// <param name="crs">Optional - Spatial Reference into which to transform the points</param>
        /// <returns>IEnumerable<Vector3d></returns>
        public static IEnumerable<Vector3d> ToVector3d(this Geometry geom, SpatialReference crs = null) {
            if (crs != null)
            {
                if (geom.GetSpatialReference() == null)
                {
                    geom.AssignSpatialReference(crs);
                }
                else
                {
                    geom.TransformTo(crs);
                }
            }
            int count = geom.GetPointCount();
            if (count > 0)
                for (int i = 0; i < count; i++) {
                    double[] argout = new double[3];
                    geom.GetPoint(i, argout);
                    Vector3d v = new Vector3d(argout);
                    v.axisOrder = geom.GetSpatialReference().GetAxisOrder(); ;
                    yield return v ;
                }
            else {
                throw new NotSupportedException("No Points in Geometry (which is a pointless geometry ...");
            }
        }

        /// <summary>
        /// Converts Vector3 positions to Points in the Geometry
        ///
        /// If the optional Spatialreference is defined, the geometries are transformed from that SR.
        /// NOTE - in this case the SR of the Geometry should be set beforehand.
        /// 
        /// </summary>
        /// <param name="geom"> Geometry top add the points to</param>
        /// <param name="points"> Array of Vector3d positions</param>
        /// <returns></returns>
        public static Geometry AddPoints(this Geometry geom, IEnumerable<Vector3d> points, SpatialReference crs = null)
        {
            foreach (Vector3d point in points)
            {

                //geom.AddPoint(mapLocal.x, mapLocal.z, mapLocal.y);
            }
            return geom;
        }

        /// <summary>
        /// Checks if this Feature contains field of this name
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool ContainsKey(this Feature feature, string name) {
            int fieldCount = feature.GetFieldCount();
            bool flag = false;
            for (int i = 0; i < fieldCount; i++) {
                FieldDefn fd = feature.GetFieldDefnRef(i);
                if (fd.GetName() == name) {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

        /// <summary>
        /// Gets the Feature as an object of type T. You will to cast the returned object to type T
        ///
        /// It is usually better to use the Generic version
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static object Get(this Feature feature, string name, out Type T)
        {
            int i = feature.GetFieldIndex(name);
            FieldDefn fd = feature.GetFieldDefnRef(i);
            FieldType ft = fd.GetFieldType();
            switch (ft) {
                case FieldType.OFTString:
                    T = typeof(string);
                    return feature.GetFieldAsString(i);
                case FieldType.OFTReal:
                    T = typeof(double);
                    return feature.GetFieldAsDouble(i);
                case FieldType.OFTInteger:
                    T = typeof(int);
                    return feature.GetFieldAsInteger(i);
                case FieldType.OFTIntegerList:
                    T = typeof(List<int>);
                    return feature.GetFieldAsIntegerList(i, out _).ToList<int>();
                case FieldType.OFTRealList:
                    T = typeof(List<double>);
                    return feature.GetFieldAsDoubleList(i, out _).ToList<double>();
                case FieldType.OFTStringList:
                    T = typeof(List<string>);
                    return feature.GetFieldAsStringList(i).ToList<string>();
                case FieldType.OFTWideString:
                    T = typeof(string);
                    return feature.GetFieldAsString(i);
                case FieldType.OFTWideStringList:
                    break;
                case FieldType.OFTBinary:
                    T = typeof(int);
                    return feature.GetFieldAsInteger(i);
                case FieldType.OFTDate:
                    int year, month, day, hour, minute;
                    float seconds;
                    feature.GetFieldAsDateTime(i,
                        out year,
                        out month,
                        out day,
                        out hour,
                        out minute,
                        out seconds,
                        out _
                        );
                    T = typeof(DateTime);
                    int s = (int)Math.Floor(seconds);
                    return new DateTime(
                        year,
                        month,
                        day,
                        hour,
                        minute,
                        s,
                        (int)Math.Floor((seconds - s) * 1000)
                        );
                case FieldType.OFTTime:
                    feature.GetFieldAsDateTime(i,
                        out year,
                        out month,
                        out day,
                        out hour,
                        out minute,
                        out seconds,
                        out _
                        );
                    T = typeof(DateTime);
                    s = (int)Math.Floor(seconds);
                    return new DateTime(
                        year,
                        month,
                        day,
                        hour,
                        minute,
                        s,
                        (int)Math.Floor((seconds - s) * 1000)
                        );
                case FieldType.OFTDateTime:
                    feature.GetFieldAsDateTime(i,
                        out year,
                        out month,
                        out day,
                        out hour,
                        out minute,
                        out seconds,
                        out _
                        );
                    T = typeof(DateTime);
                    s = (int)Math.Floor(seconds);
                    return new DateTime(
                        year,
                        month,
                        day,
                        hour,
                        minute,
                        s,
                        (int)Math.Floor((seconds - s) * 1000)
                        );
                case FieldType.OFTInteger64:
                    T = typeof(long);
                    return feature.GetFieldAsInteger64(i);
                case FieldType.OFTInteger64List:
                    T = typeof(List<int>);
                    return feature.GetFieldAsIntegerList(i, out _).ToList<int>();
                default:
                    T = null;
                    return null;
            }
            T = null;
            return null;
        }

        /// <summary>
        /// Get as Type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="feature"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T Get<T>(this Feature feature, string name)
        {
            object value = feature.Get(name, out Type S);
            if (typeof(T) != S ) throw new Exception("OGR Feature Get : The data Types do not match");
            return (T)value;
        }

        public static Dictionary<string, object> GetAll(this Feature feature) {
            Dictionary<string, object> ret = new Dictionary<string, object>();
            if (feature != null) {
                int fieldCount = feature.GetFieldCount();
                for (int i = 0; i < fieldCount; i++) {
                    FieldDefn fd = feature.GetFieldDefnRef(i);
                    string key = fd.GetName();
                    object value = feature.Get(key, out _);
                    ret.Add(key, value);
                }
            }
            return ret;
        }

        public static void Set(this Feature feature, string name, double value) {
            int i = feature.GetFieldIndex(name);
            if (i > -1) {
                feature.SetField(name, value);
            }
        }

        public static void Set(this Feature feature, string name, string value) {
            int i = feature.GetFieldIndex(name);
            if (i > -1) {
                feature.SetField(name, value);
            }
        }

        public static void Set(this Feature feature, string name, int value) {
            int i = feature.GetFieldIndex(name);
            if (i > -1) {
                feature.SetField(name, value);
            }
        }

        public static void Set(this Feature feature, string name, long value)
        {
            int i = feature.GetFieldIndex(name);
            if (i > -1)
            {
                feature.SetField(name, value);
            }
        }
    }
}