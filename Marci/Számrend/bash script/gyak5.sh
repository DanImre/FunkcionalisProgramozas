#!/bin/bash

echo -n "Mondj egy számot: "
read SZAM
if [ $SZAM -lt 10 ]
then
    echo "Egyjegyű szám"
else
    echo "Nem egyjegű szám"
fi

echo "-----"
T=4
[ $T -eq 3 ] && K=8 || K=2 
echo $K

echo "-----"
echo -n "Add meg a hét napjának sorszámát: "
read NAP
case $NAP in
1)echo "Hétfő" ;;
2)echo "Kedd" ;;
3)echo "Szerda" ;;
4)echo "Csütörtök" ;;
5)echo "Péntek" ;;
6)echo "Szombat" ;;
7)echo "Vasárnap" ;;
*)echo "Hibás adat" ;;
esac

echo "-----"
for I in alma körte dió
do
echo $I
done

echo "-----"
for I in $( seq 1 2 10 )
do
echo $I
done

echo "-----"
#kiirja az osszes filet
for I in *
do
echo $I
done