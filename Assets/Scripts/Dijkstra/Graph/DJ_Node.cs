using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DJ_Node
{
    public int Id { get; private set; } = 0;

    public Vector3 Position { get; private set; } = Vector3.zero;

    public TBG_StretchyBuffer<DJ_Node> Neighbours { get; private set; }

    public DJ_Node(Vector3 _pos, TBG_StretchyBuffer<DJ_Node> _neighbours, int _id)
    {
        Id = _id;
        Position = _pos;
        Neighbours = _neighbours;
    }

    public void AddNeighbor(DJ_Node _node)
    {
        Neighbours.Add(_node);
    }
}
