using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(teacher_skills))]
public class teacher_skills_editor : Editor
{
    teacher_skills teacher;

    public void OnEnable()
    {
        teacher = (teacher_skills)target;
    }

    public void OnSceneGUI()
    {
        var style = new GUIStyle();

        Handles.color = Color.red;
        style.normal.textColor = Color.red;
        Handles.Label(teacher.transform.position + Vector3.right * teacher.shoutRadius, "shout radius", style);
        Handles.DrawWireDisc(teacher.transform.position, -teacher.transform.forward, teacher.shoutRadius);

        Handles.color = Color.blue;
        style.normal.textColor = Color.blue;
        Handles.Label(teacher.transform.position + Vector3.right * teacher.sayRadius + Vector3.down * 0.5f, "say radius", style);
        Handles.DrawWireDisc(teacher.transform.position, -teacher.transform.forward, teacher.sayRadius);

        Handles.color = Color.green;
        style.normal.textColor = Color.green;
        Handles.Label(teacher.transform.position + Vector3.right * teacher.rattleRadius + Vector3.down * 1f, "rattle radius", style);
        Handles.DrawWireDisc(teacher.transform.position, -teacher.transform.forward, teacher.rattleRadius);

        Handles.color = Color.yellow;
        style.normal.textColor = Color.yellow;
        Handles.Label(teacher.transform.position + Vector3.right * teacher.comeHereRadius + Vector3.down * 1.5f, "cmhere radius", style);
        Handles.DrawWireDisc(teacher.transform.position, -teacher.transform.forward, teacher.comeHereRadius);
    }
}
