#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <string.h>

const int szamdb=100; //ennyi szamot generálunk
int n;

int beolvas(char *filenev, char *x[n])
{
    FILE *fp; //filepointer, egyertelműen a filera mutat
    fp = fopen(filenev, "r"); // filenyitás
    char buff[20];
    int db=0;
    int i = 0;
    while (fgets(buff, 20, fp) != NULL) //addig megy amíg a sorban van adat
    {
        db++;
    }
    fclose(fp); //zárás
    fp = fopen(filenev, "r");
    for (int i = 0; i < db; i++)
    {
        x[i] = malloc(20);
        fgets(x[i], 20, fp);
        printf("%s",x[i]);
    }
    fclose(fp);
    printf("----------\nbeolvastal\n----------\n");
    return db;
}

void longerthan(char *x[n], int hossz)
{
    printf("Az %d-nel hosszabb szavak:\n",hossz);
    for (int i = 0; i < n; i++)
    {
        printf("benn\n");
        printf("%s",x[i]);
    }
}

void hasChar(char *x[n], char c)
{
    printf("Az %c-t tartalmazo szavak:\n",c);
    for (int i = 0; i < n; i++)
    {
        if(strchr(x[i],c)) printf("%s",x[i]); //stringben chart keres
    }
}

void contains(char *x[n], char szo[])
{
    printf("Az %s-t tartalmazo szavak:\n",szo);
    for (int i = 0; i < n; i++)
    {
        if(strstr(x[i],szo)) printf("%s",x[i]); //stringben stringet keres
    }
}

int exists(char *x[n], char szo[]) //bool tipus sincs, 0 -> true, minden más false
{
    for (int i = 0; i < n; i++)
    {
        if(strcmp(x[i],szo)) return 0;
    }
    return 1;
}

int commandLineSzorzas(int a, int b)
{
    int s=0;
    for (int i = 0; i < b; i++)
    {
        s+=a;
    }
    return s;
}

int fact(int a)
{
   if(a==0) return 1;
   return(a*fact(a-1));
}

void kiir(char filenev[], char szo[])
{
    FILE *fp=fopen(filenev,"w+");
    fprintf(fp, "%s",szo);
    fclose(fp);
}

void intkiir(char filenev[], int x)
{
    FILE *fp=fopen(filenev,"a"); // mivel a, hozzáfűzi a már létező filehoz
    fprintf(fp, "%d\n",x);
    fclose(fp);
}

void nullaz(char filenev[]) // igazából felülírja az előző filet
{
    FILE *fp=fopen(filenev,"w+");
    fclose(fp);
}

int parosok(int szamok[])
{
    srand(time(NULL));
    printf("Hozzafuzzuk az elozo filehoz? (I/N): ");
    char valasz;
    scanf("%c", &valasz);
    if(valasz == 'N') nullaz("even_numbers.txt");
    int db = 0;
    for (int i = 0; i < szamdb; i++)
    {
        szamok[i]=rand() % 100;
        if(szamok[i]%2 == 0 && szamok[i] != 0) intkiir("even_numbers.txt",szamok[i]);
        db++;
    }
}

int parosSum(char file[], int db)
{
    FILE *fp=fopen(file, "r");
    int s=0;
    for (int i = 0; i < db; i++)
    {
        int x = 0;
        fscanf(fp, "%d", &x);
        s+=x;
    }
    fclose(fp);
    return s;
}

void LIWE ()
{
    int x;
    char uzenet[255];
    printf("Uzenet tipusa: ");
    scanf("%d",&x);
    printf("Uzenet: ");
    scanf("%s",uzenet);
    switch (x)
    {
    case 0:
        printf("INFO - %s", uzenet);
        break;
    case 1:
        printf("WARNING - %s", uzenet);
        break;
    case 2:
        printf("ERROR - %s", uzenet);
        break;
    default:
        printf("LOG - %s", uzenet);
        break;
    }
}

int main(int argc, char *argv[])
{
    char *nev = "ora6.txt";
    char *szavak[n];
    n = beolvas(nev,szavak);
    longerthan(szavak,5);
    hasChar(szavak,'x');
    contains(szavak,"ci");
    if(exists(szavak,"hiccup")==0) printf("Van ilyen szo\n");
    else printf("Nincs ilyen szo\n");
    printf("%d\n",commandLineSzorzas(atoi(argv[1]),atoi(argv[2]))); // 2. és 3. parancssori argumentumot használja, mert a program neve is argumentum
    printf("%d faktorialis: %d\n",10,fact(10));
    kiir("player.txt","Papp Márton");
    int szamok[szamdb];
    int parosdb = parosok(szamok);
    printf("Az even_numbers.txt-ben a szamok osszege: %d\n",parosSum(argv[3],parosdb));
    LIWE();
    for (int i = 0; i < n; i++)
    {
        free(szavak[i]);
    }
    return 0;
}
