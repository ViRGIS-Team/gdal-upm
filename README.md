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

UPDATED NOTE ABOUT VERSIONS:

This OpenUPM package wraps a Conda Package that invokes GDAL.

Because of the way that Conda works as a package manager, this means that actual library version is actually determined by the Conda version algorithm which is dependent on platform and on what other packages are loaded. The version pin is "x.x" which means that, if the the package version is "3.4.xx" (e.g. 3.4.100) then the actual version loaded is most recent version in the range 3.4.00a0 to 3.5.00a0 - which at the time of writing would be 3.4.3!

Therefore, the OpenUPM package number is done as follows x.y.b where x.y MAJOR & MINOR versions of GDAL to which this package is pinned and b is the build number of the package (starting e.g at 3.5.0 to mean build 0 of the package for GDAL 3.5.x and going up incrementally).

Also note that this evaluation is only done when downloading GDAL. There is currently no function to update GDAL - the only way is to delete the Conda folder (and Conda.meta ) WHEN Unity is closed.

## A note about Upgrading

Unity is a bit "graby" about DLLs and SOs. Once it is loaded it keeps a hardlink to the DLL and does not like changing. This means that for this package, once you have upgraded to a new version of the UPM package you will, usually, need to restart the Unity Editor for the change to work.

## Development and Use in the player

> NOTE For the avoidance of doubt, Conda is NOT required when the application is deployed as a Unity application. All required libraries are automatically included in the distribution package created by Unity.

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

> NOTE 1 - the UWP architecture has currently only been tested as far as a successful build and not as far as deploying an application - since I do not have the time or resources to do that. If you have successfully deployed an app then please raise an issue to say so. Thanks.

> NOTE 2 - The package architecture should in principle be able to support Android and iOS architectures in the future but this has not been implemented. Yet.

> NOTE 3 - the package architecture will never be able to support the WebGL player - since it relies on native dlls. 


## Running in the Editor

This package uses [Conda](https://docs.conda.io/en/latest/) to download the latest version of GDAL.

> NOTE - When installing Miniconda on Windows, you should select the option to add to the Windows Path. This is not the preferred option but IS required for this package to work.

For this package to work, the development machine MUST have a working copy of Conda (either full Conda or Miniconda) installed and in the path. The following CLI command should work without change or embellishment:

```
conda info
```

If the development machine is running Windows, it must also have a reasonably up-to-date version of Powershell installed.

> NOTE - recent versions of Miniconda for Windows by default create a "Conda Shell" for running conda commands and have not included the conda executables in the general Windows path. For this package to work, the command listed above MUST work in the general Command Prompt and thus conda must be in the path.
> If the path entry is not created during the Miniconda install, this usually (if Miniconda was installed for one user only) means that the following must be added to the path environment variable in Control Panel:
> `%USERPROFILE%\miniconda3\condabin`. If Miniconda was installed for All Users, the actual path will be different and this entry should be updated accordingly

The package will keep the installation of GDAL in `Assets\Conda`. You may want to exclude this folder from source control.

## Initializing GDAL

This package installs the GDAL package, which copies data for GDAL and for PROJ into the `Assets/StreamingAssets`folder. You may also want to exclude this folder from source control.

GDAL needs to be told where to find the proj data package,

To do this, the package includes a version of the GdalConfiguration.cs script (see https://github.com/ViRGIS-Team/gdal-upm/blob/main/Runtime/Scripts/GdalConfiguration.cs)

A minimum program would look like:

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OSGeo;


public class test : MonoBehaviour
{
    void Start()
    {
        GdalConfiguration.ConfigureGdal();
    }
}
```

## Documentation

See the [GDAL/OGR C# Api](https://gdal.org/api/csharp/index.html).

# Use with Unity Cloud Build

As of release 3.6.1, this package will work with Unity Cloud Build.

You will need to add a pre-build script to your build configuration in Unity Cloud Build to load Miniconda.

For Windows configurations - the following script works well:

https://gist.github.com/nimaid/a7d6d793f2eba4020135208a57f5c532

For Macos and Linux, something like this:

```
echo starting conda install

curl https://repo.anaconda.com/miniconda/Miniconda3-latest-MacOSX-x86_64.sh -o conda.sh
bash conda.sh -b -p ~/local/miniconda3
echo completed conda install
```
