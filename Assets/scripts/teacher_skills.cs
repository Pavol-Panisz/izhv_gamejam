using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class teacher_skills : MonoBehaviour
{
    [Space]
    public string StudentTag;
    public float shoutRadius;
    public float sayRadius;
    public float rattleRadius;
    public float comeHereRadius;

    // tasty delicious students brains yummy!
    [SerializeField] [Header("Dont touch, showing only for debug!")] 
    private List<student_reactions> studentBrains = new List<student_reactions>();

    [Space]
    [Header("Radius visualizations")]
    public Transform sayRadiusVisual;
    public Transform shoutRadiusVisual;
    public Transform rattleRadiusVisual;
    public Transform comeHereRadiusVisual;

    [MenuItem("CUSTOM DEBUG/teacher findStudents")]
    static void DebugFindStudents()
    {
        var obj = Selection.activeGameObject;
        var teacher = obj.GetComponent<teacher_skills>();
        if (!teacher)
        {
            Debug.LogError("To perform this you must select the teacher (who should have teacher_skills on him)");
        } else
        {
            teacher.FindStudents();
        }
    }

    // Should be called after all students are instantiated - puts all students into the internal students list
    public void FindStudents()
    {
        GameObject[] students = GameObject.FindGameObjectsWithTag(StudentTag);
        if (students.Length == 0) { 
            Debug.LogWarning("Yo when the teacher tried to find students, it found zero. " +
                "Maybe the StudentTag is wrong or you dont have any students in the scene " +
                "when this method is being executed.");  
        }

        // remove previous students
        studentBrains.Clear();

        foreach (var go in students)
        {
            var brain = go.GetComponent<student_reactions>();
            if (!brain)
            {
                Debug.LogError("Yo in the method FindStudents in teacher_skills.cs: This student ain't got no brain!" +
                    "I expect there to be a brain on every gameobject with the student tag.");
            } else {
                studentBrains.Add(brain);
            }
        }
    }

    public void ShoutName(string name)
    {
        foreach (var student in studentBrains) {
            if (Vector2.Distance(transform.position, student.transform.position) <= shoutRadius) {
                student.OnNameShouted(student.name, transform.position);
            }
        }
    }

    public void SayName(string name)
    {
        foreach (var student in studentBrains) {
            if (Vector2.Distance(transform.position, student.transform.position) <= sayRadius)
            {
                student.OnNameSaid(student.name, transform.position);
            }
        }
    }

    public void Rattle()
    {
        foreach (var student in studentBrains) {
            if (Vector2.Distance(transform.position, student.transform.position) <= sayRadius) {
                student.OnRattled(transform.position);
            }
        }
    }

    // yeah this could be done a lot cleaner, but I don't have the time
    public void VisualizeShout(bool doIt) {
        shoutRadiusVisual.transform.localScale = new Vector3(2f, 2f, 0f) * shoutRadius;
        shoutRadiusVisual.gameObject.SetActive(doIt); }
    public void VisualizeSay(bool doIt) {
        sayRadiusVisual.transform.localScale = new Vector3(2f, 2f, 0f) * sayRadius;
        sayRadiusVisual.gameObject.SetActive(doIt); }
    public void VisualizeRattle(bool doIt) {
        rattleRadiusVisual.transform.localScale = new Vector3(2f, 2f, 0f) * rattleRadius;
        rattleRadiusVisual.gameObject.SetActive(doIt); }
    public void VisualizeCmHere(bool doIt) {
        comeHereRadiusVisual.transform.localScale = new Vector3(2f, 2f, 0f) * comeHereRadius;
        comeHereRadiusVisual.gameObject.SetActive(doIt); }

    void Update()
    {

        /*if (Input.GetKeyDown("q"))
        {
            student.OnNameSaid("Jakub",transform.position);
        }
        if (Input.GetKeyDown("e"))
        {
            student.OnNameShouted("Jakub", transform.position);
        }*/
    }
}
