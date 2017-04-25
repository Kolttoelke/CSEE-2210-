using System.Collections;
using System;
using UnityEngine;

public class Heap<T> where T: IHeaps<T> {

    T[] bits;
    int bitCount;

    

    public int Count
    {
        get
        {
            return bitCount;
        }
    }

    public T Remove()
    {
        T firstBit = bits[0];
        bitCount--;
        bits[0] = bits[bitCount];
        bits[0].Heaps = 0;
        SortDown(bits[0]);
        return firstBit;
    }
    public void Update(T bit)
    {
        SortUp(bit);
    }
   

    public bool Contains(T bit)
    {
        return Equals(bits[bit.Heaps], bit);
    }
    void SortUp(T bit)
    {
        int masterIndex = (bit.Heaps - 1) / 2;

        while (true)
        {
            T masterBit = bits[masterIndex];
            if (bit.CompareTo(masterBit) > 0)
            {
                Swap(bit, masterBit);
            }
            else
            {
                break;
            }
            masterIndex = (bit.Heaps - 1) / 2;
        }
    }
    public Heap(int maxHeap)
    {
        bits = new T[maxHeap];
    }
    void SortDown(T bit)
    {
        while (true)
        {
            int slaveIndexLeft = bit.Heaps * 2 + 1;
            int slaveIndexRight = bit.Heaps * 2 + 2;
            int swap = 0;

            if(slaveIndexLeft < bitCount)
            {
                swap = slaveIndexLeft;
                if (slaveIndexRight < bitCount)
                {
                    if (bits[slaveIndexLeft].CompareTo(bits[slaveIndexRight]) < 0)
                    {
                        swap = slaveIndexRight;
                    }
                }
                if (bit.CompareTo(bits[swap])< 0)
                {
                    Swap(bit, bits[swap]);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
            
        }

    }
    void Swap(T bitA, T bitB)
    {
        bits[bitA.Heaps] = bitB;
        bits[bitB.Heaps] = bitA;
        int bitAIndex = bitA.Heaps;
        bitA.Heaps = bitB.Heaps;
        bitB.Heaps = bitAIndex;
    }

    
    
    public void Add(T bit)
    {
        bit.Heaps = bitCount;
        bits[bitCount] = bit;
        SortUp(bit);
        bitCount++;
    }
}

public interface IHeaps<T> : IComparable<T> {
    int Heaps
    {
        get;
        set;
    }
}