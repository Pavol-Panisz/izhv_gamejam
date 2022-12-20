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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnNameShouted(Vector3 position)
    {
        if (!isDeaf)
        {
            myAnimator.onPlayLook(position);
        }
        
    }
    public void OnNameSaid(string name, Vector3 position)
    {
        if (!isDeaf && !isHalfDead && string.Equals(name,myName))
        {
            myAnimator.onPlayLook(position);
        }
    }
    public void OnGoToTeacher(Vector3 position)
    {
        Vector3 forward = transform.TransformDirection(Vector3.right);
        Vector3 toOther = position - transform.position;
        if (Vector3.Dot(forward, toOther) < 0)
        {
            //Debug.Log("nalavo");
            if(myAnimator.isLookLeft == true)
            {
                //Debug.Log("vsimol som si ze si nalavo");
                myLegs.SetTargetPosition(position);
            }
        }
        else
        {
            //Debug.Log("napravo");
            if (myAnimator.isLookLeft == false)
            {
                //Debug.Log("vsimol som si ze si napravo");
                myLegs.SetTargetPosition(position);
            }
        }
        
    }
    public void OnRattled(Vector3 position)
    {
        if (isRattle)
        {
            Debug.Log("cotijebe hrkalka UAAAA");
            myLegs.SetTargetPosition(position);
        }
    }
}
