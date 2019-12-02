using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DJ_Dijkstra 
{
    TBG_StretchyBuffer<DJ_Node> vertices = null, path = new TBG_StretchyBuffer<DJ_Node>();
    TBG_StretchyBuffer<int> distance = new TBG_StretchyBuffer<int>(), deleted = new TBG_StretchyBuffer<int>();

    public Dictionary<int, DJ_Node> previous = new Dictionary<int, DJ_Node>();
    public Dictionary<int, DJ_Node> list = new Dictionary<int, DJ_Node>();

    public TBG_StretchyBuffer<DJ_Node> Path => path;

    public DJ_Dijkstra(TBG_StretchyBuffer<DJ_Node> _buffer)
    {
        vertices = _buffer;

    }

    public void ChangeMap(TBG_StretchyBuffer<DJ_Node> _buffer)
    {
        vertices = _buffer;
    }

    public void Reset()
    {
        previous.Clear();
        list.Clear();
        distance.Clear();
        deleted.Clear();
        path.Clear();
    }

    int FindMin()
    {
        int _min = int.MaxValue;
        int _vertex = -1;

        foreach (var item in list)
        {
            int _id = item.Value.Id;
            int _nodeDist = distance[_id];
            if (_nodeDist < _min)
            {
                _min = _nodeDist;
                //Debug.Log("BONJOUR");
                _vertex = _id;
            }
        }
        /*for (int i = 0; i < list.Count; i++)
        {
            int _nodeDist = distance[i];
            if (_nodeDist < _min)
            {
                _min = _nodeDist;
                //Debug.Log("BONJOUR");
                _vertex = i;
            }
        }*/
        return _vertex;
    }

    public void Compute(int _id, int _target)
    {
        previous.Clear();
        list.Clear();
        distance.Clear();
        deleted.Clear();
        path.Clear();

        for (int i = 0; i < vertices.elements; i++)
        {
            distance.Add(int.MaxValue);
            previous.Add(i, null);
            list.Add(i, vertices[i]);
        }
        distance[_id] = 0;

        while (list.Count != 0)
        {
            int _vertex = FindMin();

            if (_vertex == _target)
            {
                DJ_Node _pathNode = vertices[_target];
                while (_pathNode != null)
                {
                    path.PushFront(_pathNode);
                    _pathNode = previous[_pathNode.Id];
                }
                return;
            }

            list.Remove(_vertex);

            deleted.Add(_vertex);

            DJ_Node _node = vertices[_vertex];

            TBG_StretchyBuffer<DJ_Node> _neighbours = _node.Neighbours;
            for (int i = 0; i < _neighbours.elements; i++)
            {
                DJ_Node _neighbour = _neighbours[i];
                int _neightbourId = _neighbour.Id;
                if (list.ContainsValue(_neighbour))
                {
                    int _weight = distance[_id]; //DISTANCE TO NEIGHBOUR SHOULD BE MODIFIED WHEN WORKING
                    if (_weight < distance[_neightbourId] )
                    {
                        distance[_neightbourId] = _weight;
                        previous[_neightbourId] = _node;
                    }
                }
            }
        }

    }
}
