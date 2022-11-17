$dolgozok = Get-Content dolgozok.txt

#1.
Write-Host "Pultosok:"
$van=0
foreach($i in $dolgozok)
{
    $sor=$i.Split(",")
    if($sor[1] -eq "Pultos")
    {
        Write-Host $sor[0]
        $van=1
    }
}
if($van -eq 0)
{
    Write-Host "NINCS"
}
#2. 
Write-Host "Haromnal tobb munkakore van:"
foreach($i in $dolgozok)
{
    $sor=$i.Split(",")
    if($sor.Count -gt 5)
    {
        Write-Host $sor[0]
    }
}
#3.
Write-Host "Adj meg egy poziciot:"
$poz=Read-Host
$db=0
foreach($i in $dolgozok)
{
    $sor=$i.Split(",")
    if($poz -eq $sor[1])
    {
        $db++
    }
}
Write-Host "Az adott pozicioban"$db" ember dolgozik"