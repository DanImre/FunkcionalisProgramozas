#include <stdio.h>
#include <time.h>
#include <stdlib.h>
#include <math.h>
#include <ctype.h>
int *lokalis();
int doga();

int main()
{
    //doga();
    
    int i = 10;
    int *j = &i;
    *j = 20;
    printf("%d \n",i);

    //4.
    int *z = j;

    *z = 30;
    printf("%d \n",i);

    //9
    printf("%d\n",*lokalis());

    printf("Cime : %d\n",j);
    printf("Ertek : %d\n",*j);

    int arr[3] = {1,2,3};
    printf("Arr cime : %d\n",arr);
    printf("Arr ertek : %d\n",*arr);

}

int *lokalis()
{
    int a = 40;
    return &a;
}

int doga()
{
    char string[20];

    scanf("%s",&string);


    int kulonbseg = (int)'a' - (int)'A';

    for (size_t i = 0; i < sizeof(string)/sizeof(string[0]); i++)
    {
        if(string[i] == ' ')
        {
            string[i] = '\n';
        }
        if(isdigit(string[i]))
        {
            string[i] = ((int)string[i] - 1);
        }
        else
        {
            if((int)string[i] < 'a') //kisbetű
            {
                string[i] += kulonbseg;
            }
            else //nagybetű
            {
                string[i] -= kulonbseg;
            }
        }
        printf("%c",string[i]);
    }
}