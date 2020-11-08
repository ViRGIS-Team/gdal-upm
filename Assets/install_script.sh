#!/bin/bash

while getopts "p:i:d:t:" opt
do
   case "$opt" in
      p ) package="$OPTARG" ;;
      d ) destination="$OPTARG" ;;
      i ) install="$OPTARG" ;;
      t ) test="$OPTARG" ;;
   esac
done

export PATH="~/opt/anaconda3/bin:$PATH"
echo "Package is $package"
echo "Install is $package"
conda create --name upm -y
conda install -c conda-forge --name upm $install -y --no-deps

env=`conda info --envs |grep upm | grep -o '/.*'`

echo "copy $env/lib/*.dylib to $destination" 
cp "$env"/lib/*.dylib $destination

echo "copy $env/bin/$test $destination"
cp "$env"/bin/$test $destination

conda remove --name upm --all -y