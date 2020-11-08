Param( 
    [string]$package,
    [string]$install,  
    [string]$destination,
    [string]$test,
    [string]$shared_assets
    )

conda create --name upm -y
conda install --name upm $install -y

$env = conda info --envs
$temp = conda info --envs | Select-String  -Pattern 'upm'
$temp -match "C\:.*"
$conda_bin = -join($matches[0], "\Library/bin")

Write-Output "Processing *.dll"
$file = -join($conda_bin, '\*.dll' )
Write-Output "Copy $file to $destination"
Copy-Item $file -Destination $destination -Force

Write-Output "Processing $test"
$file = -join($conda_bin, '\', $test )
Copy-Item $file -Destination $destination -Force
 
Write-Output "Processing gdal data"
$file = -join($matches[0], "\Library\share\gdal")
Write-Output "Copy $file to $shared_assets"
Copy-Item -Path $file -Destination $shared_assets -Recurse -Force

Write-Output "Processing proj data"
$file = -join($matches[0], "\Library\share\proj")
Write-Output "Copy $file to $shared_assets"
Copy-Item -Path $file -Destination $shared_assets -Recurse -Force

conda remove --name upm --all -y