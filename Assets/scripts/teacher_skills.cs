using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teacher_skills : MonoBehaviour
{
    public GameObject student;
    void Start()
    {
        student.gameObject.SendMessage("holyshit",50f);
    }
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            student.gameObject.SendMessage("holyshit", 30f);
        }
    }
}
