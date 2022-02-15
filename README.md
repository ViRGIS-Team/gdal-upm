# Unity Package for GDAL

The [Geospatial Data Abstraction Layer](https://gdal.org//) (GDAL) is a translator library for raster and vector geospatial data formats that is released under an X/MIT style Open Source License by the Open Source Geospatial Foundation. It provides a single data model for multiple supported data formats. 

This repo is a Unity Package for using GDAL in a Unity project.

This Package is part of the [ViRGiS project](https://www.virgis.org/) - bringing GiS to VR. 

## Installation

The Package can be installed from [Open UPM](https://openupm.com/packages/com.virgis.gdal/). If you use this method, the dependencies will be automatically loaded provided the relevent scoped registry is included in your project's `manifest.json` :
```
scopedRegistries": [
    {
      "name": "package.openupm.com",
      "url": "https://package.openupm.com",
      "scopes": [
        "com.openupm",
        "com.virgis"
      ]
    }
  ],
```

The Package can also be installed using the Unity Package Manager directly from the [GitHub Repo](https://github.com/ViRGIS-Team/gdal-upm).

## Version numbers

This package is a wrapper around a C++ library. We want to keep the link to the library version. However, we also need to be able to have multiple
builds of the package for the same underlying library version. Unfortunately, UPM does not have the concept of a build number.

Therefore, this package uses the version number ing proposed by [Favo Yang to solve this](https://medium.com/openupm/how-to-maintain-upm-package-part-3-2d08294269ad#88d8). This adds two digits for build number to the SemVer patch value i.e. 3.1.1 => 3.1.100, 3.1.101, 3.1.102 etc.

This has the unfortunate side effect that 3.1.001 will revert to 3.1.1 and this means :

| Package | Library |
| ------- | ------- |
| 3.1.0   | 3.1.0   |
| 3.1.1   | 3.1.0   |
| 3.1.100 | 3.1.1.  |

## A note about Upgrading

Unity is a bit "graby" about DLLs and SOs. Once it is loaded it keeps a hardlink to the DLL and does not like changing. This means that for this package, once you have upgraded to a new version of the UPM package you will, usually, need to restart the Unity Editor for the change to work.

## Development and Use in the player

The scripts for accessing GDAL/OGR functions are included in the `OSGEO`namespace and follow the [GDAL/OGR C# Api](https://gdal.org/api/csharp.html).

The GDAL library is loaded as an unmanaged native plugin. This plugin will load correctly in the player when built. See below for a note about use in the Editor.

All versions of this package works in all supported architectures using the Mono scripting back end. As of package version 3.4.102, the package will succesfully build using the IL2CPP scripting backend. The package does NOT support the .NET scripting backend in the UWP player since that scripting backend does not support the System.Runtime.InteropServices namespace.

| Architecture        | Mono    | IL2CPP  | .NET |
|---------------------|---------|---------|------|
| Windows Standalone  | Yes     | Yes     |  --  |
| MacOS Standalone    | Yes     | Yes     |  --  |
| Linux Standalone    | Yes     | Yes     |  --  |
| UWP  (see note 1)    |  --     | Yes     | No   |
| Android             | Not Yet | Not Yet |  --  |
| iOS                 | Not Yet | Not Yet |  --  |
| WebGL.              | Never   | Never   |  --  |

> NOTE 1 - the UWP architecture has currently only been tested as far as a successful build and not as far as deployng an application - since I do not have the time or resources to do that. If you have successfully deployed an app then please raise an issue to say so. Thanks.

> NOTE 2 - The package architecture should in principle be able to support Android and iOS architectures in the future but this has not been implemented. Yet.

> NOTE 3 - the package architecture will never be able to support the WebGL player - since it relies on native dlls. 


## Running in the Editor

This package uses [Conda](https://docs.conda.io/en/latest/) to download the latest version of GDAL.

For this package to work, the development machine MUST have a working copy of Conda (either full Conda or Miniconda) installed and in the path. The following CLI command should work without change or embellishment:

```
conda info
```

If the development machine is running Windows, it must also have a reasonably up to date version of Powershell installed.

The package will keep the installation of GDAL in `Assets\Conda`. You may want to exclude this folder from source control.

This package installs the GDAL package, which copies data for GDAL and for PROJ into the `Assets/StreamingAssets`folder. You may also want to exclude this folder from source control.

## Documentation

See the [GDAL/OGR C# Api](https://gdal.org/api/csharp/index.html).

