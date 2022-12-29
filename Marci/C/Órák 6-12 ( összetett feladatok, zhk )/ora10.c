#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <string.h>

int n = 30; //hallgatók zsáma

typedef enum tipus //hallgatók típusa
{
    Bsc,
    Msc,
    PhD
} Type;

typedef struct PhD_extra //ha egy hallgató PhD, akkor ez van a 21.sorban
{
    double pub;
    int erdosszam;
} Px;

typedef union xtra //hallgató típusától függ
{
    int osszkur;
    double kredind;
    Px *impfakt;
} extra;

typedef struct Stdnt //egy hallgató adatai
{
    int id;
    double avg;
    int age;
    extra x;
} Student;

Student *student_init(Type x, int i) //felvesz egy hallgatót, és egy rá mutató pointert ad vissza
{
    Student *sptr = malloc(sizeof(Student));
    //típustól függően beállítjuk az adatokat random
    if (x == Bsc)
    {
        sptr->id = i; //-> operátor olyan mint a . csak pointerekre
        sptr->avg = ((double)(rand() % 400) / 100) + 1;
        sptr->age = (rand() % 20) + 20;
        sptr->x.osszkur = (rand() % 30) + 1;//unionnak egy mezőjére is .-tal hivatkozunk
    }
    if (x == Msc)
    {
        sptr->id = i;
        sptr->avg = ((double)(rand() % 400) / 100) + 1;
        sptr->age = (rand() % 20) + 20;
        sptr->x.kredind = ((double)(rand() % 500) / 100) + 1;
    }
    if (x == PhD)
    {
        sptr->id = i;
        sptr->avg = ((double)(rand() % 400) / 100) + 1;
        sptr->age = (rand() % 20) + 20;
        sptr->x.impfakt = malloc(sizeof(Px));
        sptr->x.impfakt->pub = ((double)(rand() % 900) / 100) + 1; //ha az unió azon helye pointer akkor arra is ->
        sptr->x.impfakt->erdosszam = (rand() % 30) + 1;
    }
    return sptr;
}

void stdnt_info(Student *x, Type t) //kiirja a hallgatók infoit
{
    if (t == Bsc)
    {
        printf("Id: %d\nAvg: %.2f\nAge: %d\nCourses: %d db\n",x->id,x->avg,x->age,x->x.osszkur);
    }
    if (t == Msc)
    {
        printf("Id: %d\nAvg: %.2f\nAge: %d\nCredit index: %.2f\n",x->id,x->avg,x->age,x->x.kredind);
    }
    if (t == PhD)
    {
        printf("Id: %d\nAvg: %.2f\nAge: %d\nPublication: %.2f\nErdos-number: %d\n",x->id,x->avg,x->age,x->x.impfakt->pub,x->x.impfakt->erdosszam);
    }
}

Student *MaxAvg(Student *x[]) // visszaadja a legjobb átlakú tanulót
{
    int max=0;
    for (int i = 1; i < n; i++)
    {
        if(x[max]->avg < x[i]->avg) max=i;
    }
    return x[max];
}

int main()
{
    srand(time(NULL));
    Student *hallgatok[n]; //hallgatók tömbje
    Type t[n]; //hallgatók típusainak tömbje
    for (int i = 0; i < n; i++)
    {
        t[i]=i%3; //0-2 közt ad vissza számot, az enum miatt azokból Bsc, Msc vagy PhD lesz
        hallgatok[i] = student_init(t[i], i + 1);
    }
    for (int i = 0; i < n; i++) //kiirja az összes hallgató adatát, igazából csak teszt volt
    {
        stdnt_info(hallgatok[i],t[i]);
    }
    int maxid = (MaxAvg(hallgatok)->id)-1;
    printf("Studrnt with highest AVG:\n");
    stdnt_info(hallgatok[maxid],t[maxid]);
    for (int i = 0; i < n; i++) //minden hallgatót freeelünk
    {
        if(t[i]==PhD) free(hallgatok[i]->x.impfakt); //a PhD-s hallgatók extra mezőjét is kell freeelni
        free(hallgatok[i]);
    }
    return 0;
}