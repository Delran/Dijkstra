using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DJ_Node : MonoBehaviour
{

    [SerializeField]
    public int Id { get; private set; } = 0;

    public Vector3 Position { get; private set; } = Vector3.zero;

    public List<DJ_Node> Neighbours { get; private set; }

    [SerializeField]
    public bool Enabled = true;

    public void Init(Vector3 _pos, List<DJ_Node> _neighbours, int _id)
    {
        Id = _id;
        name = $"Node {_id}";
        Position = _pos;
        Neighbours = _neighbours;
        transform.position = _pos;
        //Collider = gameObject.AddComponent<SphereCollider>();
        //Collider.isTrigger = true;
    }

    public void AddNeighbor(DJ_Node _node)
    {
        Neighbours.Add(_node);
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PF_Obstacle>())
            Enabled = false;
    }*/

    /*private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<PF_Obstacle>())
            Enabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        Enabled = true;
    }*/
}
