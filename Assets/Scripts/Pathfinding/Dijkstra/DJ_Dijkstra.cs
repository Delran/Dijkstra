using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DJ_Dijkstra 
{
    TBG_StretchyBuffer<DJ_Node> vertices = null, path = new TBG_StretchyBuffer<DJ_Node>();
    TBG_StretchyBuffer<int> deleted = new TBG_StretchyBuffer<int>();

    public Dictionary<int, float> distance = new Dictionary<int, float>();
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


    public TBG_StretchyBuffer<DJ_Node> GetPath(DJ_Node _target) => GetPath(_target.Id);

    public TBG_StretchyBuffer<DJ_Node> GetPath(int _target)
    {
        DJ_Node _pathNode = vertices[_target];
        if (!previous.ContainsKey(_pathNode.Id)) return null;
        path = new TBG_StretchyBuffer<DJ_Node>();
        while (_pathNode != null)
        {
            path.PushFront(_pathNode);
            _pathNode = previous[_pathNode.Id];
        }
        return path;
    }


    int FindMin()
    {
        float _min = Mathf.Infinity;
        int _vertex = -1;

        foreach (var item in list)
        {
            int _id = item.Value.Id;
            float _nodeDist = distance[_id];
            if (_nodeDist < _min)
            {
                _min = _nodeDist;
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

    public void Compute(int _id, int _target = -1)
    {
        Reset();

        for (int i = 0; i < vertices.elements; i++)
        {
            DJ_Node _node = vertices[i];
            if (_node.Enabled)
            {
                distance.Add(i, Mathf.Infinity);
                previous.Add(i, null);
                list.Add(i, vertices[i]);
            }
        }
        distance[_id] = 0;

        while (list.Count != 0)
        {
            int _vertex = FindMin();

            if (_vertex == -1) return;

            if (_target != -1)
            {
                if (_target == _vertex) return;
            }

            list.Remove(_vertex);

            deleted.Add(_vertex);

            DJ_Node _node = vertices[_vertex];

            TBG_StretchyBuffer<DJ_Node> _neighbours = _node.Neighbours;
            for (int i = 0; i < _neighbours.elements; i++)
            {
                DJ_Node _neighbour = _neighbours[i];
                int _neightbourId = _neighbour.Id;
                if (list.ContainsKey(_neighbour.Id))
                {
                    float _weight = distance[_vertex] + Vector3.Distance(_node.Position, _neighbour.Position); //DISTANCE TO NEIGHBOUR SHOULD BE MODIFIED WHEN WORKING
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
