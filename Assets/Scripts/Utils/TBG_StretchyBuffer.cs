using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBG_StretchyBuffer<T>
{
    private T[] array = new T[2];

    public short elements = 0;
    short allocated = 2;

    public void Add(T _e)
    {
        if (elements+1>allocated)
        {
            allocated *= 2;
            T[] _tmp = new T[allocated];
            for (int i = 0; i < elements; i++)
            {
                _tmp[i] = array[i];
            }
            array = _tmp;
        }
        array[elements] = _e;
        elements++;
    }

    public void PushFront(T _e)
    {
        if (elements + 1 > allocated)
        {
            elements++;
            allocated *= 2;
            T[] _tmp = new T[allocated];
            for (int i = 1; i < elements; i++)
            {
                _tmp[i] = array[i-1];
            }
            _tmp[0] = _e;
            array = _tmp;
        }
        else
        {
            elements++;
            T[] _tmp = new T[allocated];
            for (int i = 1; i < elements; i++)
            {
                _tmp[i] = array[i - 1];
            }
            _tmp[0] = _e;
            array = _tmp;
        }
    }

    public void RemoveAt(int _index)
    {
        if (_index >= elements) throw new System.Exception("Streachy buffer remove at out of range");
        for (int i = _index+1; i < elements; i++)
        {
            array[i - 1] = array[i];
        }
        elements--;
    }

    public bool Contains(T _item)
    {
        for (int i = 0; i < elements; i++)
        {
            if (array[i].Equals(_item)) return true;
        }
        return false;
    }

    public void Clear()
    {
        elements = 0;
    }

    public T this[int _index] 
    {
        get
        {
            if (_index >= elements) throw new System.Exception("Streachy buffer index out of range GET");
            else return array[_index];
        }
        set
        {
            if (_index >= elements) throw new System.Exception("Streachy buffer inder out of range SET");
            array[_index] = value;
        }
    }
}
