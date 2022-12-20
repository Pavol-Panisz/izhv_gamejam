using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class student_reactions : MonoBehaviour
{
    public StudentMovement myLegs;
    public studentAnimation myAnimator;

    public bool isDeaf;
    public bool isHalfDead;
    public bool isRattle;
    public bool isBlind;
    public bool isLoudScared;
    public bool isIgnorant;

    public string[] myNames = new string[] { "Viktor", "Jakub", "Adam", "Mario", "Palo", "Brano", "Daniel", "Igor" };
    public string myName;
    void Start()
    {
        myName = myNames[Random.Range(0, myNames.Length)];
    }

    void Update()
    {
        
    }

    public void OnNameShouted(string name,Vector3 position)
    {
        if (!isDeaf)
        {
            myAnimator.onPlayLook(position);
        }
        
    }
    public void OnNameSaid(string name, Vector3 position)
    {
        if (!isDeaf && !isHalfDead && !isIgnorant)
        {
            myLegs.SetTargetPosition(position);
        }
    }
    public void OnGoToTeacher(Vector3 position)
    {

    }
    public void OnRattled(Vector3 position)
    {

    }
}
