#!/bin/bash

while getopts "p:i:d:s:" opt
do
   case "$opt" in
      p ) package="$OPTARG" ;;
      d ) destination="$OPTARG" ;;
      i ) install="$OPTARG" ;;
      s ) shared_assets="$OPTARG" ;;
   esac
done

conda install -c conda-forge --prefix $destination --copy --mkdir $install -y

echo "Processing gdal data"
echo "copy $destination/share/gdal to $shared_assets"
mkdir -p "$shared_assets/gdal" 
cp "$destination"/share/gdal/* "$shared_assets/gdal"

echo "Processing proj data"
echo "copy $destination/share/proj to $shared_assets"
mkdir -p "$shared_assets/proj" 
cp "$destination"/share/proj/* "$shared_assets/proj"

find "$destination" -type d -not \( -name *bin -or -name *lib -or -name *Conda -or -name *conda-meta \) -maxdepth 1 -print0 | xargs -0 -I {} rm -r {}

find "$destination/lib" -type d -not -name *lib -maxdepth 1 -print0 | xargs -0 -I {} rm -r {}
rm "$destination/lib/terminfo"

rm "$destination"/lib/*_csharp.dll
rm "$destination"/lib/GDAL*