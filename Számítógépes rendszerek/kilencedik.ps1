param($a)

if(!$a)
{
    Write-Host "Add meg a file nevet!"
    $a = Read-Host
}

$f = Get-Content $a

#"" > ki1.txt
#"" > ki2.txt

$index = 0
foreach ($item in $f) {
    Write-Host "$index. sor: $item"
    $index += 1
}