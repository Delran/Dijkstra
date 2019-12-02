using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class DJ_PathFinding : MonoBehaviour
{
    [SerializeField, Header("Grid length")] int gridLength = 5;
    [SerializeField, Header("Grid width")] int gridWidth = 5;

    [SerializeField, Header("Target ID")] int target = 0;

    DJ_Grid grid = null;
    TBG_StretchyBuffer<DJ_Node> path = null;
    DJ_Dijkstra dijkstra = null;
    System.Diagnostics.Stopwatch clock = new System.Diagnostics.Stopwatch();
    // Start is called before the first frame update
    // Start is called before the first frame update
    TimeSpan worst = TimeSpan.Zero;
    TimeSpan best = TimeSpan.Zero;

    void Start()
    {
        grid = new DJ_Grid(gridLength, gridWidth);

        dijkstra = new DJ_Dijkstra(grid._nodes);
    }

    // Update is called once per frame
    void Update()
    {
        System.Diagnostics.Stopwatch clock = new System.Diagnostics.Stopwatch();
        clock.Start();
        int _size = (gridWidth + 1) * (gridLength + 1);
        if (target > _size) return;
        dijkstra.Compute(0, _size-1);

        clock.Stop();

        TimeSpan ts = clock.Elapsed;
        if (best == TimeSpan.Zero)
        {
            best = ts;
        }
        if (worst == TimeSpan.Zero)
        {
            worst = ts;
        }

        if (ts < best) best = ts;
        if (ts > worst) worst = ts;

        Debug.Log($"{best} -- Benchmark best");
        Debug.Log($"{worst} -- Benchmark worst");

        path = dijkstra.Path;
    }

    private void OnDrawGizmos()
    {
        if (grid == null) return;
        var _nodes = grid._nodes;
        for (int i = 0; i < _nodes.elements; i++)
        {
            DJ_Node _node = _nodes[i];
            var _neighbour = _node.Neighbours;
            Vector3 _position = _node.Position;
            for (int j = 0; j < _neighbour.elements; j++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(_position, _neighbour[j].Position);
            }
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(_nodes[i].Position, 0.1f);
            Handles.Label(_position + Vector3.up, $"Node : {i}");
            Handles.DrawDottedLine(_position, _position + Vector3.up, 0.1f);
        }
        Gizmos.color = Color.white;

        if (path != null)
        {
            for (int i = 1; i < path.elements; i++)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(path[i - 1].Position, path[i].Position);
            }
        }
    }
}
