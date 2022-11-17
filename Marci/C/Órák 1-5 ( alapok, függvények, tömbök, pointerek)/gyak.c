#include <stdio.h>

int main()
{
    int f=0;
    printf("Fahrenheit fok: \n");
    scanf("%d",&f);
    printf("Celsius-ban: %f\n",(float)(f-32)/1.8);
    for (int i = -20; i < 201; i+=10)
    {
        printf("Fahrenheit fok: %d\tCelsius-ban: %f\n",i,(float)(i-32)/1.8);
    }

    int a=0;
    printf("Szam: \n");
    scanf("%d",&a);
    printf("Osztok: ");
    for (int i = 1; i < a+1; i++)
    {
        if(a % i == 0) printf("%d\n",i);
    }

    printf("Valos szamok (0-1): \n");
    for (float i = 0; i <= 1; i+=0.1)
    {
        printf("%.1f\t",i);
    }
    
    
    return 0;
}