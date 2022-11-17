#include <stdio.h>

int main()
{
    int n,m;
    printf("N: \n");
    scanf("%d",&n);
    printf("M: \n");
    scanf("%d",&m);
    if(n<=m)
    {
        for (int i = n; i <= m; i++)
        {
            if(i%2==0) printf("%d ",i);
        }
        printf("\n");
        int j=m;
        while (j>=n)
        {
            if(j%7==0) printf("%d ",j--);
            else j--;
        }
    }
    return 0;
}