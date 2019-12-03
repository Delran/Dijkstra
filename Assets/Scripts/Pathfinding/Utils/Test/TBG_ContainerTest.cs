using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TBG_ContainerTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ContainerTest();
    }

    void ContainerTest()
    {
        System.Diagnostics.Stopwatch _clock = new System.Diagnostics.Stopwatch();
        _clock.Start();
        TBG_StretchyBuffer<int> _buffer = new TBG_StretchyBuffer<int>();
        _buffer.Add(1);
        _buffer.Add(2);
        _buffer.Add(3);
        _buffer.Add(4);
        _buffer.Add(5);
        _buffer.RemoveAt(4);
        int _test = 0;
        for (int i = 0; i < _buffer.elements; i++)
        {
            _test += _buffer[i];
        }
        Debug.Log(_test);
        _clock.Stop();

        TimeSpan ts = _clock.Elapsed;

        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);

        Debug.Log($"{ts} -- Streachy buffer time");

        _clock.Start();
        List<int> _list = new List<int>();
        _list.Add(1);
        _list.Add(2);
        _list.Add(3);
        _list.Add(4);
        _list.Add(5);
        _list.RemoveAt(4);
        _test = 0;
        for (int i = 0; i < _list.Count; i++)
        {
            _test += _list[i];
        }
        Debug.Log(_test);
        _clock.Stop();

        ts = _clock.Elapsed;

        elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);

        Debug.Log($"{ts} -- List time");
    }

}
