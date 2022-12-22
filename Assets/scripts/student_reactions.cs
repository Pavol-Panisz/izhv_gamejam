using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class student_reactions : MonoBehaviour
{
    public StudentMovement myLegs;
    public studentAnimation myAnimator;
    public Transform aahImage;
    public float aahImaheShowTime;


    Vector3 rattle_pos;
    public Vector3 flee_pos;

    // the teacher will look at the sprite renderer when determining whether this student was clicked on
    public SpriteRenderer mySpriteRenderer;

    [Header("When scared, in what radius to flee")]
    public float scaredFleeRadius;

    [HideInInspector] public Transform teacherTransform; // so we know what position to follow

    [Space]
    public bool isDeaf = false;
    public bool isHalfDead = false;
    public bool isRattle = false;
    public bool isBlind = false;
    public bool isLoudScared = false;
    public bool isIgnorant = false;
    public bool isLocated = false;
    public Location likedLocation = null;

    private bool isFleeing = false; // when the loudScared kid is fleeing, this is true

    [Space]
    public string[] myNames = new string[] { "Viktor", "Jakub", "Adam", "Mario", "Palo", "Brano", "Daniel", "Igor" };
    public string myName;

    [HideInInspector] public Transform fallbackRandomPoints;

    [HideInInspector] public bool isFollowingTeacher = false;

    private void Awake()
    {

        if (!mySpriteRenderer)
        {
            Debug.LogError("Forgot to assign MySpriteRenderer in student_reactions. This" +
"should already be assigned in the student prefab.");
        }
    }

    void Start()
    {
        rattle_pos = transform.position;
        flee_pos = transform.position;
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
            if (isRattle) 
            {
                myLegs.SetTargetPosition(rattle_pos);    
            }
            if (isFleeing)
            {
                myLegs.SetTargetPosition(flee_pos);
            }

        }

    }

    public void OnNameShouted(Vector3 position)
    {
        if (isRattle)
        {
            return;
        }
        if (!isDeaf)
        {
            if (isBlind)
            {
                myAnimator.onNameSaidBlind();
            }
            else
            {

                myAnimator.onPlayLook(position);
            }
        }

        if (isLoudScared)
        {
            StartCoroutine(aahTextCorot());
            flee_pos = levelLogic.RandomPoint(transform.position, scaredFleeRadius, fallbackRandomPoints);
        }
    }
    public void OnNameSaid(string calledName, Vector3 position)
    {
        if (isRattle)
        {
            return;
        }
        Debug.Log("on name said called on " + this.myName + "  " + calledName);
        if (!isDeaf && !isHalfDead && string.Equals(calledName, myName))
        {
            if (isBlind)
            {
                Debug.Log("hej som slepy nehovor moje meno pls");
                myAnimator.onNameSaidBlind();
            }
            else
            {
                myAnimator.onPlayLook(position);
            }
        }
    }
    public void OnSetFollowTeacher(bool doFollow)
    {
        if (isBlind || isRattle || isIgnorant)
        {
            if (Vector3.Distance(teacherTransform.position, transform.position) > 2f) {
                return;
            }
            isFollowingTeacher = doFollow;
            return;
        }
        Vector3 forward = transform.TransformDirection(Vector3.right);
        Vector3 toOther = teacherTransform.position - transform.position;
        if (Vector3.Dot(forward, toOther) < 0)
        {
            //Debug.Log("nalavo");
            if (myAnimator.isLookLeft == true)
            {
                //Debug.Log("vsimol som si ze si nalavo");
                isFollowingTeacher = doFollow;
            }
        }
        else
        {
            //Debug.Log("napravo");
            if (myAnimator.isLookLeft == false)
            {
                //Debug.Log("vsimol som si ze si napravo");

                isFollowingTeacher = doFollow;
            }
        }
    }
    public void OnRattled(Vector3 position)
    {
        //Debug.Log("cotijebe hrkalka UAAAA");
        if (isRattle)
        {
            myAnimator.onRattled();
            //Debug.Log("cotijebe hrkalka UAAAA");
            if (!isBlind) 
            { 
                rattle_pos = position;
            }
        }
    }

    public IEnumerator aahTextCorot()
    {
        isFleeing = true;
        myLegs.SetWalkSpeedToRun();
        
        aahImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(aahImaheShowTime);
        aahImage.gameObject.SetActive(false);
        
        isFleeing = false;
        myLegs.SetWalkSpeedToDefault();
    }
}