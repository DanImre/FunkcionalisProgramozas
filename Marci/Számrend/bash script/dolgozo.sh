#!/bin/bash

filename='proba.txt'
declare -a seged
IFS=',' #ez alapján splitel

#1, 
van='n'
echo "Pultosok:"
while read line;
do
    read -ra seged <<< "$line"
    nev=${seged[0]}
    beosztas=${seged[1]}
    if [ $beosztas == "Pultos" ]
    then
        echo $nev
        van='v'
    fi
done < $filename
if [ $van == 'n' ]
then
    echo "NINCS"
fi

#2
echo "3-nál több munkaköre van:"
while read line;
do
    read -ra seged <<< "$line"
    nev=${seged[0]}
    db=${#seged[@]}
    db=$((db-2))
    if [ $db -gt 3 ]
    then
        echo $nev
    fi
done < $filename

#3
echo -n "Adj meg egy pozíciót: "
read poz
db=0
while read line;
do
    read -ra seged <<< "$line"
    beosztas=${seged[1]}
    if [ $beosztas == $poz ]
    then
        db=$((db+1))
    fi
done < $filename
echo "Az adott pozícióban $db ember dolgozik."