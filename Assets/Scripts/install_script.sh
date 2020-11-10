#!/bin/bash

while getopts "p:i:d:t:s:" opt
do
   case "$opt" in
      p ) package="$OPTARG" ;;
      d ) destination="$OPTARG" ;;
      i ) install="$OPTARG" ;;
      t ) test="$OPTARG" ;;
      s ) shared_assets="$OPTARG" ;;
   esac
done

export PATH="~/opt/anaconda3/bin:$PATH"
echo "Package is $package"
echo "Install is $package"
echo "Shared Assett path $shared_assets"
conda create --name upm -y
conda install -c conda-forge --name upm $install -y

env=`conda info --envs |grep upm | grep -o '/.*'`

echo "copy $env/lib/*.dylib to $destination/lib" 
mkdir -p "$destination/lib" 
cp -avf "$env"/lib/*.dylib "$destination/lib"

echo "copy $env/lib/*.so to $destination/lib" 
mkdir -p "$destination/lib" 
cp -avf "$env"/lib/*.so "$destination/lib"

echo "copy $env/bin/$test $destination"
mkdir -p "$destination/bin" 
cp -avf "$env/bin/$test" "$destination/bin"

echo "Processing gdal data"
echo "copy $env/share/gdal to $shared_assets"
mkdir -p "$shared_assets/gdal" 
cp -av "$env"/share/gdal/* "$shared_assets/gdal"

echo "Processing proj data"
echo "copy $env/share/proj to $shared_assets"
mkdir -p "$shared_assets/proj" 
cp -av "$env"/share/proj/* "$shared_assets/proj"

conda remove --name upm --all -y