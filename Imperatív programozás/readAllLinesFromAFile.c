#include <stdio.h>
#include <time.h>
#include <stdlib.h>
#include <math.h>
#include <ctype.h>
#include <string.h>

char *beolvas(char filenev[]);
void beolvasas(char result[255][255], int db, char filename[255]);

int main(int argc, char *argv[])
{
    printf("Mi a fajl neve amit be szeretnel olvasni ?\n");
    char filename[255];
    scanf("%255[^\n]",filename); //fgets csak bemenetre
    printf("%s",filename);

    char dbBuffer[255];
    int db = 0;
    FILE *fp;
    fp = fopen(filename, "r");
    while (fgets(dbBuffer, 255, fp) != NULL)
    {
        db++;
    }
    fclose(fp);

    char megoldasTomb[255][255]; //Ebben lesz elérhető a megoldás.
    megoldasTomb[db][0] = 'E';
    megoldasTomb[db][1] = 'N';
    megoldasTomb[db][2] = 'D';
    //majd ha megtanulunk dinamikusan memórát kezelni akkor lehet majd db méretű, most csak a (db + 1). helyet beállítom END re és akkor lehet arra hovatkozni
    
    beolvasas(megoldasTomb, db, filename);

    printf("\n");
    printf("Fajl tartalma:\n");
    for (size_t i = 0; i < db; i++)
    {
        printf("%s",megoldasTomb[i]);
    }
    return 0;
}

void beolvasas(char result[255][255], int db, char filename[255])
{
    FILE *fp;
    fp = fopen(filename, "r");
    for (int i = 0; i < db; i++)
    {
        fgets(result[i], 255, fp);
    }
    fclose(fp);
}