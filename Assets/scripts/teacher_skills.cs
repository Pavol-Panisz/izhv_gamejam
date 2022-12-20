using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teacher_skills : MonoBehaviour
{
    public student_reactions student; 
    
    void Start()
    {
    }
    void Update()
    {

        if (Input.GetKeyDown("q"))
        {
            student.OnNameSaid("Jakub",transform.position);
        }
        if (Input.GetKeyDown("e"))
        {
            student.OnNameShouted(transform.position);
        }
        if (Input.GetKeyDown("r"))
        {
            student.OnGoToTeacher(transform.position);
        }
    }
}
