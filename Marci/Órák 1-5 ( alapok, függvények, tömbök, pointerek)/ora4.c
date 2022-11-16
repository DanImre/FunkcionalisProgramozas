#include <stdio.h>
#include <time.h>
#include <stdlib.h>

//hát ezek sem nehezek xdd
int ellentet(int x)
{
    return -x;
}

int max(int x[], int n)
{
    int max=0;
    for (int i = 1; i < n; i++)
    {
        if(x[i]>x[max]) max=i;
    }
    return x[max];
}

int min2(int x[], int n)
{
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            if (x[i] < x[j])
            {
                int a = x[i];
                x[i] = x[j];
                x[j] = a;
            }
        }
    }
    return x[1];
}

int main()
{
    printf("%d\n",ellentet(2));
    int n=5;
    int x[]={1,2,3,4,5};
    printf("%d\n",max(x,n));
    printf("%d\n",min2(x,n));
    return 0;
}