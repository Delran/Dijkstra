using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PF_Obstacle : MonoBehaviour
{
    [SerializeField, Header("Box Collider")] DJ_PathFinding path = null;

    [SerializeField, Header("Box Collider")] BoxCollider boxCollider = null;

    TBG_StretchyBuffer<DJ_Node> nodes => path.Grid._nodes;

    List<Vector3> Corners = new List<Vector3>();

    Vector3 _position = Vector3.zero;
    Vector3 _obstaclePosition = Vector3.zero;

    private void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider>();
        if (!path) return;
    }

    private void UpdateNodes()
    {
        if (nodes == null) return;
        for (int i = 0; i < nodes.elements; i++)
        {
            Corners.Clear();
            DJ_Node _node = nodes[i];
            //Vector3 _position = transform.InverseTransformPoint( _node.Position );
            _position = transform.InverseTransformPoint(_node.Position);
            float _nodeX = _position.x, _nodeY = _position.y, _nodeZ = _position.z;
            //Vector3 _obstaclePosition = transform.position;
            _obstaclePosition = transform.position;

            _nodeX = _nodeX < 0 ? _nodeX += 0.1f : _nodeX -= 0.1f;
            _nodeY = _nodeY < 0 ? _nodeY += 0.1f : _nodeY -= 0.1f;
            _nodeZ = _nodeZ < 0 ? _nodeZ += 0.1f : _nodeZ -= 0.1f;

            //Debug.Log(boxCollider.size);

            /*Vector3 _upLeft = _obstaclePosition + Vector3.forward * gameObject.transform.localScale.z / 2 + Vector3.left * gameObject.transform.localScale.x / 2;
            Vector3 _upRight = _obstaclePosition + Vector3.forward * gameObject.transform.localScale.z / 2 + Vector3.right * gameObject.transform.localScale.x / 2;
            Vector3 _downLeft = _obstaclePosition + Vector3.back * gameObject.transform.localScale.z / 2 + Vector3.left * gameObject.transform.localScale.x / 2;
            Vector3 _downRight = _obstaclePosition + Vector3.back * gameObject.transform.localScale.z / 2 + Vector3.right * gameObject.transform.localScale.x / 2;*/

            //float _halfX = _obstaclePosition.x

            float _xCheck = 0.5f;
            float _yCheck = 0.5f;
            float _zCheck = 0.5f;

            if (_nodeX < _xCheck && _nodeX > -_xCheck &&
                _nodeZ < _yCheck && _nodeZ > -_yCheck &&
                _nodeY < _zCheck && _nodeY > -_zCheck)
            {
                _node.Enabled = false;
            }
            else
            {
                _node.Enabled = true;
            }

            /*Corners.Add(_upLeft);
            Corners.Add(_upRight);
            Corners.Add(_downRight);
            Corners.Add(_downLeft);*/
        }
    }

    private void Update()
    {
        UpdateNodes();
    }


}
