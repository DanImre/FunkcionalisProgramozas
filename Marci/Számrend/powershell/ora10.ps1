#1.
param($a)

if(!$a)
{
    Write-Host "Add meg a file nevet!"
    $a=Read-Host
}

$f = Get-Content $a

"" > ki1.txt
"" > ki2.txt

$c=1
foreach($e in $f)
{
    if($c -eq 1)
    {
        $e >> ki1.txt
        $c=2
    }
    else
    {
        $e >> ki2.txt
        $c=1
    }
}
#2.
Write-Host "Kerem az egyenlet egyutthatoit: "
[double]$x1=Read-Host
[double]$x2=Read-Host
[double]$x3=Read-Host
if($x1 -eq 0) 
{
    Write-Host "Nem masodfoku!"
}
else 
{
    $d = $x2*$x2-4*$x1*$x3
    if($d -lt 0)
    {
        Write-Host "Nincs valos gyok!"
    }
    elseif ($d -eq 0)
    {
        Write-Host "Megoldas:" -$x2/2*$x1
    }
    else
    {
        Write-Host "Megoldasok:" $((-$x2+[System.Math]::Sqrt($d))/(2*$x1)) $((-$x2-[System.Math]::Sqrt($d))/(2*$x1))
    }
}