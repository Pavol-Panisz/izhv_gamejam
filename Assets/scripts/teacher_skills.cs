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
            student.OnNameShouted("jakub",transform.position);
        }
    }
}
