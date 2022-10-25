#include <stdio.h>
#include <stdlib.h>
#include <string.h>

const int maxsorok=50;
const int maxsorhossz=255;

void beolvas(char filenev[], char x[maxsorok][maxsorhossz])
{
    FILE *fp;
    fp = fopen(filenev, "r");
    char buff[maxsorhossz];
    int db=0;
    while (fgets(buff, maxsorhossz, fp) != NULL)
    {
        db++;
    }
    x[db][0]='E';
    x[db][1]='N';
    x[db][2]='D';
    for (int i = 0; i < db; i++)
    {
        fgets(x[i], maxsorhossz, fp);
    }
    fclose(fp);
    printf("----------\nbeolvastal\n----------\n");
}

void kiir(char x[maxsorok][maxsorhossz])
{
    for (int i = 0; i < maxsorok; i++)
    {
        //if(strcmp(x[i],"END")) break;
        printf("%s",x[i]);
    }
}

int main(int argc, char *argv[])
{
    char szavak[maxsorok][maxsorhossz];
    /*     
    char proba[3][5];
    proba[0][0]='e';
    proba[0][1]='g';
    proba[0][2]='y';
    proba[1][0]='n';
    proba[1][1]='e';
    proba[1][2]='m';
    proba[2][0]='E';
    proba[2][1]='N';
    proba[2][2]='D'; 
    */
    char filenev[]={"ora6.txt"};
    beolvas(filenev,szavak);
    for (int i = 0; i < maxsorok; i++)
    {
        //if(strcmp(szavak[i],"END")) break;
        printf("%s",szavak[i]);
    }
    return 0;
}
