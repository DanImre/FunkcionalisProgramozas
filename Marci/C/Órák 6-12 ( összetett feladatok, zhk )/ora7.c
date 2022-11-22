#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <string.h>
#include <math.h>
#include <ctype.h>

const int oldal = 10;
int db;

void drawTable(int table[oldal][oldal], int shown[oldal][oldal])
{
    int kiir[oldal+1][oldal+1];
    kiir[0][0]=32;
    for (int i = 1; i <= oldal; i++)
    {
        kiir[0][i] = i-1;
        kiir[i][0] = 64 + i;
    }
    for (int i = 1; i <= oldal; i++)
    {
        for (int j = 1; j <= oldal; j++)
        {
            if( shown[i][j]==0 )
            {
                kiir[i][j] = table[i][j];
            }
            else kiir[i][j]=0;
        }
    }
    for (int i = 0; i <= oldal; i++)
    {
        for (int j = 0; j <= oldal; j++)
        {
            if(j==0) printf("%c\t",kiir[i][j]);
            else printf("%d\t", kiir[i][j]);
        } 
        printf("\n");
    }
}

void initTable(int table[oldal][oldal])
{
    for (int i = 0; i < oldal; i++)
    {
        for (int j = 0; j < oldal; j++)
        {
            table[i][j]=0;
        }
    }    
    srand(time(NULL));
    int elhelyezett = 0;
    while (elhelyezett != db)
    {
        int randomX = rand() % 9;
        int randomY = rand() % 9;
        if(table[randomX][randomY] != -1)
        {
            ++elhelyezett;
            table[randomX][randomY] = -1;
        }
    }
    
}

void countMines(int table[oldal][oldal])
{
    for (int i = 0; i < oldal; i++)
    {
        for (int j = 0; j < oldal; j++)
        {
            if(table[i][j]!=-1)
            {
                if(i > 0 && table[i-1][j] == -1) table[i][j]++; //balra
                if(i < 9 && table[i+1][j] == -1) table[i][j]++;  //jobbra
                if(j > 0 && table[i][j-1] == -1) table[i][j]++;  //fel
                if(j < 9 && table[i][j+1] == -1) table[i][j]++;  //le
                if(j < 9 && i < 9 && table[i+1][j+1] == -1) table[i][j]++;  //jobbrafel
                if(j < 9 && i > 0 && table[i-1][j+1] == -1) table[i][j]++;  //balrafel
                if(j > 0 && i < 9 && table[i+1][j-1] == -1) table[i][j]++;  //jobbrale
                if(j > 0 && i > 0 && table[i-1][j-1] == -1) table[i][j]++;  //balrale
            }
        }
    }
}

void game(int table[oldal][oldal], int shown[oldal][oldal])
{
    int rdb = db;
    initTable(table);
    countMines(table);
    while (rdb!=0)
    {
        printf("Adj meg egy koordinatat! : ");
        char be[3];
        scanf("%s", &be);
        int i = be[0]-65;
        int j = be[1]-48;
        printf("---%d %d: %d---\n",i,j,table[i][j]);
        if(table[i][j]!=-1)
        {
            //shown[i][j]=0;
            drawTable(table,shown);
        }
        else
        {
            printf("Akna\n");
            rdb=0;
        }
    }
    
}

int main(int argc, char *argv[])
{
    db=atoi(argv[1]);
    int table[oldal][oldal];
    int showntable[oldal][oldal];
    for (int i = 0; i < oldal; i++)
    {
        for (int j = 0; j < oldal; j++)
        {
            showntable[i][j]=0;
        }
    }
    game(table,showntable);
    return 0;
}