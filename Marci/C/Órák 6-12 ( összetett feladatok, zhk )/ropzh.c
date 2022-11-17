#include <stdlib.h>
#include <stdio.h>

#define CNT 3

int fun(int* s, int* a, int e) 
{
    static int db = 0;
    if(db == 0)
    {
        *s = 0;
        *a = 0;
        db++;
    }
    *s += e;
    *a += e/CNT;
    return 0;
}

int main() 
{
    int i, j, sum, avg;
    for(i = 0; i < CNT; i++) {
        scanf("%d", &j);
        fun(&sum, &avg, j);
    }
    printf("Sum: %d\n", sum);
    printf("Avg: %d\n", avg);
    return 0;
}