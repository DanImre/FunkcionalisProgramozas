#include <stdio.h>
#include <stdlib.h>

void valami(char bemenet[], char kimenet[]);

int main()
{
    char hello[14] = "asd dsa hello";
    char viszlat[14] = "              ";
    valami(hello,viszlat);
    //printf("%s\n", viszlat);
    
}

void valami(char bemenet[], char kimenet[])
{
    /*
    asdasdasd
    dasdasd
    */
   
    char forditottSzavak[256];
    int forditottSzavakIndex = 0;

    int i = 0;
    int elozoi = 0;
    while (bemenet[i] != '\0')
    {
        ++i;
        if(bemenet[i] == ' ' || bemenet[i] == '\0')
        {
            int j = i - 1;
            while (j > elozoi)
            {
                forditottSzavak[forditottSzavakIndex] = bemenet[j];
                --j;
                ++forditottSzavakIndex;
            }
            elozoi = i;
            
        }
    }

    int db = 10;
    int szavakbetuinekszama[db];

    
    for (size_t kk = 0; kk < db; kk++)
    {
        char mystr[10];
        sprintf(mystr, "%d", szavakbetuinekszama[kk]);
        kimenet[kk] = mystr[0];
    }

    for (size_t kk = 0; kk < db; kk++)
    {
        kimenet[db + kk] = forditottSzavak[kk];
    }
    
    printf("%s\n", forditottSzavak);
}