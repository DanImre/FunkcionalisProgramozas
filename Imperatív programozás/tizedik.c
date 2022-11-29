#include <memory.h>
#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <string.h>
#include <time.h>
#include <ctype.h>
#include <math.h>

int main(int argc, char *argv[])
{
    int hegytolLegnagyobbIdealisTavolsag, vizforrastolLegfeljebbiTavolsag, homokvihartolMinimalisBiztTavolsag;
    int i, j, sor, oszlop;
    FILE *ki;
    FILE *be=fopen("be.txt", "r");
    fscanf(be, "%d %d %d", &hegytolLegnagyobbIdealisTavolsag, &vizforrastolLegfeljebbiTavolsag, &homokvihartolMinimalisBiztTavolsag);
    fscanf(be,"%d %d", &sor, &oszlop);
    char** terkep;
    terkep = (char **)malloc(sor*sizeof (char*));
    for (int i = 0; i < sor; ++i) {
        terkep[i] = (char *)malloc(oszlop* sizeof (char));
    }
    for(i=0; i<sor; ++i)
    {
        for (j = 0;  j<oszlop+1; ++j)
        {
            fscanf(be, "%c", &terkep[i][j]);
            printf("%c ", terkep[i][j]);
        }
        printf("\n");
    }



    if(be==NULL)
    {
        ki=fopen("ki.txt", "w");
        fprintf(ki, "%d\n", 0);
        fclose(ki);
        return 0;
    }
    fclose(be);

    
    int solution = 0;

    bool ezazelso = true;

    for (size_t i = 0; i < sor; i++)
    {
        for (size_t j = 0; j < oszlop + 1; j++)
        {
            if(terkep[i][j] == 'A' || terkep[i][j] == 'X')
            {
                continue;
            }

            //víz check
            bool vizes = false;
            for (int ii = i - vizforrastolLegfeljebbiTavolsag; ii < sor && ii <= i + vizforrastolLegfeljebbiTavolsag; ii++)
            {
                for (int jj = j - vizforrastolLegfeljebbiTavolsag; jj < oszlop + 1 && jj <= j + vizforrastolLegfeljebbiTavolsag; jj++)
                {
                    if(ii < 0 || jj < 0)
                    {
                        continue;
                    }
                    if((int)terkep[ii][jj] == 126)
                    {
                        vizes = true;
                        break;
                    }
                }
                if(vizes)
                {
                    break;
                }
            }
            //hegy check
            bool hegyes = false;
            for (int ii = i - hegytolLegnagyobbIdealisTavolsag; ii < sor && ii <= i + hegytolLegnagyobbIdealisTavolsag; ii++)
            {
                for (int jj = j - hegytolLegnagyobbIdealisTavolsag; jj < oszlop + 1 && jj <= j + hegytolLegnagyobbIdealisTavolsag; jj++)
                {
                    if(ii < 0 || jj < 0)
                    {
                        continue;
                    }
                    if(terkep[ii][jj] == 'A')
                    {
                        hegyes = true;
                        break;
                    }
                }
                if(hegyes)
                {
                    break;
                }
            }
            //homokvihar check
            bool homokos = false;
            for (int ii = i - homokvihartolMinimalisBiztTavolsag; ii < sor && ii <= i + homokvihartolMinimalisBiztTavolsag; ii++)
            {
                for (int jj = j - homokvihartolMinimalisBiztTavolsag; jj < oszlop + 1 && jj <= j + homokvihartolMinimalisBiztTavolsag; jj++)
                {
                    if(ii < 0 || jj < 0)
                    {
                        continue;
                    }
                    if(terkep[ii][jj] == 'X')
                    {
                        homokos = true;
                        break;
                    }
                }
                if(homokos)
                {
                    break;
                }
            }
            if(vizes && hegyes && !homokos)
            {
                ++solution;
            }

        }
    }

    ki=fopen("ki.txt", "w");
    fprintf(ki, "%d\n",solution);
    fclose(ki);
    for (int i = 0; i < sor; ++i)
    {
        free(terkep[i]);
        terkep[i]=NULL;
    }

    free(terkep);
    terkep=NULL;


}