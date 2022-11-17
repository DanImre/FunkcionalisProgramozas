#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <string.h>

const int szamdb = 20; // ennyi szamot generálunk
static int n; // ennyi szót kérünk be max
static int talalt = 0; //8. pontban használt változó

void reverseip(char* szo) //2. helyben fordít meg egy stringet
{
    char* rev = malloc(strlen(szo)); // malloc olyan hosszan amilyen hosszú a szó
    for (int i = 0; i < strlen(szo); i++)
    {
        rev[i] = szo[strlen(szo)-(i+1)]; // maga a csere
    }
    strcpy(szo,rev); // belemásoljuk a fordítottat az eredeti helyére
    free(rev);
}

char *reverse(char *szo) //3. visszaad egy fordított stringet
{
    char* rev = malloc(strlen(szo)); //ugyanaz mint 2. csak nincs másolás
    for (int i = 0; i < strlen(szo); i++)
    {
        rev[i] = szo[strlen(szo)-(i+1)];
    }
    return rev;
}

int beolvasnrev(char* x[])//4-7. beolvasz szavakat max n-ig vagy addig amíg nem mondjuk hogy EOF (ctrl+z)
{
    int i = 0;
    int n2;
    char v;
    printf("%d db szo (max 20 char): ",n);
    while(i < n && (v = getchar()) != EOF) // addig megyünk amíg nem találunk EOF karakret
    {
        x[i] = malloc(21); // malloc a "string tömb" i. elemének
        scanf("%s",x[i]);
        n2 = i-1;
        i++;
    }
    printf("Forditott sorrendben:\n");
    for (int j = n2; j >= 0; j--)
    {
        printf("%s\n",x[j]);
    }
    return n2; // visszaadjuk hogy valójában mennyi szót kértünk be (kell a freehez)
}

int *span(int x[]) //8. a párosokat a tömb elejére teszi, a páratlanokat hátra
{
    int *y = malloc(sizeof(int)*szamdb); //igazából szamdb elemű tömb
    int odb = 0; //páratlanok száma
    int edb = 0; //párosok száma
    for (int i = 0; i < szamdb; i++)
    {
        if(x[i]%2 == 0) //előről tölt fel
        {
            y[edb] = x[i];
            edb++;
        }
        else //hátulróé tölt fel
        {
            y[szamdb-odb-1] = x[i];
            odb++;
        }
    }
    return y; // a változtatott tömbre mutató pointert ad vissza
}

int main(int argc, char *argv[])
{
    n = atoi(argv[1]); // itt adjuk meg a 7. sor n-jét
    //1.
    char *szo = malloc(21); //egy karakter egy byte -> nem kell sizeof(char), 20 helyett 21 mert \0
    do
    {
        printf("Szo: (max 20 char): ");
        scanf("%s", szo);
    } while (strlen(szo)>21);
    char *szocopy = malloc(strlen(szo));
    strcpy(szocopy,szo);
    printf("Masolas utan: %s\n",szocopy);
    free(szocopy);
    //2.
    reverseip(szo);
    printf("Forditva: %s\n",szo);
    reverseip(szo);
    //3.
    char *rev = reverse(szo);
    printf("Forditva (masik memoriacimen): %s\n",rev);
    free(rev);
    //4-7.
    char *szavak[n];
    int n2 = beolvasnrev(szavak);
    //8.
    int szamok[szamdb];
    int sortedszamok[szamdb];
    srand(time(NULL));
    for (int i = 0; i < szamdb; i++)
    {
        szamok[i] = (rand() % 100) + 1;
    }
    int *ptr = span(szamok);
    printf("Szamok rendezes nelkul:\n");
    for (int i = 0; i < szamdb; i++)
    {
        printf("%d\t",szamok[i]);
        if((i+1)%10 == 0) printf("\n");
    }
    printf("\nSzamok rendezve:\n");
    for (int i = 0; i < szamdb; i++)
    {
        printf("%d\t",ptr[i]);
        if(ptr[i+1]%2==1 && talalt == 0) 
        {
            printf("\n");
            talalt = 1;
        }
    }
    //memória visszaadása
    free(szo);
    for (int i = 0; i < n2; i++)
    {
        free(szavak[i]);
    }
    free(ptr);
    return 0;
}