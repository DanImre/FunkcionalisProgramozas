#include<stdio.h>
#include<time.h>
#include<stdlib.h>

int main()
{
    int N = 0;
    int M = 0;
    printf("1. szam (N):\n");
    scanf("%d", &N);
    printf("2. szam (M):\n");
    scanf("%d", &M);

    if(N<=M)
    {
        printf("A paros szamok:");
        for(int i = N; i <= M; ++i)
        {
            if(i%2 == 0)
            {
                printf("%d\n",i);
            }
        }

        printf("A hettel oszthato szamok:\n");
        //int szamlalo = 0;
        int szamlalo = M;
        while(szamlalo >= N)
        {
            if(szamlalo%7 == 0)
            {
                printf("%d\n",szamlalo--);
            }
            else
            {
                --szamlalo;
            }
        }
    }
}