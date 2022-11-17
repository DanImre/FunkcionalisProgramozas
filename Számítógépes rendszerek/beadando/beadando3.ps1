$filename = "be.txt"

$fileContent = Get-Content $filename

#1. feladat
$db = 0
foreach ($item in $fileContent) {
    $tempList = $item.Split(",")
    if($tempList[1] -eq " Pultos")
    {
        Write-Host $tempList[0]
        $db += 1
    }
}
if($db -eq 0)
{
    Write-Host "NINCS"
}

$db = 0
foreach ($item in $fileContent) {
    $tempList = $item.Split(",")
    if($tempList.Count -gt 4)
    {
        Write-Host $tempList[0]
        $db += 1
    }
}
if($db -eq 0)
{
    Write-Host "NINCS ILYEN"
}

$db = 0
$keresettPoz = Read-Host "Keresett pozicio: "
foreach ($item in $fileContent) {
    $tempList = $item.Split(",")
    if($tempList[1] -eq " $keresettPoz")
    {
        $db += 1
    }
}
Write-Host $db