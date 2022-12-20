using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelLogic : MonoBehaviour
{
    public GameObject[] students;
    public int NumberOfStudents=5;

    public string[] myNames = new string[] { "Viktor", "Jakub", "Adam", "Mario", "Palo", "Brano", "Daniel", "Igor" };
    void Start()
    {
        for (int i = 0; i < NumberOfStudents; i++)
        {
            int p = Random.RandomRange(0, 0);
            GameObject myBoi = Instantiate(students[p]);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
