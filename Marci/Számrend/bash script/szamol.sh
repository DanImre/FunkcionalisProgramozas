#!/bin/bash
if [ $# -ne 3 ]
then
    if [[ $l == "-h" ]]
    then
	echo "Használat: szam1 művelet szam2, ahol művelet (+,-,x,/)"
	exit
    fi
    echo "kevés paraméter"
    exit
fi
case $2 in
'+') n=`expr $1 + $3`; echo $n ;;
'-') n=`expr $1 - $3`; echo $n ;;
'x') n=`expr $1 \* $3`; echo $n ;;
'/') n=`expr $1 / $3`; echo $n;;
*) echo Nem támogatott művelet
esac