#1. feladat

$szam = 10
$tipp = Read-Host "Tippelj! "
while($szam -ne $tipp)
{
    $tipp = Read-Host "Tippelj mast! "
}
Write-Host "Eltalaltad !"

#2. feladat

[Int] $xHossz = Read-Host "Szelessege: "
[Int] $yHossz = Read-Host "Magassaga: "

for ($i = 0; $i -lt $yHossz; $i++) {
    for ($j = 0; $j -lt $xHossz; $j++) {
        Write-Host "#" -NoNewline
    }
    Write-Host ""
}

$terulet = ($xHossz * $yHossz)
Write-Host "Terulet: " $terulet

$kerulet = 2 *($xHossz + $yHossz)
Write-Host "Kerulet: " $kerulet

#3. feladat

[Int] $a = Read-Host "Egyik szam: "
[Int] $b = Read-Host "Masik szam: "

$relativePrimek = $true

for ([float] $i = $a - 1; $i -gt 1; $i--) {
    if((($a/$i) % 2 -eq 1 -or ($a/$i) % 2 -eq 0) -and (($b/$i) % 2 -eq 1 -or ($b/$i) % 2 -eq 0))
    {
        $relativePrimek = $false
    }
}

if($relativePrimek) {
    Write-Host "Relativ primek!"
}
else {
    Write-Host "Nem relativ primek!"
}

#4. feladat

for ([Int] $i = 1; $i -lt 10; $i++) {
    for ([Int] $j = 1; $j -lt 10; $j++) {
        Write-Host ($i * $j) "`t" -NoNewline
    }
    Write-Host ""
}