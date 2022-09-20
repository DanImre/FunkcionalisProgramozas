
#include <stdio.h>
#include <math.h>

int main()
{
	/*
	signed:

	%c = char
	%hi = short
	%i = int
	%li = long (int) || long long (int)
	
	unsigned:

	%c = char
	%hu = short
	%u = int
	%lu = long (int) || long long (int)
	*/

	//integer literals:
	//nyolcas számrendszer a 0 val kezdőd számok pl: 012 = 10
	//Hexadecimális : 0x el kezdődik pl 0x45 = 69
	
	//floats
	//double + float
	//sizeof()

	//void

	//char string[] = "hello";

	//Feladatok:

	printf("1. feladat: ");
	int a;
	printf("%d\n",a);

	printf("2. feladat: ");
	scanf("%d", &a);
	if(a%2 == 0)
		printf("Paros\n");
	else
		printf("Paratlan\n");
	

	printf("3. feladat: ");
	if(a < 0)
		printf("Negatív\n");
	else if(a > 0)
		printf("Pozitiv\n");
	else
		printf("Nulla\n");

	printf("4. feladat: ");
	
	int b = -1;
	printf("Int <- char : ");
	b = "a";
	printf(b);
	printf("\t");

	printf("Int <- bool : ");
	//b = (b == 'c');
	//printf((0 == 0));
	printf(" nem tudja \t");

	printf("Int <- string : ");
	b = "hello";
	printf(b);
	printf("\n");

	printf("6. feladat: ");

	printf("Size of the largest number : ");
	b = sizeof(unsigned long long int);
	printf("%d to the power of 2",b);
	printf("\n");

	unsigned long long int c = 1;
	for (size_t i = 0; i < b*b-2; i++)
	{
		c = c*2;
	}
	
	printf("Hozzaadok egyet : %lu", c);
}
