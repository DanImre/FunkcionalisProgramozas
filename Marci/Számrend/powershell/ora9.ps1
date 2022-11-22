Get-Content sor.txt | Measure-Object ## tömbbe szedi a sorokat

Write-Host "Parameterek szama: " $args.Length

$s=$args[0]*$args[1]
Write-Host "Szorzat: $s"

$sor = Get-Content sor.txt
Write-Host "A file masodik sora: "
$sor[1]