if [ $# -lt 2 ]
then
    echo "Kevés paraméter !"
else
    declare -i a=$1
    declare -i b=$2
    while [ $a -ne $b ]
    do
        if [ $a -gt $b ]
        then
            a=$(($a-$b))
        else
            b=$(($b-$a))
        fi
    done
    echo $a
fi