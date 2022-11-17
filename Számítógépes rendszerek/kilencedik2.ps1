Write-Host "Masodfoku egyenlet megoldasa:"
[float]$a = Read-Host "Masodfoku"
[float]$b = Read-Host "Elsofoku: "
[float]$c = Read-Host "Konstans: "

$d = ($b*$b) - 4*$a*$c
if($a -eq 0)
{
    Write-Host "Nem masodfoku!"
}
elseif ($d -lt 0)
{
    Write-Host "Nincs valos megoldasa"
}
elseif($d -eq 0)
{
    Write-Host "Egy gyok:", (-$b/(2*$a))
}
else
{
    $x1 = (-$b+[System.Math]::Sqrt($d))/(2*$a)
    $x2 = (-$b-[System.Math]::Sqrt($d))/(2*$a)
    Write-Host "Egyik gyok:", $x1
    Write-Host "Masik gyok:", $x2
}