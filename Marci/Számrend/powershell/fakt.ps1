$num = $args[0]
$fact = 1
while($num -gt 1)
{
    $fact = $fact * $num
    $num = $num - 1
}
Write-Host $args[0]"faktorialis:"$fact