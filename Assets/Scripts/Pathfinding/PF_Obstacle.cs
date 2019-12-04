using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PF_Obstacle : MonoBehaviour
{
    [SerializeField, Header("Path Finder")] DJ_PathFinding path = null;

    List<DJ_Node> nodes => path.Grid._nodes;
    Dictionary<int, DJ_Node> visited = new Dictionary<int, DJ_Node>();

    Vector3 _position = Vector3.zero;

    private void Start()
    {
        if (!path) return;
    }

    private void UpdateNodes()
    {
        if (path.Grid == null)
        {
            return;
        }
        for (int i = 0; i < nodes.Count; i++)
        {
            DJ_Node _node = nodes[i];
            _position = transform.InverseTransformPoint(_node.Position);
            float _nodeX = _position.x, _nodeY = _position.y, _nodeZ = _position.z;

            float _scale = 0.5f;
            float _scaleX = _scale * (1 / transform.localScale.x);
            float _scaleY = _scale * (1 / transform.localScale.y);
            float _scaleZ = _scale * (1 / transform.localScale.z);
            _nodeX = _nodeX < 0 ? _nodeX += _scaleX : _nodeX -= _scaleX;
            _nodeY = _nodeY < 0 ? _nodeY += _scaleY : _nodeY -= _scaleY;
            _nodeZ = _nodeZ < 0 ? _nodeZ += _scaleZ : _nodeZ -= _scaleZ;

            float _xCheck = 0.5f;
            float _yCheck = 0.5f;
            float _zCheck = 0.5f;

            bool _contains = visited.ContainsValue(_node);

            if (_nodeX < _xCheck && _nodeX > -_xCheck &&
                _nodeZ < _yCheck && _nodeZ > -_yCheck &&
                _nodeY < _zCheck && _nodeY > -_zCheck)
            {
                _node.Enabled = false;
                if (!_contains)
                 visited.Add(_node.Id, _node);
            }
            else if (_contains)
            {
                _node.Enabled = true;
                visited.Remove(_node.Id);
            }
        }
    }

    private void Update()
    {
        UpdateNodes();
    }
}
