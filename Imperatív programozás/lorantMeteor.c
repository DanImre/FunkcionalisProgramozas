#include <stdio.h>
#include <time.h>
#include <stdlib.h>
#include <math.h>
#include <ctype.h>
#include <string.h>
#include <stdbool.h>

int main()
{
    int oszlop = 0;
    int sor = 0;
    int meteorok[10][10];

    int helyzet = 0;
    int meteorokSzama = 0;
    for (size_t i = 0; i < oszlop; i++)
    {
        if(meteorok[0][i] = 1)
        {
            helyzet = i;
            break;
        }
    }

    for (size_t i = 1; i < sor; i++)
    {
        int min = sor;
        int minHely = -1;
        bool vanEgyenloTavolsagra = false;
        for (size_t j = 0; j < oszlop; j++)
        {
            if(meteorok[i][j] == 1 && abs(helyzet - j) < min)
            {
                min = abs(helyzet - j);
                minHely = j;
            }
            else if(meteorok[i][j] == 1 && abs(helyzet - j) == min)
            {
                vanEgyenloTavolsagra = true;
            }
        }

        if(minHely == -1 || (vanEgyenloTavolsagra && min == 1))
        {
            //semmi
        }
        else if(min == 0)
        {
            --meteorokSzama;
        }
        else if(min == 1)
        {
            helyzet = minHely;
            --meteorokSzama;
        }
        else if(!vanEgyenloTavolsagra && min > 1)
        {
            if(minHely < helyzet)
            {
                --helyzet;
            }
            if(minHely > helyzet)
            {
                ++helyzet;
            }
        }
        else if(min > 1)
        {
            int bal = 0;
            for (size_t j = 0; j < helyzet; j++)
            {
                if(meteorok[i][j] == 1)
                {
                    ++bal;
                }
            }

            int jobb = 0;
            for (size_t j = helyzet + 1; j < oszlop; j++)
            {
                if(meteorok[i][j] == 1)
                {
                    ++jobb;
                }
            }

            if(bal > jobb)
            {
                --helyzet;
            }
            else if (jobb > bal)
            {
                ++helyzet;
            }
        }

    }
}