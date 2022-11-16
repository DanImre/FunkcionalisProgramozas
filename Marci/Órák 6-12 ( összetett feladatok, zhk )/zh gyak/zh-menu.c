#include <stdio.h>
#include <stdlib.h>
#include <time.h>

static double prices[5]={80.5, 95.9, 82.0, 100.2, 101.3}; // árak

int dailyDiscountMenu() // randomizál egy sorszámot, hogy melyik legyek az olcsobb
{
    int x = rand() % 5;
    return x;
}

double modifiedPrice(int sorsz, float m) //áfát hozzáad
{
    return prices[sorsz]*m;
}

void generateReview(int x[])//random review-kat ad 1-10
{
    for (int i = 0; i < 5; i++)
    {
        x[i] = (rand() % 10) + 1;
    }
}

void printMenuData(double ar[], double mar[], int rev[]) //kiirja egy menü adatait
{
    for (int i = 0; i < 5; i++)
    {
        printf("Menu #%d\nNet price: %.2f\nGross price: %.2f\nCustomer review: %d\n",i+1,ar[i],mar[i],rev[i]);
    }
}

void printDailyData(double ar[], double mar[], int rev[], int rend[], int count)//összegzés
{
    double sn, gn, avgr;
    sn=0;
    gn=0;
    avgr=0;
    for (int i = 0; i < count; i++)
    {
        sn+=ar[rend[i]-1];
        gn+=mar[rend[i]-1];
    }
    for (int i = 0; i < 5; i++)
    {
        avgr+=rev[i];
    }
    avgr = avgr/5;
    printf("Number of daily orders: %d\nDaily net income: %.2f\nDaily gross income: %.2f\nDaily average review: %.2f\n",count,sn,gn,avgr);
    if(avgr<3.5) printf("Restaurant did poorly today.\n");
    else if(avgr<6.5) printf("Restaurant did fine today.\n");
    else printf("Restaurant did great today.\n");
}

int main()
{
    srand(time(NULL));
    int rendelesek[] = {1, 3, 4, 2, 2, 1, 5, 4, 4, 3, 5};
    int disc = dailyDiscountMenu();
    double modprices[5];
    for (int i = 0; i < 5; i++)
    {
        if(i==disc) modprices[i] = modifiedPrice(i,0.8*1.27);
        else modprices[i] = modifiedPrice(i,1.27);
    }
    int reviews[5];
    generateReview(reviews);
    printMenuData(prices,modprices,reviews);
    printDailyData(prices,modprices,reviews,rendelesek,sizeof(rendelesek)/sizeof(int));
    return 0;
}