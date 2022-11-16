#include<stdio.h>

const int oldal = 10;
int table[oldal][oldal];
int hajok[4];

void init(int x[oldal][oldal], int h[])
{
    for(int i=0;i<oldal;i++)
    {
        for(int j=0;j<oldal;j++)
        {
            x[i][j] = 0;
        }
    }
    x[0][0] = 1;
    x[0][9] = 1;
    x[9][0] = 1;
    x[9][9] = 1;
    for(int i = 0;i < 4;i++)
    {
        h[i] = 1;
        if(i==1) h[i] = 2;
    }
}

void printTable(int x[oldal][oldal])
{
    x[0][0] = 0;
    x[0][9] = 0;
    x[9][0] = 0;
    x[9][9] = 0;
    printf("    ");
    for(char i = 'A';i <= 'J';i++)
    {
        printf("%c  ",i);
    }
    printf("\n");
    for(int i=0;i<oldal;i++)
    {
        if(i==9) printf("%d. ",i+1);
        else printf("%d.  ",i+1);
        for(int j=0;j<oldal;j++)
        {
            printf("%d  ",x[i][j]);
        }
        printf("\n");
    }
    printf("\n");
}

int valid(int x[oldal][oldal], int h[], char hely[3], int meret, char irany)
{
    int oszlop = hely[0] - 'A';
    int sor = hely[1] - 49;
    if(hely[2]=='0') sor = 9;
    if(h[meret-2]==0) return 3;
    if(irany=='_')
    {
        for(int i=0;i<meret;i++)
        {
            if(oszlop+i>=oldal) return 2;
            if(x[sor][oszlop+i]!=0) return 1;
        }
    }
    else
    {
        for(int i=0;i<meret;i++)
        {
            if(sor+i>=oldal) return 2;
            if(x[sor+i][oszlop]!=0) return 1;
        }
    }
    return 0;
}

void submit(int x[oldal][oldal], int h[], char hely[3], char irany, int meret)
{
    int valide = valid(x,h,hely,meret,irany);
    int oszlop = hely[0] - 'A';
    int sor = hely[1] - 49;
    if(hely[2]=='0') sor = 9;
    if(valide == 3) printf("Nincs eleg hajo!\n");
    else if(valide == 2) printf("A hajo lelog a tablarol!\n");
    else if(valide == 1) printf("Itt mar van hajo vagy sarokhoz er!\n");
    else
    {
        if(irany == '_')
        {
            for(int i=0;i<meret;i++)
            {
                x[sor][oszlop+i] = 1;
            }
            h[meret-2]--;
        }
        else
        {
            for(int i=0;i<meret;i++)
            {
                x[sor+i][oszlop] = 1;
            }
            h[meret-2]--;
        }
    }
}

void game(int x[oldal][oldal], int h[], char *hely[5], char irany[5], int meret[5])
{
    init(table,hajok);
    for(int i = 0; i < 5; i++)
    {
        submit(table,hajok,hely[i],irany[i],meret[i]);
    }
    int db=0;
    for(int i=0;i<4;i++)
    {
        if(h[i]==0) db++;
    }
    printTable(table);
    if(db<4) printf("Helytelen felallitas!\n");
    else printf("Helyes felallitas!\n");
}

int main()
{
    char *probaHely1[] = {"A2", "H6", "D2", "C6", "F4"}; // hibás próba, két hajó összeér
    int proaHossz1[] = {2,3,3,4,5};
    char probaIrany1[] = {'_','|','|','_','|'};
    char *probaHely2[] = {"A2", "H6", "D2", "C6", "H9"}; // hibás próba, lelóg egy hajó
    int proaHossz2[] = {2,3,3,4,5};
    char probaIrany2[] = {'_','_','_','_','_'};
    char *probaHely3[] = {"A2", "H6", "D2", "C6", "A1"}; // hibás próba, egy hajó a sarokhoz ér
    int proaHossz3[] = {2,3,3,4,5};
    char probaIrany3[] = {'_','|','|','_','_'};
    char *probaHely4[] = {"A2", "A4", "A6", "A8", "B10"}; // hibás próba, nincs elég ugyanakkora hajó
    int proaHossz4[] = {2,3,3,3,3};
    char probaIrany4[] = {'_','_','_','_','_'};
    char *probaHely5[] = {"A2", "A4", "A6", "A8", "B10"}; // jó felállás
    int proaHossz5[] = {2,3,3,4,5};
    char probaIrany5[] = {'_','_','_','_','_'};
    game(table,hajok,probaHely1,probaIrany1,proaHossz1);
    game(table,hajok,probaHely2,probaIrany2,proaHossz2);
    game(table,hajok,probaHely3,probaIrany3,proaHossz3);
    game(table,hajok,probaHely4,probaIrany4,proaHossz4);
    game(table,hajok,probaHely5,probaIrany5,proaHossz5);
    return 0;
}