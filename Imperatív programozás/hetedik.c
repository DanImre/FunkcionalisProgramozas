#include<stdio.h>
#include<stdlib.h>

void drawTable(int table[10][10], int showntable[10][10]);
void initTable(int n, int table[10][10]);
void countNeighbourMines(int table[10][10]);

int main(int argc, char *argv[])
{
    int table[10][10];
    int showntable[10][10];
    for (size_t i = 0; i < 10; i++)
    {
        for (size_t j = 0; j < 10; j++)
        {
            showntable[i][j] = 0;
        }
    }
    
    int megadottertek = atoi(argv[1]);

    initTable(megadottertek,table);
    countNeighbourMines(table);
    drawTable(table, showntable);

    int counter = 100 - megadottertek;
    int vege = 0;
    while (vege == 0 && counter != 0)
    {
        printf("Adjon egy kordinatat:\n");
        char beolvasott[100];
        scanf("%s",beolvasott);
        if(strcmp(beolvasott,"save") == 0)
            {
                scanf("%s",beolvasott);
                FILE *fp = fopen(beolvasott,"w");
                for (size_t i = 0; i < 10; i++)
                {
                    for (size_t j = 0; j < 10; j++)
                    {
                        if(table[i][j] == -1)
                        {
                            fprintf(fp,"%d,%d\n",i,j);
                        }
                    }
                    
                }
                fprintf(fp,"show\n");
                for (size_t i = 0; i < 10; i++)
                {
                    for (size_t j = 0; j < 10; j++)
                    {
                        if(showntable[i][j] == 1)
                        {
                            fprintf(fp,"%d,%d\n",i,j);
                        }
                    }
                }
                
                fclose(fp);                
            }
        else if(strcmp(beolvasott,"load") == 0)
            {
                scanf("%s",beolvasott);
                FILE *fp = fopen(beolvasott,"w");
                    for (size_t i = 0; i < 10; i++)
                    {
                        for (size_t j = 0; j < 10; j++)
                        {
                            table[i][j] = 0;
                            showntable[i][j] = 0;
                        }
                        
                    }
                
                int shown = 0;
                while (fgets(beolvasott,255,fp) != NULL)
                {
                    if(strcmp(beolvasott,"show") == 0)
                    {
                        shown = 1;
                    }
                    else if (shown == 0)
                    {
                        table[(int)beolvasott[0]-48][(int)beolvasott[2]-48] = -1;
                    }
                    else
                    {
                        showntable[(int)beolvasott[0]-48][(int)beolvasott[2]-48] = 1;
                    }
                }
                
                countNeighbourMines(table);
                drawTable(table, showntable);
                fclose(fp);                
            }
        else if(table[(int)beolvasott[0]-65][(int)beolvasott[1]-48] == -1) 
        {
            vege = 1;
            printf("\n");
            printf("BOMBA !\n");
        }
        else
        {
            --counter;
            showntable[(int)beolvasott[0]-65][(int)beolvasott[1]-48] = 1;
            drawTable(table, showntable);
        }

    }
    

    return 0;
}

void drawTable(int table[10][10], int showntable[10][10])
{
    printf(" \t 0 \t 1 \t 2 \t 3 \t 4 \t 5 \t 6 \t 7 \t 8 \t 9 \n");
    for (size_t i = 0; i < 10; i++)
    {
        printf("%c ",i+65);
        for (size_t j = 0; j < 10; j++)
        {
            if(showntable[i][j] == 1)
            {
                printf("\t %d",table[i][j]);
            }
            else
            {
                printf("\t ");
            }
        }
        printf("\n");   
    }
}

void initTable(int n, int table[10][10])
{
    srand(time(NULL));
    int elhelyezett = 0;
    while (elhelyezett != n)
    {
        int randomX = rand() % 10;
        int randomY = rand() % 10;
        if(table[randomX][randomY] != -1)
        {
            ++elhelyezett;
            table[randomX][randomY] = -1;
        }
    }
    
}

void countNeighbourMines(int table[10][10])
{
    for (int i = 0; i < 10; i++)
    {
        for (int j = 0; j < 10; j++)
        {
            if(table[i][j] != -1)
            {
                int temp = 0;
                if(i > 0 && table[i-1][j] == -1) ++temp; //balra
                if(i < 9 && table[i+1][j] == -1) ++temp; //jobbra
                if(j > 0 && table[i][j-1] == -1) ++temp; //fel
                if(j < 9 && table[i][j+1] == -1) ++temp; //le
                if(j < 9 && i < 9 && table[i+1][j+1] == -1) ++temp; //jobbrafel
                if(j < 9 && i > 0 && table[i-1][j+1] == -1) ++temp; //balrafel
                if(j > 0 && i < 9 && table[i+1][j-1] == -1) ++temp; //jobbrale
                if(j > 0 && i > 0 && table[i-1][j-1] == -1) ++temp; //balrale

                table[i][j] = temp;
            }
        }
        
    }
    
}