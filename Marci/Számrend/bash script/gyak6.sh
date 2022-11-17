#!/bin/bash

echo -n "Mondj egy number: "
read num

fact=1

while [ $num -gt 1 ]
do
  fact=$((fact * num))  #fact = fact * num
  num=$((num - 1))      #num = num - 1
done

echo $fact
echo -n "Mondj egy word: "
read szo

while [ $szo != "vége" ]
do
    read szo
done
echo "Tényleg vége"

