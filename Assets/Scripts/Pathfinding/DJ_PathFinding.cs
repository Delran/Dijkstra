using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class DJ_PathFinding : MonoBehaviour
{
    [SerializeField, Header("Grid container")] GameObject gridContainer = null;
    public GameObject GridContainer => gridContainer;

    [SerializeField, Header("Grid length")] int gridLength = 5;
    [SerializeField, Header("Grid width")] int gridWidth = 5;

    [SerializeField, Header("Target ID")] int target = 0;

    [SerializeField, Header("Path update tick"), Range(0.001f, 1f)] float tick = 0.01f;
    float timer = 0;

    DJ_Grid grid = null;
    public DJ_Grid Grid => grid;

    TBG_StretchyBuffer<DJ_Node> path = null;
    DJ_Dijkstra dijkstra = null;
    // Start is called before the first frame update
    // Start is called before the first frame update
    TimeSpan worst = TimeSpan.Zero;
    TimeSpan best = TimeSpan.Zero;

    void Start()
    {
        if (!gridContainer)
        {
            gridContainer = new GameObject();
            gridContainer.name = "Grid";
        }
        if (grid == null)
        {
            grid = new DJ_Grid(gridLength, gridWidth, gridContainer.transform);
        }

        dijkstra = new DJ_Dijkstra(grid._nodes);
    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (grid._nodes[target].Enabled)
        {
            if (timer > tick )
            {
                timer = 0;
                UpdatePathFinding();
            }
        }
    }

    void UpdatePathFinding()
    {
        System.Diagnostics.Stopwatch clock = new System.Diagnostics.Stopwatch();

        int _size = (gridWidth + 1) * (gridLength + 1);
        //target = _size - 1;
        if (target > _size) return;

        clock.Start();

        dijkstra.Compute(0, target);

        clock.Stop();

        TimeSpan ts = clock.Elapsed;
        if (best == TimeSpan.Zero && worst == TimeSpan.Zero)
        {
            best = ts;
            worst = ts;
            Debug.Log($"{worst} -- Benchmark worst");
        }
        else
        {
            if (ts < best)
            {
                best = ts;
                Debug.Log($"{best} -- Benchmark best");
            }
            if (ts > worst)
            {
                worst = ts;
                Debug.Log($"{worst} -- Benchmark worst");
            }
        }

        path = dijkstra.GetPath(target);
    }


    private void OnDrawGizmos()
    {
        if (grid == null) return;
        var _nodes = grid._nodes;
        for (int i = 0; i < _nodes.elements; i++)
        {
            DJ_Node _node = _nodes[i];
            Vector3 _position = _node.Position;
            /*var _neighbour = _node.Neighbours;
            for (int j = 0; j < _neighbour.elements; j++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(_position, _neighbour[j].Position);
            }
            Handles.Label(_position + Vector3.up, $"Node : {i}");
            Handles.DrawDottedLine(_position, _position + Vector3.up, 0.1f);*/
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(_nodes[i].Position, 0.1f);
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
