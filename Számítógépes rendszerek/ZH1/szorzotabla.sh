for i in {1..9}
do
    for j in {1..9}
    do
        echo -n -e "$((i*j)) \t"
    done
    echo ""
done