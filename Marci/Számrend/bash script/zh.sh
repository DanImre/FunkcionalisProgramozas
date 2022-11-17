#!/bin/bash

echo "5. feladat:"
if [ $# -gt 0 ]
then
    echo "Hello $1"
else
    echo "Hello $USER"
fi

echo "6. feladat:"
for i in {1..9}
do
    for j in {1..9}
    do
        echo -n "$((i*j)) "
    done
    echo ""
done

echo "7. feladat:"
szam1=$2
szam2=$3
if [ $szam1 -gt $szam2 ]
then
    i=$szam1
else
    i=$szam2
fi
while [ $i -gt 0 ]
do
    if [ $((szam1 % i)) -eq 0 ] && [ $((szam2 % i)) -eq 0 ]
    then
        echo $i
        exit
    fi
    i=$((i-1))
done