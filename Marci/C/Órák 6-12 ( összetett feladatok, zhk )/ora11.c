#include <stdio.h>
#include <stdlib.h>

typedef struct box_struct
{
    int weight;
    struct box_struct *next;
}Box;

Box *top;

void init()
{
    top=NULL;
}

int isEmpty()
{
    if(top) return 0;
    else return 1;
}

void peek()
{
    if(isEmpty()) printf("A stack ures!\n");
    else printf("A legfelso doboz sulya: %d\nNext: %d\n", top->weight,top->next);
}

void push(int w)
{
    Box *new = malloc(sizeof(Box));
    new->weight=w;
    if(isEmpty())
    {
        new->next=new;
        top=new;
    }
    else
    {
        new->next=top;
    }
    top=new;
}

int main()
{
    init();
    push(2);
    peek();
    push(3);
    peek();
    return 0;
}