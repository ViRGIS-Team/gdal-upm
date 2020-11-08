Param( 
    [string]$package,
    [string]$install, 
    [string]$destination,
    [string]$test,
    [string]$shared_assets
    ) 

conda create --name upm -y
conda activate upm
conda install $install -y


$env = conda info --json | ConvertFrom-Json 
$conda_bin = -join($env.active_prefix, "\Library\bin")

Write-Output "Processing *.dll"
$file = -join($conda_bin, '/*.dll' )
Write-Output "Copy $file to $destination"
Copy-Item $file -Destination $destination -Force

Write-Output "Processing $test"
$file = -join($conda_bin, '/', $test )
Copy-Item $file -Destination $destination -Force
 
Write-Output "Processing gdal data"
$file = -join($env.active_prefix, "\Library\share\gdal")
Write-Output "Copy $file to $shared_assets"
Copy-Item -Path $file -Destination $shared_assets -Recurse -Force

Write-Output "Processing proj data"
$file = -join($env.active_prefix, "\Library\share\proj")
Write-Output "Copy $file to $shared_assets"
Copy-Item -Path $file -Destination $shared_assets -Recurse -Force

conda deactivate
conda remove --name upm --all -y