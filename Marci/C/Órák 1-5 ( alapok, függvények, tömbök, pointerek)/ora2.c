#include<stdio.h>

//ezek a feladatok azért még elég egyértelműek :)
int main()
{
    int x;
    printf("Kezdoertek: %d\n",x);
    printf("Uj kezdoertek: \n");
    scanf("%d",&x);
    //printf("%d",x);
    if(x % 2 == 0)
        printf("Paros\n");
    else
        printf("Paratlan\n");
    if(x > 0)
        printf("Pozitiv\n");
    else if( x == 0 )
        printf("Nulla\n");
    else
        printf("Negativ\n");
    x=3.14;
    printf("%f\n",x);
    x='L';
    printf("%c\n",x);
    x=(2==2);
    printf("nem lehet kiirni\n");
    int lb=sizeof(unsigned long long int);
    printf("%d byte\n",lb);
    lb=lb*8;
    int l=1;
    int i=0;
    while(i<lb)
    {
        l=l*2;
        i++;
    }
    printf("%d\n",l);
    int a,b;
    printf("A ket szam: \n");
    scanf("%d %d",&a, &b);
    printf("%f",(float)(a+b)/2);

}