#include <stdio.h>
#include <time.h>
#include <stdlib.h>

int get_value() //randomizál egy értéket 1-100
{
    srand(time(NULL));
    return rand() % 100;
}

int guessing(int r,int be) // maga a találgatás, de visszaadja a hibák számát
{
    int hiba = 0;
    while(1)
    {
        printf("Mi lehet a szam? : ");
        scanf("%d",&be);
        if(be==r)
        {
            printf("Igen\n");
            break;
        }
        else
        {
            if(be<r) printf("Nagyobb a szam\n");
            else printf("Kisebb a szam\n" );
        }
        hiba++;
    }
    return hiba;
}

void evaluate(int hiba) // kiértékel 
{
    if(hiba<5) printf("Egesz jo\n");
    else 
    {
        if(hiba<10) printf("Kozepes");
        else printf("Haaat lehetne jobb");
    }
}

int main()
{
    int r,be,hiba=0;
    r = get_value();
    hiba=guessing(r,be);
    evaluate(hiba);
    return 0;
}