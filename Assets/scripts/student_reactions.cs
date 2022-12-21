using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class student_reactions : MonoBehaviour
{
    public StudentMovement myLegs;
    public studentAnimation myAnimator;

    // the teacher will look at the sprite renderer when determining whether this student was clicked on
    public SpriteRenderer mySpriteRenderer;

    [HideInInspector] public Transform teacherTransform; // so we know what position to follow

    public bool isDeaf = false;
    public bool isHalfDead = false;
    public bool isRattle = false;
    public bool isBlind = false;
    public bool isLoudScared = false;
    public bool isIgnorant = false;
    public bool isLocated = false;
    public Location likedLocation = null;

    public string[] myNames = new string[] { "Viktor", "Jakub", "Adam", "Mario", "Palo", "Brano", "Daniel", "Igor" };
    public string myName;

    private bool isFollowingTeacher = false;

    private void Awake()
    {
        if (!mySpriteRenderer) { Debug.LogError("Forgot to assign MySpriteRenderer in student_reactions. This" +
            "should already be assigned in the student prefab."); }
    }

    void Start()
    {
        myName = myNames[Random.Range(0, myNames.Length)];
    }

    void Update()
    {
        if (isFollowingTeacher)
        {
            myLegs.SetTargetPosition(teacherTransform.position);
        }
        else
        {
            myLegs.SetTargetPosition(transform.position);
        }
    }

    public void OnNameShouted(Vector3 position)
    {
        Debug.Log("on name shouted called on " + this.myName);
        if (!isDeaf)
        {
            myAnimator.onPlayLook(position);
        }
        
    }
    public void OnNameSaid(string name, Vector3 position)
    {
        Debug.Log("on name said called on " + this.myName);
        if (!isDeaf && !isHalfDead && !isIgnorant)
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
                isFollowingTeacher = !isFollowingTeacher;
            }
        }
        else
        {
            //Debug.Log("napravo");
            if (myAnimator.isLookLeft == false)
            {
                //Debug.Log("vsimol som si ze si napravo");
                
                isFollowingTeacher = !isFollowingTeacher;
            }
        }
        
        if (isFollowingTeacher)
        {

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
