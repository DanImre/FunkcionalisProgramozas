#include <stdio.h>

void swap(int *i, int *j) // még mindig nem müködik
{
    int *x = i;
    i = j;
    j = x;
}

int szum(int x[], int hossz) // normális sum
{
    int s=0;
    for (int i = 0; i < hossz; i++)
    {
        s+=x[i];
    }
    return s;
}

int szum2(int *ptr, int hossz) // a tömbre az első elemre mutató pointerrel hivatkozok
{
    int s=0;
    for (int i = 0; i < hossz; i++)
    {
        s+=*ptr;
        ptr++; // növeli a tömbindexet, mert egymás után vannak az elemek a memóriában
    }
    return s;
}

int szum3(int *kptr, int *vptr) // hossz nélkül, mert a vptr a tömb után mutat
{
    int s=0;
    while (kptr < vptr)
    {
        s+=*kptr;
        kptr++;
    }
    return s;
}

double avg(int *kptr, int *vptr)
{
    double a=0;
    int hossz=0;
    while (kptr < vptr)
    {
        a+=(double)*kptr;
        kptr++;
        hossz++;
    }
    return a/hossz;
}

double avg2(int x[], int hossz)
{
    return (double)szum(x,hossz)/hossz; // kell a cast, mert a szum intet ad
}

void lesserIndex(int x[])
{
    printf("Az egyik index a tombben (>0): ");
    int *ptr, *ptr2, a, b;
    scanf("%d",&a);
    ptr=&x[a-1];
    printf("Az masik index a tombben (>0): ");
    scanf("%d",&b);
    ptr2=&x[b-1];
    if(ptr==ptr2) printf("Ugyanoda mutatnak\n");
    else if(ptr<ptr2) printf("Az elso pointer mutat a kisebb indexre\n");
    else printf("Az masodik pointer mutat a kisebb indexre\n");
}

int *maxPointer(int x[],int hossz)
{
    int *ptr = x;
    for (int i = 1; i < hossz; i++)
    {
        if(x[i]>*ptr) ptr = &x[i];
    }
    return ptr;
}

int main()
{
    int i = 123;
    int *ptr = &i; //&i megaja i memóriacímét
    printf("%d\n",*ptr); //* nélkül a memóriacímet adná vissza
    *ptr = 246;
    printf("%d\n",*ptr);
    int j = 100;
    int *ptr2 = &j;
    printf("Csere elott: %d %d\n",i,j);
    swap(ptr,ptr2);
    printf("Csere utan: %d %d\n",i,j);
    int **pptr = &ptr;
    **pptr = 101;
    printf("A valtoztatas utan: %d\n",**pptr);
    int x[10] = {1,2,3,4,5,6,7,8,9,10};
    ptr=&x[0];
    ptr2=&x[10];
    printf("Szumma: %d es %d es %d\n",szum(x,10),szum2(ptr,10),szum3(ptr,ptr2));
    printf("Atlag: %.2f es %.2f\n",avg2(x,10),avg(ptr,ptr2));
    lesserIndex(x);
    printf("Max. memoriacime: %p\tMax. erteke: %d\n",maxPointer(x,10),*maxPointer(x,10));
    printf("---a tomb felere---\n");
    printf("Max. memoriacime: %p\tMax. erteke: %d\n",maxPointer(x,5),*maxPointer(x,5));
    return 0;
}