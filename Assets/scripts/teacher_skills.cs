using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teacher_skills : MonoBehaviour
{
    [Header("this is useless but i left it here anyway so u see it dano")]
    public student_reactions student;

    [Space]
    public string StudentTag;
    public float shoutRadius;
    public float sayRadius;
    public float rattleRadius;
    public float comeHereRadius;

    // tasty delicious students brains yummy!
    private List<student_reactions> studentBrains = new List<student_reactions>();

    // Should be called after all students are instantiated - puts all students into the internal students list
    public void FindStudents()
    {
        GameObject[] students = GameObject.FindGameObjectsWithTag(StudentTag);
        if (students.Length == 0) { 
            Debug.LogWarning("Yo when the teacher tried to find students, it found zero. " +
                "Maybe the StudentTag is wrong or you dont have any students in the scene " +
                "when this method is being executed.");  
        }

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
        Debug.Log("shouting name " + name); 
    }

    public void SayName(string name)
    {
        Debug.Log("saying name " + name);
    }

    public void Rattle()
    {
        Debug.Log("TEACHER RATTLED");
    }
    
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
