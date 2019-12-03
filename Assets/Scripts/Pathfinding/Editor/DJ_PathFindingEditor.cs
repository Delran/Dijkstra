using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using EditoolsUnity;

[CustomEditor(typeof(DJ_PathFinding))]
public class DJ_PathFindingEditor : EditorCustom<DJ_PathFinding>
{
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
