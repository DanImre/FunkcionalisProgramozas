#include<stdio.h>
#include<time.h>
#include<stdlib.h>
#include<math.c>

int beker();
int check(int bekert, int i);
void ertekeles(int hibak);
/*
int get_target();
void guessing();
void evaluate();*/

int main()
{
    srand(time(NULL));
    int i = rand() % 100;
    int hibak = 0;
    /*
    printf("%d\n",i);
    printf("Talald ki a szamot !\n");*/
    while (1)
    {
        if(check(beker(),i))
        {
            break;
        }

        ++hibak;
    }

    ertekeles(hibak);
    return 0;
}

int beker()
{
    return scanf("%d");
}

int check(int bekert, int i)
{
    //printf("%d\n", bekert);
    if(bekert == i)
    {
        return 1;
    }
    else if(bekert < i)
    {
        printf("ennel nagyobb\n");
    }
    else
    {
        printf("ennel kisebb\n");
    }
    return 0;
}

void ertekeles(int hibak)
{
    if(hibak < 5)
    {
        printf("Ennyi ! 5 alatt megvolt.\n");
    }
    else if(hibak < 10)
    {
        printf("Nem rossz. 10 alatt megvolt.\n");
    }
    else
    {
        printf("borzalmas. nem volt meg 10 alatt.\n");
    }
}