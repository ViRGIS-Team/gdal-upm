//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (https://www.swig.org).
// Version 4.2.1
//
// Do not make changes to this file unless you know what you are doing - modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace OSGeo.OGR {

using global::System;
using global::System.Runtime.InteropServices;

public class Ogr {

  internal class OgrObject : IDisposable {
	public virtual void Dispose() {

    }
  }
  internal static OgrObject theOgrObject = new OgrObject();
  protected static object ThisOwn_true() { return null; }
  protected static object ThisOwn_false() { return theOgrObject; }

  public static void UseExceptions() {
    OgrPINVOKE.UseExceptions();
  }

  public static void DontUseExceptions() {
    OgrPINVOKE.DontUseExceptions();
  }


  internal static byte[] StringToUtf8Bytes(string str)
  {
    if (str == null)
      return null;

    int bytecount = System.Text.Encoding.UTF8.GetMaxByteCount(str.Length);
    byte[] bytes = new byte[bytecount + 1];
    System.Text.Encoding.UTF8.GetBytes(str, 0, str.Length, bytes, 0);
    return bytes;
  }

  internal unsafe static string Utf8BytesToString(IntPtr pNativeData)
  {
    if (pNativeData == IntPtr.Zero)
        return null;

    byte* pStringUtf8 = (byte*) pNativeData;
    int len = 0;
    while (pStringUtf8[len] != 0) len++;
    return System.Text.Encoding.UTF8.GetString(pStringUtf8, len);
  }

  internal static void StringListDestroy(IntPtr buffer_ptr) {
    OgrPINVOKE.StringListDestroy(buffer_ptr);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
  }

public delegate void GDALErrorHandlerDelegate(int eclass, int code, IntPtr msg);
public delegate int GDALProgressFuncDelegate(double Complete, IntPtr Message, IntPtr Data);
  public static int GetGEOSVersionMajor() {
    int ret = OgrPINVOKE.GetGEOSVersionMajor();
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static int GetGEOSVersionMinor() {
    int ret = OgrPINVOKE.GetGEOSVersionMinor();
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static int GetGEOSVersionMicro() {
    int ret = OgrPINVOKE.GetGEOSVersionMicro();
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static Geometry CreateGeometryFromWkb(uint len, IntPtr bin_string, OSGeo.OSR.SpatialReference reference) {
    IntPtr cPtr = OgrPINVOKE.CreateGeometryFromWkb(len, bin_string, OSGeo.OSR.SpatialReference.getCPtr(reference));
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static Geometry CreateGeometryFromWkt(ref string val, OSGeo.OSR.SpatialReference reference) {
    IntPtr cPtr = OgrPINVOKE.CreateGeometryFromWkt(ref val, OSGeo.OSR.SpatialReference.getCPtr(reference));
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static Geometry CreateGeometryFromGML(string input_string) {
    IntPtr cPtr = OgrPINVOKE.CreateGeometryFromGML(input_string);
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static Geometry CreateGeometryFromJson(string input_string) {
    IntPtr cPtr = OgrPINVOKE.CreateGeometryFromJson(input_string);
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static Geometry CreateGeometryFromEsriJson(string input_string) {
    IntPtr cPtr = OgrPINVOKE.CreateGeometryFromEsriJson(input_string);
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static Geometry BuildPolygonFromEdges(Geometry hLineCollection, int bBestEffort, int bAutoClose, double dfTolerance) {
    IntPtr cPtr = OgrPINVOKE.BuildPolygonFromEdges(Geometry.getCPtr(hLineCollection), bBestEffort, bAutoClose, dfTolerance);
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static Geometry ApproximateArcAngles(double dfCenterX, double dfCenterY, double dfZ, double dfPrimaryRadius, double dfSecondaryAxis, double dfRotation, double dfStartAngle, double dfEndAngle, double dfMaxAngleStepSizeDegrees) {
    IntPtr cPtr = OgrPINVOKE.ApproximateArcAngles(dfCenterX, dfCenterY, dfZ, dfPrimaryRadius, dfSecondaryAxis, dfRotation, dfStartAngle, dfEndAngle, dfMaxAngleStepSizeDegrees);
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static Geometry ForceToPolygon(Geometry geom_in) {
    IntPtr cPtr = OgrPINVOKE.ForceToPolygon(Geometry.getCPtr(geom_in));
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static Geometry ForceToLineString(Geometry geom_in) {
    IntPtr cPtr = OgrPINVOKE.ForceToLineString(Geometry.getCPtr(geom_in));
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static Geometry ForceToMultiPolygon(Geometry geom_in) {
    IntPtr cPtr = OgrPINVOKE.ForceToMultiPolygon(Geometry.getCPtr(geom_in));
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static Geometry ForceToMultiPoint(Geometry geom_in) {
    IntPtr cPtr = OgrPINVOKE.ForceToMultiPoint(Geometry.getCPtr(geom_in));
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static Geometry ForceToMultiLineString(Geometry geom_in) {
    IntPtr cPtr = OgrPINVOKE.ForceToMultiLineString(Geometry.getCPtr(geom_in));
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static Geometry ForceTo(Geometry geom_in, wkbGeometryType eTargetType, string[] options) {
    IntPtr cPtr = OgrPINVOKE.ForceTo(Geometry.getCPtr(geom_in), (int)eTargetType, (options != null)? new OgrPINVOKE.StringListMarshal(options)._ar : null);
    Geometry ret = (cPtr == IntPtr.Zero) ? null : new Geometry(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static FieldDomain CreateRangeFieldDomain(string name, string description, FieldType type, FieldSubType subtype, double min, bool minIsInclusive, double max, double maxIsInclusive) {
    IntPtr cPtr = OgrPINVOKE.CreateRangeFieldDomain(name, description, (int)type, (int)subtype, min, minIsInclusive, max, maxIsInclusive);
    FieldDomain ret = (cPtr == IntPtr.Zero) ? null : new FieldDomain(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static FieldDomain CreateRangeFieldDomainDateTime(string name, string description, string min, bool minIsInclusive, string max, double maxIsInclusive) {
    IntPtr cPtr = OgrPINVOKE.CreateRangeFieldDomainDateTime(name, description, min, minIsInclusive, max, maxIsInclusive);
    FieldDomain ret = (cPtr == IntPtr.Zero) ? null : new FieldDomain(cPtr, false, ThisOwn_false());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static FieldDomain CreateGlobFieldDomain(string name, string description, FieldType type, FieldSubType subtype, string glob) {
    IntPtr cPtr = OgrPINVOKE.CreateGlobFieldDomain(name, description, (int)type, (int)subtype, glob);
    FieldDomain ret = (cPtr == IntPtr.Zero) ? null : new FieldDomain(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static GeomCoordinatePrecision CreateGeomCoordinatePrecision() {
    IntPtr cPtr = OgrPINVOKE.CreateGeomCoordinatePrecision();
    GeomCoordinatePrecision ret = (cPtr == IntPtr.Zero) ? null : new GeomCoordinatePrecision(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static int GetDriverCount() {
    int ret = OgrPINVOKE.GetDriverCount();
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static int GetOpenDSCount() {
    int ret = OgrPINVOKE.GetOpenDSCount();
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static int SetGenerate_DB2_V72_BYTE_ORDER(int bGenerate_DB2_V72_BYTE_ORDER) {
    int ret = OgrPINVOKE.SetGenerate_DB2_V72_BYTE_ORDER(bGenerate_DB2_V72_BYTE_ORDER);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static void RegisterAll() {
    OgrPINVOKE.RegisterAll();
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
  }

  public static string GeometryTypeToName(wkbGeometryType eType) {
    string ret = OgrPINVOKE.GeometryTypeToName((int)eType);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static string GetFieldTypeName(FieldType type) {
    string ret = OgrPINVOKE.GetFieldTypeName((int)type);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static string GetFieldSubTypeName(FieldSubType type) {
    string ret = OgrPINVOKE.GetFieldSubTypeName((int)type);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static wkbGeometryType GT_Flatten(wkbGeometryType eType) {
    wkbGeometryType ret = (wkbGeometryType)OgrPINVOKE.GT_Flatten((int)eType);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static wkbGeometryType GT_SetZ(wkbGeometryType eType) {
    wkbGeometryType ret = (wkbGeometryType)OgrPINVOKE.GT_SetZ((int)eType);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static wkbGeometryType GT_SetM(wkbGeometryType eType) {
    wkbGeometryType ret = (wkbGeometryType)OgrPINVOKE.GT_SetM((int)eType);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static wkbGeometryType GT_SetModifier(wkbGeometryType eType, int bSetZ, int bSetM) {
    wkbGeometryType ret = (wkbGeometryType)OgrPINVOKE.GT_SetModifier((int)eType, bSetZ, bSetM);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static int GT_HasZ(wkbGeometryType eType) {
    int ret = OgrPINVOKE.GT_HasZ((int)eType);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static int GT_HasM(wkbGeometryType eType) {
    int ret = OgrPINVOKE.GT_HasM((int)eType);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static int GT_IsSubClassOf(wkbGeometryType eType, wkbGeometryType eSuperType) {
    int ret = OgrPINVOKE.GT_IsSubClassOf((int)eType, (int)eSuperType);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static int GT_IsCurve(wkbGeometryType arg0) {
    int ret = OgrPINVOKE.GT_IsCurve((int)arg0);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static int GT_IsSurface(wkbGeometryType arg0) {
    int ret = OgrPINVOKE.GT_IsSurface((int)arg0);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static int GT_IsNonLinear(wkbGeometryType arg0) {
    int ret = OgrPINVOKE.GT_IsNonLinear((int)arg0);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static wkbGeometryType GT_GetCollection(wkbGeometryType eType) {
    wkbGeometryType ret = (wkbGeometryType)OgrPINVOKE.GT_GetCollection((int)eType);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static wkbGeometryType GT_GetCurve(wkbGeometryType eType) {
    wkbGeometryType ret = (wkbGeometryType)OgrPINVOKE.GT_GetCurve((int)eType);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static wkbGeometryType GT_GetLinear(wkbGeometryType eType) {
    wkbGeometryType ret = (wkbGeometryType)OgrPINVOKE.GT_GetLinear((int)eType);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static void SetNonLinearGeometriesEnabledFlag(int bFlag) {
    OgrPINVOKE.SetNonLinearGeometriesEnabledFlag(bFlag);
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
  }

  public static int GetNonLinearGeometriesEnabledFlag() {
    int ret = OgrPINVOKE.GetNonLinearGeometriesEnabledFlag();
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static DataSource GetOpenDS(int ds_number) {
    IntPtr cPtr = OgrPINVOKE.GetOpenDS(ds_number);
    DataSource ret = (cPtr == IntPtr.Zero) ? null : new DataSource(cPtr, false, ThisOwn_false());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static DataSource Open(string utf8_path, int update) {
    IntPtr cPtr = OgrPINVOKE.Open(Ogr.StringToUtf8Bytes(utf8_path), update);
    DataSource ret = (cPtr == IntPtr.Zero) ? null : new DataSource(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static DataSource OpenShared(string utf8_path, int update) {
    IntPtr cPtr = OgrPINVOKE.OpenShared(Ogr.StringToUtf8Bytes(utf8_path), update);
    DataSource ret = (cPtr == IntPtr.Zero) ? null : new DataSource(cPtr, true, ThisOwn_true());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static Driver GetDriverByName(string name) {
    IntPtr cPtr = OgrPINVOKE.GetDriverByName(name);
    Driver ret = (cPtr == IntPtr.Zero) ? null : new Driver(cPtr, false, ThisOwn_false());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static Driver GetDriver(int driver_number) {
    IntPtr cPtr = OgrPINVOKE.GetDriver(driver_number);
    Driver ret = (cPtr == IntPtr.Zero) ? null : new Driver(cPtr, false, ThisOwn_false());
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static string[] GeneralCmdLineProcessor(string[] papszArgv, int nOptions) {
        /* %typemap(csout) char**options */
        IntPtr cPtr = OgrPINVOKE.GeneralCmdLineProcessor((papszArgv != null)? new OgrPINVOKE.StringListMarshal(papszArgv)._ar : null, nOptions);
        IntPtr objPtr;
        int count = 0;
        if (cPtr != IntPtr.Zero) {
            while (Marshal.ReadIntPtr(cPtr, count*IntPtr.Size) != IntPtr.Zero)
                ++count;
        }
        string[] ret = new string[count];
        if (count > 0) {
	        for(int cx = 0; cx < count; cx++) {
                objPtr = System.Runtime.InteropServices.Marshal.ReadIntPtr(cPtr, cx * System.Runtime.InteropServices.Marshal.SizeOf(typeof(IntPtr)));
                ret[cx]= (objPtr == IntPtr.Zero) ? null : System.Runtime.InteropServices.Marshal.PtrToStringAnsi(objPtr);
            }
        }
        
    if (OgrPINVOKE.SWIGPendingException.Pending) throw OgrPINVOKE.SWIGPendingException.Retrieve();
        return ret;
}

  public static readonly int wkb25DBit = OgrPINVOKE.wkb25DBit_get();
  public static readonly int ogrZMarker = OgrPINVOKE.ogrZMarker_get();
  public static readonly int OGRNullFID = OgrPINVOKE.OGRNullFID_get();
  public static readonly int OGRUnsetMarker = OgrPINVOKE.OGRUnsetMarker_get();
  public static readonly string OLCRandomRead = OgrPINVOKE.OLCRandomRead_get();
  public static readonly string OLCSequentialWrite = OgrPINVOKE.OLCSequentialWrite_get();
  public static readonly string OLCRandomWrite = OgrPINVOKE.OLCRandomWrite_get();
  public static readonly string OLCFastSpatialFilter = OgrPINVOKE.OLCFastSpatialFilter_get();
  public static readonly string OLCFastFeatureCount = OgrPINVOKE.OLCFastFeatureCount_get();
  public static readonly string OLCFastGetExtent = OgrPINVOKE.OLCFastGetExtent_get();
  public static readonly string OLCFastGetExtent3D = OgrPINVOKE.OLCFastGetExtent3D_get();
  public static readonly string OLCCreateField = OgrPINVOKE.OLCCreateField_get();
  public static readonly string OLCDeleteField = OgrPINVOKE.OLCDeleteField_get();
  public static readonly string OLCReorderFields = OgrPINVOKE.OLCReorderFields_get();
  public static readonly string OLCAlterFieldDefn = OgrPINVOKE.OLCAlterFieldDefn_get();
  public static readonly string OLCAlterGeomFieldDefn = OgrPINVOKE.OLCAlterGeomFieldDefn_get();
  public static readonly string OLCTransactions = OgrPINVOKE.OLCTransactions_get();
  public static readonly string OLCDeleteFeature = OgrPINVOKE.OLCDeleteFeature_get();
  public static readonly string OLCUpsertFeature = OgrPINVOKE.OLCUpsertFeature_get();
  public static readonly string OLCUpdateFeature = OgrPINVOKE.OLCUpdateFeature_get();
  public static readonly string OLCFastSetNextByIndex = OgrPINVOKE.OLCFastSetNextByIndex_get();
  public static readonly string OLCStringsAsUTF8 = OgrPINVOKE.OLCStringsAsUTF8_get();
  public static readonly string OLCCreateGeomField = OgrPINVOKE.OLCCreateGeomField_get();
  public static readonly string OLCCurveGeometries = OgrPINVOKE.OLCCurveGeometries_get();
  public static readonly string OLCMeasuredGeometries = OgrPINVOKE.OLCMeasuredGeometries_get();
  public static readonly string OLCZGeometries = OgrPINVOKE.OLCZGeometries_get();
  public static readonly string OLCRename = OgrPINVOKE.OLCRename_get();
  public static readonly string OLCFastGetArrowStream = OgrPINVOKE.OLCFastGetArrowStream_get();
  public static readonly string OLCFastWriteArrowBatch = OgrPINVOKE.OLCFastWriteArrowBatch_get();
  public static readonly string ODsCCreateLayer = OgrPINVOKE.ODsCCreateLayer_get();
  public static readonly string ODsCDeleteLayer = OgrPINVOKE.ODsCDeleteLayer_get();
  public static readonly string ODsCCreateGeomFieldAfterCreateLayer = OgrPINVOKE.ODsCCreateGeomFieldAfterCreateLayer_get();
  public static readonly string ODsCCurveGeometries = OgrPINVOKE.ODsCCurveGeometries_get();
  public static readonly string ODsCTransactions = OgrPINVOKE.ODsCTransactions_get();
  public static readonly string ODsCEmulatedTransactions = OgrPINVOKE.ODsCEmulatedTransactions_get();
  public static readonly string ODsCMeasuredGeometries = OgrPINVOKE.ODsCMeasuredGeometries_get();
  public static readonly string ODsCZGeometries = OgrPINVOKE.ODsCZGeometries_get();
  public static readonly string ODsCRandomLayerRead = OgrPINVOKE.ODsCRandomLayerRead_get();
  public static readonly string ODsCRandomLayerWrite = OgrPINVOKE.ODsCRandomLayerWrite_get();
  public static readonly string ODrCCreateDataSource = OgrPINVOKE.ODrCCreateDataSource_get();
  public static readonly string ODrCDeleteDataSource = OgrPINVOKE.ODrCDeleteDataSource_get();
  public static readonly string OLMD_FID64 = OgrPINVOKE.OLMD_FID64_get();
  public static readonly int GEOS_PREC_NO_TOPO = OgrPINVOKE.GEOS_PREC_NO_TOPO_get();
  public static readonly int GEOS_PREC_KEEP_COLLAPSED = OgrPINVOKE.GEOS_PREC_KEEP_COLLAPSED_get();
  public static readonly int OGRERR_NONE = OgrPINVOKE.OGRERR_NONE_get();
  public static readonly int OGRERR_NOT_ENOUGH_DATA = OgrPINVOKE.OGRERR_NOT_ENOUGH_DATA_get();
  public static readonly int OGRERR_NOT_ENOUGH_MEMORY = OgrPINVOKE.OGRERR_NOT_ENOUGH_MEMORY_get();
  public static readonly int OGRERR_UNSUPPORTED_GEOMETRY_TYPE = OgrPINVOKE.OGRERR_UNSUPPORTED_GEOMETRY_TYPE_get();
  public static readonly int OGRERR_UNSUPPORTED_OPERATION = OgrPINVOKE.OGRERR_UNSUPPORTED_OPERATION_get();
  public static readonly int OGRERR_CORRUPT_DATA = OgrPINVOKE.OGRERR_CORRUPT_DATA_get();
  public static readonly int OGRERR_FAILURE = OgrPINVOKE.OGRERR_FAILURE_get();
  public static readonly int OGRERR_UNSUPPORTED_SRS = OgrPINVOKE.OGRERR_UNSUPPORTED_SRS_get();
  public static readonly int OGRERR_INVALID_HANDLE = OgrPINVOKE.OGRERR_INVALID_HANDLE_get();
  public static readonly int OGRERR_NON_EXISTING_FEATURE = OgrPINVOKE.OGRERR_NON_EXISTING_FEATURE_get();
}

}
