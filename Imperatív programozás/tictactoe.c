#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <stdbool.h>

int magassag = 6;
int szelesseg = 7;
int x[6][7];

void init()
{
    for (size_t i = 0; i < magassag; i++)
    {
        for (size_t j = 0; j < szelesseg; j++)
        {
            x[i][j] = 0;
        }
    }
}

void printTable()
{
    for (int i = magassag-1; i >= 0; i--)
    {
        for (size_t j = 0; j < szelesseg; j++)
        {
            printf("%d\t",x[i][j]);
        }
        printf("\n");
    }
}

int submit(int jatekos, char oszlop)
{
    if(jatekos > 2 || jatekos < 1 || oszlop < 65 || oszlop > 71 )
    {
        printf("Nem legális lépés\n");
        return 1;
    }


    for (size_t i = 0; i < magassag; i++)
    {
        if(x[i][(int)oszlop - 65] == 0)
        {
            x[i][(int)oszlop - 65] = jatekos;
            return 0;
        }
    }
    
    printf("Nem legális lépés\n");
    return 1;
}

int evaluate(int jatekos, char oszlop)
{
    int elozolegLetettElemIndexe = 0;
    int counter = 0;
    for (size_t i = 0; i < magassag; i++)
    {
        if(x[i][(int)oszlop - 65] == jatekos)
        {
            ++counter;
            if(counter == 3)
            {
                return 0;
            }
            elozolegLetettElemIndexe = i;
        }
        else
        {
            counter = 0;
        }
    }
    
    int index = (int)oszlop - 65 - 2;
    counter = 0;
    while(index < (int)oszlop - 65 + 3)
    {
        if(index >= 0 && index < szelesseg)
        {
            if(x[elozolegLetettElemIndexe][index] == jatekos)
            {
                ++counter;
                if(counter == 3)
                {
                    return 0;
                }
            }
            else
            {
                counter = 0;
            }
        }

        ++index;
    }
    
    return 1;
}

void game(char input[])
{
    int index = 0;
    int actplayer = 1;
    while (input[index] != '\0')
    {
        //printTable();
        //printf("játékos : %d oszlop : %c , számban : %d \n",actplayer,input[index], (int)input[index] );
        if(submit(actplayer,input[index]) == 0)
        {
            if(evaluate(actplayer,input[index]) == 0)
            {
                if(actplayer == 1)
                {
                    printf("First player wins.\n");
                }
                else
                {
                    printf("Second player wins.\n");
                }

                return;
            }
        }

        if(actplayer == 1)
        {
            actplayer = 2;
        }
        else
        {
            actplayer = 1;
        }

        ++index;
    }
    
    printf("Draw.\n");
}

int main()
{
    init();
    game("ABDCAGEEE");
    init();
    game("ABDCAEEEEEEFFFAC");
    init();
    game("ABDCAEEEEEEFFG");
    init();
    game("AAAAAAABBBBBBBCCCCCCCDDDDDDDEEEEEEEFFFFFFFGGGGGGG");
}