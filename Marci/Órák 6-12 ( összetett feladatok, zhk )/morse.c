#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <ctype.h>

int n;
const int maxsorhossz = 600;

int elemszam(char filenev[])
{
    FILE *fp;
    fp = fopen(filenev, "r");
    char buff[255];
    int db=0;
    while (fgets(buff, 255, fp) != NULL)
    {
        db++;
    }
    fclose(fp);
    return db;
}

void toMorse(char x[n][maxsorhossz], char file[])
{
    FILE *fp;
    fp = fopen(file, "w");
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < strlen(x[i]); j++)
        {
            switch (tolower(x[i][j]))
            {
            case 'a':
                fprintf(fp,"%s",".-");
                break;;
            case 'b':
                fprintf(fp,"%s","-...");
                break;;
            case 'c':
                fprintf(fp,"%s","-.-.");
                break;
            case 'd':
                fprintf(fp,"%s","-..");
                break;
            case 'e':
                fprintf(fp,"%s",".");
                break;
            case 'f':
                fprintf(fp,"%s","..-.");
                break;
            case 'g':
                fprintf(fp,"%s","--.");
                break;
            case 'h':
                fprintf(fp,"%s","....");
                break;
            case 'i':
                fprintf(fp,"%s","..");
                break;
            case 'j':
                fprintf(fp,"%s",".---");
                break;
            case 'k':
                fprintf(fp,"%s","-.-");
                break;   
            case 'l':
                fprintf(fp,"%s",".-..");
                break;    
            case 'm':
                fprintf(fp,"%s","--");
                break;    
            case 'n':
                fprintf(fp,"%s","-.");
                break; 
            case 'o':
                fprintf(fp,"%s","---");
                break;  
            case 'p':
                fprintf(fp,"%s",".--.");
                break;
            case 'q':
                fprintf(fp,"%s","--.-");
                break;   
            case 'r':
                fprintf(fp,"%s",".-.");
                break;   
            case 's':
                fprintf(fp,"%s","...");
                break;  
            case 't':
                fprintf(fp,"%s","-");
                break;    
            case 'u':
                fprintf(fp,"%s","..-");
                break;  
            case 'v':
                fprintf(fp,"%s","...-");
                break;    
            case 'w':
                fprintf(fp,"%s",".--");
                break;  
            case 'x':
                fprintf(fp,"%s","-..-");
                break;  
            case 'y':
                fprintf(fp,"%s","-.--");
                break;  
            case 'z':
                fprintf(fp,"%s","--..");
                break;   
            case '0':
                fprintf(fp,"%s","-----");
                break;  
            case '1':
                fprintf(fp,"%s",".----");
                break;    
            case '2':
                fprintf(fp,"%s","..---");
                break;
            case '3':
                fprintf(fp,"%s","...--");   
                break;    
            case '4':
                fprintf(fp,"%s","....-");   
                break;     
            case '5':
                fprintf(fp,"%s",".....");   
                break;    
            case '6':
                fprintf(fp,"%s","-....");  
                break;  
            case '7':
                fprintf(fp,"%s","--...");   
                break;  
            case '8':
                fprintf(fp,"%s","---..");
                break;  
            case '9':
                fprintf(fp,"%s","----.");   
                break;    
            case '.':
                fprintf(fp,"%s"," |");   
                break;                                                                                                                                                                                                                                                                                                                   
            default:
                fprintf(fp,"%c",x[i][j]);
            }
        }
    }
    fclose(fp);
}

int main(int argc, char *argv[])
{
    FILE *fp = fopen(argv[1], "r");
    n = elemszam(argv[1]);
    char szavak[n][maxsorhossz];
    for (int i = 0; i < n; i++)
    {
        fgets(szavak[i], maxsorhossz, fp);
    }
    fclose(fp);
    toMorse(szavak,argv[2]);
    return 0;
}