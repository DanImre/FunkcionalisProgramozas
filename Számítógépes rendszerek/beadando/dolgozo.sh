#!/bin/bash
filename='be.txt'
n=1;
declare -a nagyArray
declare -a sorhosszak #literalisan a sorok hossza
sorhosszak[0]=0
while read line; 
do
    IFS=',' read -r -a array <<< "$line" #beolvassa az array-be
    for index in "${!array[@]}"
    do
        if [ $index -gt 0 ]
        then
            array[index]=${array[index]:1}
        fi
        actualIndex=$((index-((n-1))+sorhosszak[$((n-1))]))
        nagyArray[actualIndex]=${array[index]} #leszedi a space-t és beteszi az arraybe
        sorhosszak[n]=$((index+1))
        #echo "${nagyArray[actualIndex]}"
    done
    #arrayOfArrays[n]=$array
    n=$((n+1))
done < $filename

#1. feladat
echo "Elso feladat:"
declare -a pultosok
n=1
for index in "${!nagyArray[@]}"
do
    actualIndex=$((index-((n-1))+sorhosszak[$((n-1))]))
    temp="${nagyArray[actualIndex]}"
    if [ $active -eq $"Pultos" ]
    then
        pultosok[n]=${nagyArray[((actualIndex-index))]}
        n=$((n+1))
    fi
    ##echo "$index ${array[index]}"
done

for index in "${!pultosok[@]}"
do
    echo "${pultosok[index]}, "
done
