using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class TBG_DijkstraTest : MonoBehaviour
{
    DJ_Grid grid = null;
    TBG_StretchyBuffer<DJ_Node> path = null;
    // Start is called before the first frame update
    void Start()
    {
        /*grid = new DJ_Grid(5, 5);


        System.Diagnostics.Stopwatch _clock = new System.Diagnostics.Stopwatch();
        _clock.Start();
        DJ_Dijkstra _dijkstra = new DJ_Dijkstra(grid._nodes);
        _dijkstra.Compute(0, 5 * 5);

        _clock.Stop();


        TimeSpan ts = _clock.Elapsed;

        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);

        Debug.Log($"{ts} -- Dijkstra time");

        path = _dijkstra.Path;
        Debug.Log(path.elements);*/
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
                Gizmos.DrawLine(path[i-1].Position, path[i].Position);
            }
        }
    }
}
