#!/bin/bash
filename='be.txt'
n=1;
declare -a nevekArray
declare -a besorolasokArray
declare -a munkakorokArray # itt '-'-ekkel elvalasztva lesznek
declare -a sorhosszak #literalisan a sorok hossza
sorhosszak[0]=0
while read line; 
do
    IFS=',' read -r -a array <<< "$line" #beolvassa az array-be
    munkakorokArray[n]=""
    for index in "${!array[@]}"
    do
        if [ $index -gt 0 ]
        then
            array[index]=${array[index]:1}
        else
            nevekArray[n]=${array[index]} 
        fi

        if [ $index -eq 1 ]
        then
            besorolasokArray[n]=${array[index]}
        fi

        if [ $index -gt 1 ]
        then
            if [ $index -eq ${#array[@]} ]
            then
                munkakorokArray[n]="${munkakorokArray[n]}${array[index]}"
            else    
                munkakorokArray[n]="${munkakorokArray[n]}${array[index]}-"
            fi
        fi
    done
    n=$((n+1))
done < $filename

#1. feladat
echo "a) feladat:"
n=0
for index in "${!besorolasokArray[@]}"
do
    if [ "${besorolasokArray[index]}" == "Pultos" ]
    then
        echo "${nevekArray[index]}"
        n=$((n+1))
    fi
done

if [ $n == "0" ]
then
    echo "NINCS"
fi

#2. feladat
echo "b) feladat:"
for index in "${!nevekArray[@]}"
do
    IFS='-' read -r -a array <<< "${munkakorokArray[index]}"
    if [ ${#array[@]} -gt 2 ]
    then
        echo "${nevekArray[index]}"
    fi
done

#3. feladat
echo "c) feladat:"
echo -n "Adjon meg egy besorolást: "
read besorolas
n=0
for index in "${!besorolasokArray[@]}"
do
    if [ "${besorolasokArray[index]}" == "$besorolas" ]
    then
        n=$((n+1))
    fi
done

echo "$n"