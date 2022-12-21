using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(levelLogic))]
public class levelLogic_editor : Editor
{

    levelLogic levelLogic;

    public void OnEnable()
    {
        levelLogic = (levelLogic)target;
    }

    public void OnSceneGUI()
    {
        foreach (var locationCluster in levelLogic.locations)
        {
            foreach (var concretePos in locationCluster.allPositions)
            {
                var style = new GUIStyle();
                Handles.color = Color.red;
                style.normal.textColor = Color.red;

                Handles.Label(concretePos + Vector3.right * 0.25f, locationCluster.name, style);
                Handles.DrawWireDisc(concretePos, Vector3.forward, 0.2f);
                Handles.DrawWireDisc(concretePos, Vector3.forward, locationCluster.radius);
            }
        }
    }
}
