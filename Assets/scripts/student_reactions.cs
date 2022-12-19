using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class student_reactions : MonoBehaviour
{
    public StudentMovement myLegs;
    public string myName;
    public bool isDeaf;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnNameShouted(string name,Vector3 position)
    {
        if (!isDeaf)
        {
            Debug.Log(name);
            if (string.Equals(name,myName))
            {
                Debug.Log("AAAAAAAAa");
                myLegs.SetTargetPosition(position);
                
            }
        }
        
    }
}
