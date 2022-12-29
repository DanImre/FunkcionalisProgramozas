#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "beadando.h"

char **beolvas(char *fn, int c, int *size)
{
    char **x = malloc((*size)*sizeof(char*));
    if(x==NULL)
    {
        printf("Memory allocation failed!\n");
        return (char**)1;
    }
    if(c>1)
    {
        FILE *fp = fopen(fn,"r");
        if(fp == NULL) return NULL;
        int cntr = 0;
        char *buff = malloc(1024);
        while(1)
        {
            if(cntr >= *size)
            {
                char y[*size][1024];
                for (int i = 0; i < *size; i++)
                {
                    strcpy(y[i],x[i]);
                }
                *size = (*size)*2;
                x = realloc(x,(*size)*sizeof(char*));
                if(x == NULL) return (char**)1;
                for (int i = 0; i < (*size)/2; i++)
                {
                    strcpy(x[i],y[i]);
                }
            }
            if(fgets(buff,1024,fp)==NULL) break;
            x[cntr] = malloc(1024);
            int i = 0;
            while (buff[i] != '\n' && buff[i] != '\0')
            {
                x[cntr][i] = buff[i];
                i++;
            }
            x[cntr][i] = '\0';
            cntr++;
        }
        free(buff);
        *size = cntr;
        fclose(fp);
    }
    else
    {
        int cntr = 0;
        char *buff = malloc(1024);
        while(1)
        {
            if(cntr >= *size)
            {
                char y[*size][1024];
                for (int i = 0; i < *size; i++)
                {
                    strcpy(y[i],x[i]);
                }
                *size = (*size)*2;
                x = realloc(x,(*size)*sizeof(char*));
                if(x == NULL) return (char**)1;
                for (int i = 0; i < (*size)/2; i++)
                {
                    strcpy(x[i],y[i]);
                }
            }
            if(scanf("%s",buff)==EOF) break;
            x[cntr] = malloc(1024);
            strcpy(x[cntr],buff);
            cntr++;
        }
        free(buff);
        *size = cntr;
    }
    return x;
}

char **revall(char **x, int db)
{
    char **y = malloc(db*sizeof(char*));
    for (int i = 0; i < db; i++)
    {
        y[i] = malloc(strlen(x[db-1-i]));
        strcpy(y[i],strrev(x[db-1-i]));
    }
    return y;
}