using System;
using System.Threading;

class Program
{
    static void Main()
    {

    }
}

class Op
{
    int value;

    public int Value
    {
        get
        {
            return value;
        }
    }

    public void Increment()
    {
        //value++;
        Interlocked.Increment(ref value);
    }

    public void Set(int newValue)
    {
        //this.value = newValue;
        Interlocked.Exchange(ref value, newValue);
    }

    public void CompareAndChange(int compare, int newValue)
    {
        //if (value == compare)
        //{
        //    value = newValue;
        //}

        Interlocked.CompareExchange(ref value, newValue, compare);
    }
}