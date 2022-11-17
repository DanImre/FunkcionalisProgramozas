#include <memory.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>
#include <ctype.h>
#include <math.h>

#define CNT 3

void elso()
{
    printf("Kerek [hossz]<20 szöveget:\n");
    char temp[20];
    scanf("%s",temp);

    int *solution = malloc(sizeof(char) * strlen(temp));
    for (size_t i = 0; i < strlen(temp); i++)
    {
        solution[i] = temp[i];
    }
}

void reverse(char string[])
{
    int i = 0;
    int j = strlen(string)-1;
    while (i < j)
    {
        char temp = string[i];
        string[i] = string[j];
        string[j] = temp;
        ++i;
        --j;
    }
    
}

int main()
{
    char alma[] = "alma";
    reverse(alma);
    printf("%s", alma);
}