Param( 
    [string]$package,
    [string]$install,  
    [string]$destination,
    [string]$shared_assets
    )

conda install -c conda-forge --prefix $destination --copy  --mkdir $install -y

# Move the shared data to the shared assets folder to esnure that it gets built into the client

Write-Output "Processing gdal data"
$file = -join($destination, "\Library\share\gdal")
Write-Output "Copy $file to $shared_assets"
Move-Item -Path $file -Destination $shared_assets  -Force

Write-Output "Processing proj data"
$file = -join($destination, "\Library\share\proj")
Write-Output "Copy $file to $shared_assets"
Move-Item -Path $file -Destination $shared_assets  -Force

# Tree shakering

Set-Location $destination

Remove-Item *.dll

Get-ChildItem -exclude .*, conda-meta, *.meta, Library | Remove-Item -Recurse

Set-Location Library

Get-ChildItem -exclude bin | Remove-Item -Recurse
Set-Location bin
Get-ChildItem -exclude *.dll, *.exe, *.json | Remove-Item -Recurse
Remove-Item api-*
Remove-Item vcr* 
Remove-Item msvcp* # removes vs runtime - unity uses mon
Get-ChildItem | Where-Object {$_ -clike "GDAL*"} | Remove-Item # removes c# dlls that won't run in Unity
Remove-Item *_csharp.dll
