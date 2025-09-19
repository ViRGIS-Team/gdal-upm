Param( 
    [string]$package,
    [string]$install,  
    [string]$destination,
    [string]$shared_assets
    )

$logfile = -join($destination, "\gdal_log.txt")

$outp = conda install -c conda-forge --prefix "$destination\Env" --copy $install -y -v *>&1 

Set-Location $destination
$temp = [System.IO.Path]::GetFileName((Get-Location).toString()) 

if ( $temp -ne "Conda")
{
    Write-Output "Working Directory Invalid" (Get-Location).tostring()
    Write-Output $outp
    Exit
}

Write-Output $outp >> "$logfile"

# Move the shared data to the shared assets folder to esnure that it gets built into the client

Write-Output "Processing gdal data"
$file = -join($destination, "\Env\Library\share\gdal")
Write-Output "Copy $file to $shared_assets" >> "$logfile"
Move-Item -Path $file -Destination $shared_assets  -Force

Write-Output "Processing proj data"
$file = -join($destination, "\Env\Library\share\proj")
Write-Output "Copy $file to $shared_assets"  >> "$logfile"
Move-Item -Path $file -Destination $shared_assets  -Force

# Tree shakering


