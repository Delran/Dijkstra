﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DJ_Grid
{
    public TBG_StretchyBuffer<DJ_Node> _nodes { get; private set; } = new TBG_StretchyBuffer<DJ_Node>();
    
    public DJ_Grid(int _length, int _depth)
    {
        for (int y = 0; y < _depth+1; y++)
        {
            int _yIndex = y * (_depth+1);
            for (int x = 0; x < _length+1; x++)
            {
                TBG_StretchyBuffer<DJ_Node> _neighbours = new TBG_StretchyBuffer<DJ_Node>();
                if (x!=0)
                {
                    _neighbours.Add(_nodes[(x - 1) + _yIndex]);
                }
                if (y != 0)
                {
                    _neighbours.Add(_nodes[(x) + (y - 1) * (_depth+1)]);
                }
                DJ_Node _node = new DJ_Node(new Vector3(x, 0, y), _neighbours, x + _yIndex);
                if (x != 0)
                {
                    _nodes[(x - 1) + _yIndex].AddNeighbor(_node);
                }
                if (y != 0)
                {
                    _nodes[(x) + (y - 1) * (_depth+1)].AddNeighbor(_node);
                }
                _nodes.Add(_node);
            }
        }
    }
}