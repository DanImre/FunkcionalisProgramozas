#include<stdio.h>
#include<time.h>
#include<stdlib.h>
#include <math.h>

int main()
{
    int arr1[] = {1,2,3};
    int arr2[3] = {1,2,3}; //ugyanaz
    //int arr3[2] = {1,2,3}; //yields warning
    int arr4[3];
    arr4[0] = 1;
    arr4[1] = 2;
    arr4[2] = 3; //ugyanazmint arr1,arr2
/*
    int arr5[3];
    arr4[3] = 17;
    arr4[4] = 18;
    arr4[5] = 19;*/ //lehetséges, de ezek random memóriacímek

    printf("%d",elemek(arr2));
}

int szor (a)
{
    return -1*a;
}

void toltsdFelNullakkal(int tomb[])
{
    for (size_t i = 0; i < sizeof(tomb)/sizeof(tomb[0]) + 1; i++)
    {
        tomb[i] = 0;
    }
}

int elemek(int tomb[])
{
    int sum = 0;
    for (size_t i = 0; i < sizeof(tomb)/sizeof(tomb[0]) + 1; i++)
    {
        sum += tomb[i];
    }
    return sum;
}

int elemekSullyal(int tomb[], int sulyok[])
{
    int sum = 0;
    for (size_t i = 0; i < sizeof(tomb)/sizeof(tomb[0]) + 1; i++)
    {
        sum += tomb[i] * sulyok[i];
    }
    return sum;
}

int elemekLebegopontosSullyal(int tomb[], float sulyok[])
{
    float sum = 0;
    for (size_t i = 0; i < sizeof(tomb)/sizeof(tomb[0]) + 1; i++)
    {
        sum += tomb[i] * sulyok[i];
    }
    return sum;
}

int maximum(int tomb[])
{
    int max = tomb[0];
    for (size_t i = 1; i < sizeof(tomb)/sizeof(tomb[0]) + 1; i++)
    {
        if(max < tomb[i])
        {
            max = tomb[i];
        }
    }
    return max;
}

int secondMinimum(int tomb[])
{
    int min1 = 0;
    int min2 = 0;
    if(tomb[0] < tomb[1])
    {
        min1 = tomb[0];
        min2 = tomb[1];
    }
    else
    {
        min1 = tomb[1];
        min2 = tomb[0];
    }
    for (size_t i = 2; i < sizeof(tomb)/sizeof(tomb[0]) + 1; i++)
    {
        if(min1 > tomb[i])
        {
            min1 = tomb[i];
        }
        if(min2 > tomb[i] && tomb[i] != min1)
        {
            min2 = tomb[i];
        }
    }
    return min2;
}

int stringszamolo()
{
    printf("Hello, irj valamit! (Maximum 1000 karakter)\n");
    char string[1000];
    gets(string);
    printf("%d",sizeof(string)/sizeof(string[0]));
}

int melyikVanElorebb(char string1[],char string2[])
{
    if(strcmp(string1,string2)>0)
    {
        return 1;
    }
    
    return 0;
}

//4 3 25