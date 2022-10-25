#!/bash/bin

echo "Adj meg egy szamot!"
read A

eredmeny=1

while [ $A -gt 1 ]
do
    eredmeny=$((eredmeny * A))
    A=$((A - 1))
done

echo "A faktorialisa: $eredmeny"

#2. feladat
echo "Irj szavakat, a 'Vége' szo utan leall."

read szo
while [ $szo != "vége" ]
do
    read szo
done


#3. feladat
echo "Ez paramteres volt"
# a parameter az $#

osszeg=0
if [ $# -gt 10]
    echo "Tul sok adat"
    exit
fi

if [ $1 == "-help"]
    echo "1 - 10 szám megadasaval megkapod az osszeguket"
fi

for i in $#
do
    osszeg=$((osszeg + $i)) # Az i egy index igazából tehát ötltetem sincs mi van
done

echo osszeg > stdout


#4. feladat

index=1
read B

while [ $index -ne $B]
do
    for i in $( seq 1 $index )
    do
        echo -n "* "
    done
    echo ""
    index=$((index + 1))
done