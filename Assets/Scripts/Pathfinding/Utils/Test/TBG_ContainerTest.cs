using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TBG_ContainerTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //MapVsStrechy();
        //CreateVsClear();
        //ContainerTest();
    }

    void CreateVsClear()
    {

        System.Diagnostics.Stopwatch _clock = new System.Diagnostics.Stopwatch();
        _clock.Start();

        List<DJ_Node> _test1 = new List<DJ_Node>();
        _clock.Stop();

        TimeSpan ts = _clock.Elapsed;

        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);

        Debug.Log($"{ts} -- Streachy buffer time");


        _clock = new System.Diagnostics.Stopwatch();
        _clock.Start();
        _test1.Clear();
        _clock.Stop();

        ts = _clock.Elapsed;

        elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);

        Debug.Log($"{ts} -- Dictionarry time");
    }

    void MapVsStrechy()
    {
        System.Diagnostics.Stopwatch _clock = new System.Diagnostics.Stopwatch();
        _clock.Start();
        List<DJ_Node> _nodes = new List<DJ_Node>();
        DJ_Node _test = gameObject.AddComponent<DJ_Node>();
        _test.Init(Vector3.zero, null, 10);
        _nodes.Add(_test);
        _nodes.Add(gameObject.AddComponent<DJ_Node>());
        if (_nodes.Contains(_test))
        {

        }
        _clock.Stop();

        TimeSpan ts = _clock.Elapsed;

        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);

        Debug.Log($"{ts} -- Streachy buffer time");


        _clock = new System.Diagnostics.Stopwatch();
        _clock.Start();
        Dictionary<int, DJ_Node> _mapNode = new Dictionary<int, DJ_Node>();
        _test = gameObject.AddComponent<DJ_Node>();
        _test.Init(Vector3.zero, null, 10);
        _mapNode.Add(0, _test);
        _mapNode.Add(1, gameObject.AddComponent<DJ_Node>());
        if (_mapNode.ContainsKey(_test.Id))
        {

        }
        _clock.Stop();

        ts = _clock.Elapsed;

        elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);

        Debug.Log($"{ts} -- Dictionarry time");
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
        _clock.Stop();

        TimeSpan ts = _clock.Elapsed;

        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);

        Debug.Log($"{ts} -- Streachy buffer time");

        _clock = new System.Diagnostics.Stopwatch();
        _clock.Start();
        List<int> _list = new List<int>();
        _list.Add(1);
        _list.Add(2);
        _list.Add(3);
        _list.Add(4);
        _list.Add(5);
        _list.RemoveAt(4);
        for (int i = 0; i < _list.Count; i++)
        {
            _test += _list[i];
        }
        _clock.Stop();

        ts = _clock.Elapsed;

        elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);

        Debug.Log($"{ts} -- List time");
    }

}
