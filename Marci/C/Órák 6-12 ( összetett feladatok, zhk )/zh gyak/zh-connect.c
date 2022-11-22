#include <stdio.h>
#include <string.h>

const int sor= 6;
const int oszlop = 7;

void init(int x[sor][oszlop], int h[]) //kinullázza a táblát és feltölti a hely[]-t a max helyekkel (sor-1)
{
    for (int i = 0; i < sor; i++)
    {
        for (int j = 0; j < oszlop; j++)
        {
            x[i][j]=0;
        }
    }
    for (int i = 0; i < oszlop; i++)
    {
        h[i]=sor-1;
    }
}

void printTable(int x[sor][oszlop]) // kiir
{
    printf("\n");
    for (int i = 0; i < sor; i++)
    {
        for (int j = 0; j < oszlop; j++)
        {
            printf("%d  ",x[i][j]);
        }
        printf("\n");
    }
    printf("\n");
}

int valid(int h[], char c) //megnézi, hogy egy lépés valid-e
{
    int coszlop = c - 'A'; //igazából c ASCII kódjából von ki 65-t
    if(c < 'A' || c > 'G') return 1;
    else if(h[coszlop] < 0) return 2;
    else return 0;
}

void submit(int x[sor][oszlop], int h[], char c, int jatekos) // ha valid, akkor beirja a táblába
{
    int coszlop = c - 'A';
    if(valid(h,c) == 1)
    {
        printf("Ervenytelen oszlop!\n");
    }
    else if(valid(h,c) == 2)
    {
        printf("Oszlop tele!\n");
    }
    else
    {
        x[h[coszlop]][coszlop] = jatekos;
        h[coszlop]--;
    }
}

int eval(int x[sor][oszlop]) // a győztest adja vissza, vagy 0-t ha döntetlen
{
    for (int i = 1; i < sor-1; i++) //oszlopos win
    {
        for (int j = 0; j < oszlop; j++)
        {
            if(x[i][j]==x[i-1][j] && x[i][j]==x[i+1][j] && x[i][j]!=0) return x[i][j];
        }
    }
    for (int i = 0; i < sor; i++) //soros win
    {
        for (int j = 1; j < oszlop-1; j++)
        {
            if(x[i][j]==x[i][j-1] && x[i][j]==x[i][j+1] && x[i][j]!=0) return x[i][j];
        }
    }
    return 0;
}

void game(int x[sor][oszlop], int h[], char *be)
{
    init(x,h);
    for (int i = 0; i < strlen(be); i++)
    {
        submit(x,h,be[i],(i%2)+1);
        if(eval(x)==1)
        {
            printf("First player wins");
            break;
        }
        if(eval(x)==2)
        {
            printf("Second player wins");
            break;
        }
        if(i+1 == strlen(be)) printf("Draw");
    }
    printTable(x);
}

int main (int argc, char *argv[])
{
    int table[sor][oszlop];
    int hely[oszlop];
    char *be = "ABDCAEEEEEEFFFAC";
    game(table,hely,be);
    return 0;
}