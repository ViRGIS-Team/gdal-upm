//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (https://www.swig.org).
// Version 4.1.0
//
// Do not make changes to this file unless you know what you are doing - modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace OSGeo.OGR {

using global::System;
using global::System.Runtime.InteropServices;

public class Geometry : global::System.IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;
  protected object swigParentRef;

  protected static object ThisOwn_true() { return null; }
  protected object ThisOwn_false() { return this; }

  public Geometry(IntPtr cPtr, bool cMemoryOwn, object parent) {
    swigCMemOwn = cMemoryOwn;
    swigParentRef = parent;
    swigCPtr = new HandleRef(this, cPtr);
  }

  public static HandleRef getCPtr(Geometry obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }
  public static HandleRef getCPtrAndDisown(Geometry obj, object parent) {
    if (obj != null)
    {
      obj.swigCMemOwn = false;
      obj.swigParentRef = parent;
      return obj.swigCPtr;
    }
    else
    {
      return new HandleRef(null, IntPtr.Zero);
    }
  }
  public static HandleRef getCPtrAndSetReference(Geometry obj, object parent) {
    if (obj != null)
    {
      obj.swigParentRef = parent;
      return obj.swigCPtr;
    }
    else
    {
      return new HandleRef(null, IntPtr.Zero);
    }
  }

  ~Geometry() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          OgrPINVOKE.delete_Geometry(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }
public int ExportToWkb( byte[] buffer, wkbByteOrder byte_order ) {
      int retval;
      long size = WkbSize();
      if (size > Int32.MaxValue)
        throw new ArgumentException("Too big geometry (ExportToWkb)");
      if (buffer.Length < size)
        throw new ArgumentException("Buffer size is small (ExportToWkb)");

      IntPtr ptr = Marshal.AllocHGlobal((int)size * Marshal.SizeOf(buffer[0]));
      try {
          retval = ExportToWkb((int)size, ptr, byte_order);
          Marshal.Copy(ptr, buffer, 0, (int)size);
      } finally {
          Marshal.FreeHGlobal(ptr);
      }
      GC.KeepAlive(this);
      return retval;
  }
  public int ExportToWkb( byte[] buffer ) {
      return ExportToWkb( buffer, wkbByteOrder.wkbXDR);
  }

  public static Geometry CreateFromWkb(byte[] wkb){
     if (wkb.Length == 0)
        throw new ArgumentException("Buffer size is small (CreateFromWkb)");
     Geometry retval;
     IntPtr ptr = Marshal.AllocHGlobal(wkb.Length * Marshal.SizeOf(wkb[0]));
     try {
         Marshal.Copy(wkb, 0, ptr, wkb.Length);
         retval =  new Geometry(wkbGeometryType.wkbUnknown, null, wkb.Length, ptr, null);
      } finally {
          Marshal.FreeHGlobal(ptr);
      }
      return retval;
  }

  public static Geometry CreateFromWkt(string wkt){
     return new Geometry(wkbGeometryType.wkbUnknown, wkt, 0, IntPtr.Zero, null);
  }

  public static Geometry CreateFromGML(string gml){
     return new Geometry(wkbGeometryType.wkbUnknown, null, 0, IntPtr.Zero, gml);
  }

  public Geometry(wkbGeometryType type) : this(OgrPINVOKE.new_Geometry((int)type, null, 0, IntPtr.Zero, null), true, null) {
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
  }
  public Geometry(wkbGeometryType type, string wkt, int wkb, IntPtr wkb_buf, string gml) : this(OgrPINVOKE.new_Geometry((int)type, wkt, wkb, wkb_buf, gml), true, null) {
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
  }

  public int ExportToWkt(out string argout) {
    int ret = OgrPINVOKE.Geometry_ExportToWkt(swigCPtr, out argout);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int ExportToIsoWkt(out string argout) {
    int ret = OgrPINVOKE.Geometry_ExportToIsoWkt(swigCPtr, out argout);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public string ExportToGML() {
    string ret = OgrPINVOKE.Geometry_ExportToGML__SWIG_0(swigCPtr);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public string ExportToGML(string[] options) {
    string ret = OgrPINVOKE.Geometry_ExportToGML__SWIG_1(swigCPtr, (options != null)? new OgrPINVOKE.StringListMarshal(options)._ar : null);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public string ExportToKML(string altitude_mode) {
    string ret = OgrPINVOKE.Geometry_ExportToKML(swigCPtr, altitude_mode);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public string ExportToJson(string[] options) {
    string ret = OgrPINVOKE.Geometry_ExportToJson(swigCPtr, (options != null)? new OgrPINVOKE.StringListMarshal(options)._ar : null);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void AddPoint(double x, double y, double z) {
    OgrPINVOKE.Geometry_AddPoint(swigCPtr, x, y, z);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
  }

  public void AddPointM(double x, double y, double m) {
    OgrPINVOKE.Geometry_AddPointM(swigCPtr, x, y, m);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
  }

  public void AddPointZM(double x, double y, double z, double m) {
    OgrPINVOKE.Geometry_AddPointZM(swigCPtr, x, y, z, m);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
  }

  public void AddPoint_2D(double x, double y) {
    OgrPINVOKE.Geometry_AddPoint_2D(swigCPtr, x, y);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
  }

  public int AddGeometryDirectly(Geometry other_disown) {
    int ret = OgrPINVOKE.Geometry_AddGeometryDirectly(swigCPtr, Geometry.getCPtrAndDisown(other_disown, ThisOwn_false()));
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int AddGeometry(Geometry other) {
    int ret = OgrPINVOKE.Geometry_AddGeometry(swigCPtr, Geometry.getCPtr(other));
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int RemoveGeometry(int iSubGeom) {
    int ret = OgrPINVOKE.Geometry_RemoveGeometry(swigCPtr, iSubGeom);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Geometry Clone() {
    IntPtr cPtr = OgrPINVOKE.Geometry_Clone(swigCPtr);
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public wkbGeometryType GetGeometryType() {
    wkbGeometryType ret = (wkbGeometryType)OgrPINVOKE.Geometry_GetGeometryType(swigCPtr);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public string GetGeometryName() {
    string ret = OgrPINVOKE.Geometry_GetGeometryName(swigCPtr);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double Length() {
    double ret = OgrPINVOKE.Geometry_Length(swigCPtr);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double Area() {
    double ret = OgrPINVOKE.Geometry_Area(swigCPtr);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetArea() {
    double ret = OgrPINVOKE.Geometry_GetArea(swigCPtr);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int GetPointCount() {
    int ret = OgrPINVOKE.Geometry_GetPointCount(swigCPtr);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetX(int point) {
    double ret = OgrPINVOKE.Geometry_GetX(swigCPtr, point);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetY(int point) {
    double ret = OgrPINVOKE.Geometry_GetY(swigCPtr, point);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetZ(int point) {
    double ret = OgrPINVOKE.Geometry_GetZ(swigCPtr, point);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetM(int point) {
    double ret = OgrPINVOKE.Geometry_GetM(swigCPtr, point);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void GetPoint(int iPoint, double[] argout) {
    OgrPINVOKE.Geometry_GetPoint(swigCPtr, iPoint, argout);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
  }

  public void GetPointZM(int iPoint, double[] argout) {
    OgrPINVOKE.Geometry_GetPointZM(swigCPtr, iPoint, argout);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
  }

  public void GetPoint_2D(int iPoint, double[] argout) {
    OgrPINVOKE.Geometry_GetPoint_2D(swigCPtr, iPoint, argout);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
  }

  public int GetGeometryCount() {
    int ret = OgrPINVOKE.Geometry_GetGeometryCount(swigCPtr);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetPoint(int point, double x, double y, double z) {
    OgrPINVOKE.Geometry_SetPoint(swigCPtr, point, x, y, z);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetPointM(int point, double x, double y, double m) {
    OgrPINVOKE.Geometry_SetPointM(swigCPtr, point, x, y, m);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetPointZM(int point, double x, double y, double z, double m) {
    OgrPINVOKE.Geometry_SetPointZM(swigCPtr, point, x, y, z, m);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetPoint_2D(int point, double x, double y) {
    OgrPINVOKE.Geometry_SetPoint_2D(swigCPtr, point, x, y);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
  }

  public void SwapXY() {
    OgrPINVOKE.Geometry_SwapXY(swigCPtr);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
  }

  public Geometry GetGeometryRef(int geom) {
    IntPtr cPtr = OgrPINVOKE.Geometry_GetGeometryRef(swigCPtr, geom);
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, false, ThisOwn_false());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Geometry Simplify(double tolerance) {
    IntPtr cPtr = OgrPINVOKE.Geometry_Simplify(swigCPtr, tolerance);
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Geometry SimplifyPreserveTopology(double tolerance) {
    IntPtr cPtr = OgrPINVOKE.Geometry_SimplifyPreserveTopology(swigCPtr, tolerance);
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Geometry DelaunayTriangulation(double dfTolerance, int bOnlyEdges) {
    IntPtr cPtr = OgrPINVOKE.Geometry_DelaunayTriangulation(swigCPtr, dfTolerance, bOnlyEdges);
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Geometry Polygonize() {
    IntPtr cPtr = OgrPINVOKE.Geometry_Polygonize(swigCPtr);
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Geometry Boundary() {
    IntPtr cPtr = OgrPINVOKE.Geometry_Boundary(swigCPtr);
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Geometry GetBoundary() {
    IntPtr cPtr = OgrPINVOKE.Geometry_GetBoundary(swigCPtr);
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Geometry ConvexHull() {
    IntPtr cPtr = OgrPINVOKE.Geometry_ConvexHull(swigCPtr);
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Geometry MakeValid(string[] options) {
    IntPtr cPtr = OgrPINVOKE.Geometry_MakeValid(swigCPtr, (options != null)? new OgrPINVOKE.StringListMarshal(options)._ar : null);
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Geometry Normalize() {
    IntPtr cPtr = OgrPINVOKE.Geometry_Normalize(swigCPtr);
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Geometry RemoveLowerDimensionSubGeoms() {
    IntPtr cPtr = OgrPINVOKE.Geometry_RemoveLowerDimensionSubGeoms(swigCPtr);
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Geometry Buffer(double distance, int quadsecs) {
    IntPtr cPtr = OgrPINVOKE.Geometry_Buffer(swigCPtr, distance, quadsecs);
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Geometry Intersection(Geometry other) {
    IntPtr cPtr = OgrPINVOKE.Geometry_Intersection(swigCPtr, Geometry.getCPtr(other));
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Geometry Union(Geometry other) {
    IntPtr cPtr = OgrPINVOKE.Geometry_Union(swigCPtr, Geometry.getCPtr(other));
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Geometry UnionCascaded() {
    IntPtr cPtr = OgrPINVOKE.Geometry_UnionCascaded(swigCPtr);
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Geometry Difference(Geometry other) {
    IntPtr cPtr = OgrPINVOKE.Geometry_Difference(swigCPtr, Geometry.getCPtr(other));
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Geometry SymDifference(Geometry other) {
    IntPtr cPtr = OgrPINVOKE.Geometry_SymDifference(swigCPtr, Geometry.getCPtr(other));
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Geometry SymmetricDifference(Geometry other) {
    IntPtr cPtr = OgrPINVOKE.Geometry_SymmetricDifference(swigCPtr, Geometry.getCPtr(other));
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double Distance(Geometry other) {
    double ret = OgrPINVOKE.Geometry_Distance(swigCPtr, Geometry.getCPtr(other));
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double Distance3D(Geometry other) {
    double ret = OgrPINVOKE.Geometry_Distance3D(swigCPtr, Geometry.getCPtr(other));
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Empty() {
    OgrPINVOKE.Geometry_Empty(swigCPtr);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
  }

  public bool IsEmpty() {
    bool ret = OgrPINVOKE.Geometry_IsEmpty(swigCPtr);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool IsValid() {
    bool ret = OgrPINVOKE.Geometry_IsValid(swigCPtr);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool IsSimple() {
    bool ret = OgrPINVOKE.Geometry_IsSimple(swigCPtr);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool IsRing() {
    bool ret = OgrPINVOKE.Geometry_IsRing(swigCPtr);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool Intersects(Geometry other) {
    bool ret = OgrPINVOKE.Geometry_Intersects(swigCPtr, Geometry.getCPtr(other));
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool Intersect(Geometry other) {
    bool ret = OgrPINVOKE.Geometry_Intersect(swigCPtr, Geometry.getCPtr(other));
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool Equals(Geometry other) {
    bool ret = OgrPINVOKE.Geometry_Equals(swigCPtr, Geometry.getCPtr(other));
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool Equal(Geometry other) {
    bool ret = OgrPINVOKE.Geometry_Equal(swigCPtr, Geometry.getCPtr(other));
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool Disjoint(Geometry other) {
    bool ret = OgrPINVOKE.Geometry_Disjoint(swigCPtr, Geometry.getCPtr(other));
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool Touches(Geometry other) {
    bool ret = OgrPINVOKE.Geometry_Touches(swigCPtr, Geometry.getCPtr(other));
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool Crosses(Geometry other) {
    bool ret = OgrPINVOKE.Geometry_Crosses(swigCPtr, Geometry.getCPtr(other));
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool Within(Geometry other) {
    bool ret = OgrPINVOKE.Geometry_Within(swigCPtr, Geometry.getCPtr(other));
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool Contains(Geometry other) {
    bool ret = OgrPINVOKE.Geometry_Contains(swigCPtr, Geometry.getCPtr(other));
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool Overlaps(Geometry other) {
    bool ret = OgrPINVOKE.Geometry_Overlaps(swigCPtr, Geometry.getCPtr(other));
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int TransformTo(OSGeo.OSR.SpatialReference reference) {
    int ret = OgrPINVOKE.Geometry_TransformTo(swigCPtr, OSGeo.OSR.SpatialReference.getCPtr(reference));
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int Transform(OSGeo.OSR.CoordinateTransformation trans) {
    int ret = OgrPINVOKE.Geometry_Transform__SWIG_0(swigCPtr, OSGeo.OSR.CoordinateTransformation.getCPtr(trans));
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public OSGeo.OSR.SpatialReference GetSpatialReference() {
    IntPtr cPtr = OgrPINVOKE.Geometry_GetSpatialReference(swigCPtr);
    OSGeo.OSR.SpatialReference ret = (cPtr == IntPtr.Zero) ? null : new OSGeo.OSR.SpatialReference(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void AssignSpatialReference(OSGeo.OSR.SpatialReference reference) {
    OgrPINVOKE.Geometry_AssignSpatialReference(swigCPtr, OSGeo.OSR.SpatialReference.getCPtr(reference));
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
  }

  public void CloseRings() {
    OgrPINVOKE.Geometry_CloseRings(swigCPtr);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
  }

  public void FlattenTo2D() {
    OgrPINVOKE.Geometry_FlattenTo2D(swigCPtr);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
  }

  public void Segmentize(double dfMaxLength) {
    OgrPINVOKE.Geometry_Segmentize(swigCPtr, dfMaxLength);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
  }

  public void GetEnvelope(Envelope env) {
    OgrPINVOKE.Geometry_GetEnvelope(swigCPtr, Envelope.getCPtr(env));
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
  }

  public void GetEnvelope3D(Envelope3D env) {
    OgrPINVOKE.Geometry_GetEnvelope3D(swigCPtr, Envelope3D.getCPtr(env));
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
  }

  public Geometry Centroid() {
    IntPtr cPtr = OgrPINVOKE.Geometry_Centroid(swigCPtr);
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Geometry PointOnSurface() {
    IntPtr cPtr = OgrPINVOKE.Geometry_PointOnSurface(swigCPtr);
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint WkbSize() {
    uint ret = OgrPINVOKE.Geometry_WkbSize(swigCPtr);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int GetCoordinateDimension() {
    int ret = OgrPINVOKE.Geometry_GetCoordinateDimension(swigCPtr);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int CoordinateDimension() {
    int ret = OgrPINVOKE.Geometry_CoordinateDimension(swigCPtr);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int Is3D() {
    int ret = OgrPINVOKE.Geometry_Is3D(swigCPtr);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int IsMeasured() {
    int ret = OgrPINVOKE.Geometry_IsMeasured(swigCPtr);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetCoordinateDimension(int dimension) {
    OgrPINVOKE.Geometry_SetCoordinateDimension(swigCPtr, dimension);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
  }

  public void Set3D(int b3D) {
    OgrPINVOKE.Geometry_Set3D(swigCPtr, b3D);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetMeasured(int bMeasured) {
    OgrPINVOKE.Geometry_SetMeasured(swigCPtr, bMeasured);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
  }

  public int GetDimension() {
    int ret = OgrPINVOKE.Geometry_GetDimension(swigCPtr);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int HasCurveGeometry(int bLookForCircular) {
    int ret = OgrPINVOKE.Geometry_HasCurveGeometry(swigCPtr, bLookForCircular);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Geometry GetLinearGeometry(double dfMaxAngleStepSizeDegrees, string[] options) {
    IntPtr cPtr = OgrPINVOKE.Geometry_GetLinearGeometry(swigCPtr, dfMaxAngleStepSizeDegrees, (options != null)? new OgrPINVOKE.StringListMarshal(options)._ar : null);
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Geometry GetCurveGeometry(string[] options) {
    IntPtr cPtr = OgrPINVOKE.Geometry_GetCurveGeometry(swigCPtr, (options != null)? new OgrPINVOKE.StringListMarshal(options)._ar : null);
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Geometry Value(double dfDistance) {
    IntPtr cPtr = OgrPINVOKE.Geometry_Value(swigCPtr, dfDistance);
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Geometry Transform(GeomTransformer transformer) {
    IntPtr cPtr = OgrPINVOKE.Geometry_Transform__SWIG_1(swigCPtr, GeomTransformer.getCPtr(transformer));
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public PreparedGeometry CreatePreparedGeometry() {
    IntPtr cPtr = OgrPINVOKE.Geometry_CreatePreparedGeometry(swigCPtr);
    PreparedGeometry ret = (cPtr == IntPtr.Zero) ? null : new PreparedGeometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int ExportToWkb(int bufLen, IntPtr buffer, wkbByteOrder byte_order) {
    int ret = OgrPINVOKE.Geometry_ExportToWkb(swigCPtr, bufLen, buffer, (int)byte_order);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
