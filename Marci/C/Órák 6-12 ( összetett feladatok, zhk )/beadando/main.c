#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "beadando.h"

int main(int argc, char *argv[])
{
    if(argc > 1)
    {
        for (int i = 1; i <= argc-1; i++)
        {
            int size = 8;
            char **be = beolvas(argv[i],argc,&size);
            if(be == NULL)
            {
                printf("File opening unsuccessful!\n");
                continue;
            }
            if(be == (char**)1) return 1;
            char **ki = revall(be,size);
            for (int i = 0; i < size; i++)
            {
                printf("%d %s\n",size-i,ki[i]);
            }
            for (int i = 0; i < size; i++)
            {
                free(be[i]);
                free(ki[i]);
            }
            free(be);
            free(ki);
        }
    }
    else
    {
        int size = 8;
        char **be = beolvas("",argc,&size);
        if(be == (char**)1) return 1;
        char **ki = revall(be,size);
        for (int i = 0; i < size; i++)
        {
            printf("%d %s\n",size-i,ki[i]);
        }
        for (int i = 0; i < size; i++)
        {
            free(be[i]);
            free(ki[i]);
        }
        free(be);
        free(ki);
    }
    return 0;
}