#include <stdio.h>
#include <time.h>
#include <stdlib.h>
#include <math.h>
#include <ctype.h>
#include <string.h>

int faktorialis(int a);
void hetedikFeladat();

int main(int argc, char *argv[])
{
    //getline
    printf("2. feladat: %d\n", argc);

    printf("3. feladat:\n");
    for (size_t i = 1; i < argc; i++)
    {
        if(strlen(argv[i]) > 5)
        {
            printf("%s \n",argv[i]);
        }
    }
    /*
    Írd ki az 5 karakternél hosszabb szavakat (strlen).
    Írd ki az x karaktert tartalmazó szavakat (strchr).
    Írd ki az alma szót tartalmazó szavakat (pl. dalmata, almafa) (strstr).
    Vizsgáld meg, hogy szerepel-e a beolvasott szavak között a "cica" szó (strcmp).*/

    int szam1 = atoi(argv[1]);
    int szam2 = atoi(argv[2]);
    
    printf("5. feladat: %d \n", szam1 * szam2);

    printf("6. feladat: faktorialis(4) = %d", faktorialis(4));

    printf("7. feladat\n");

    printf("8. feladat: Add meg a neved!\n");
    hetedikFeladat();
    printf("Kesz !\n");


    return 0;
}

void hetedikFeladat()
{
    char nev[255];
    fgets(nev,255,stdin);
    /*size_t vege = getline(nev , 255, stdin);
    nev[vege] = '\0';*/

    FILE *fileToWrite = fopen("player.txt", "w+");
    fprintf(fileToWrite,nev);
}

int faktorialis(int a)
{
    if(a == 1)
    {
        return 1;
    }
    else
    {
        return a * faktorialis(a - 1);
    }
}

void fajlosDolgok()
{
    FILE *fp; //memóriacímet kell adni neki, honnan kezdve olvassa

    fp = fopen("/test.txt", "w+");
    /*
    r - read
    w - write
    a - write
    r+ - read + write
    w+ - törli a fájlt + write
    */

   char buff[255];
   fscanf(fp, "%s", buff);
   printf("1: %s", buff);

   fgets(buff, 255, (FILE *)fp);
   printf("1: %s", buff);

}