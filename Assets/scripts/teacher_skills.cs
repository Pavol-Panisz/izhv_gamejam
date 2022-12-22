using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class teacher_skills : MonoBehaviour
{
    public Animator animator;
    public GameObject NameSelectionMenu;
    public float reactionTime;
    public float sayTime;

    [Space]
    public string StudentTag;
    public float shoutRadius;
    public float sayRadius;
    public float rattleRadius;
    public float comeHereRadius;

    [Space]
    public SoundEffect followMeSfx;
    public SoundEffect stopFollowingMeSfx;

    // tasty delicious students brains yummy!
    [SerializeField] [Header("Dont touch, showing only for debug!")] 
    private List<student_reactions> studentBrains = new List<student_reactions>();

    [Space]
    [Header("Radius visualizations")]
    public Transform sayRadiusVisual;
    public Transform shoutRadiusVisual;
    public Transform rattleRadiusVisual;
    public Transform comeHereRadiusVisual;

    [MenuItem("CUSTOM DEBUG/teacher findStudents")]
    static void DebugFindStudents()
    {
        var obj = Selection.activeGameObject;
        var teacher = obj.GetComponent<teacher_skills>();
        if (!teacher)
        {
            Debug.LogError("To perform this you must select the teacher (who should have teacher_skills on him)");
        } else
        {
            teacher.FindStudents();
        }
    }

    private void Awake()
    {
        if (!NameSelectionMenu) { Debug.LogError("Forgot to assign name selection menu for teacher_skills."); }
        
        if (!followMeSfx || !stopFollowingMeSfx)
        {
            if (!followMeSfx || !stopFollowingMeSfx)
            {
                Debug.LogError("You forgot to assign at least one of the Sfx fields. " +
                    "The corresponding scripts should be located on the player: gameobjects \"stop/follow me audio\"");
            }
        }
    }

    // Should be called after all students are instantiated - puts all students into the internal students list
    // A better alternative (used by default in levelLogic) is AssignStudents, which doesnt need to use
    // FindObjectsWithTag
    private void FindStudents()
    {
        GameObject[] students = GameObject.FindGameObjectsWithTag(StudentTag);
        if (students.Length == 0) { 
            Debug.LogWarning("Yo when the teacher tried to find students, it found zero. " +
                "Maybe the StudentTag is wrong or you dont have any students in the scene " +
                "when this method is being executed.");  
        }

        // remove previous students
        studentBrains.Clear();

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

    public void AssignStudents(student_reactions[] students)
    {
        studentBrains.Clear();
        foreach (var brain in students) {
            brain.teacherTransform = transform;
            studentBrains.Add(brain);    
        }
    }

    public void ShoutName(string calledName)
    {
        animator.SetBool("isShout", true);
        StartCoroutine(coroutineMS());
        StartCoroutine(shout());
    }

    public void SayName(string calledName)
    {
        animator.SetBool("isSay", true);
        StartCoroutine(coroutine());
        StartCoroutine(say(calledName));
    }

    public void Rattle()
    {
        animator.SetBool("isMexico", true);
        StartCoroutine(coroutineMS());
        StartCoroutine(mexico());
    }

    // yeah this could be done a lot cleaner, but I don't have the time
    public void VisualizeShout(bool doIt) {
        shoutRadiusVisual.transform.localScale = new Vector3(2f, 2f, 0f) * shoutRadius;
        shoutRadiusVisual.gameObject.SetActive(doIt); }
    public void VisualizeSay(bool doIt) {
        sayRadiusVisual.transform.localScale = new Vector3(2f, 2f, 0f) * sayRadius;
        sayRadiusVisual.gameObject.SetActive(doIt); }
    public void VisualizeRattle(bool doIt) {
        rattleRadiusVisual.transform.localScale = new Vector3(2f, 2f, 0f) * rattleRadius;
        rattleRadiusVisual.gameObject.SetActive(doIt); }
    public void VisualizeCmHere(bool doIt) {
        comeHereRadiusVisual.transform.localScale = new Vector3(2f, 2f, 0f) * comeHereRadius;
        comeHereRadiusVisual.gameObject.SetActive(doIt); }

    void Update()
    {
        bool clickedLeft = Input.GetMouseButtonDown(0);
        bool clickedRight = Input.GetMouseButtonDown(1);

        // look at each student in range whether they were clicked on
        if ((clickedLeft || clickedRight) && !NameSelectionMenu.activeInHierarchy) // can click only if name selection isnt visible
        {
            Vector3 m = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            foreach (var student in studentBrains) {
                if (Vector2.Distance(transform.position, student.transform.position) <= comeHereRadius)
                {
                    var sr = student.mySpriteRenderer;
                    var b = sr.bounds;
                    if (b.min.x < m.x && m.x < b.max.x && b.min.y < m.y && m.y < b.max.y)
                    {
                        //Debug.Log("clicked on student " + student.myName);

                        bool doFollow = true;
                        if (clickedLeft) { doFollow = true; }
                        else if (clickedRight) { doFollow = false; }

                        student.OnSetFollowTeacher(doFollow);
                        animator.SetBool("isShow", true);
                        StartCoroutine(coroutineMS());
                        if (doFollow == true) {
                            followMeSfx.PlaySound();
                        }
                        else {
                            stopFollowingMeSfx.PlaySound();
                        }
                        
                    }
                    
                }
            }
        }

    }

    IEnumerator coroutine()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isSay", false);

    }

    IEnumerator coroutineMS()
    {
        yield return new WaitForSeconds(2f);
        animator.SetBool("isShow", false);
        animator.SetBool("isMexico", false);
        animator.SetBool("isShout", false);
    }
    IEnumerator shout()
    {
        yield return new WaitForSeconds(reactionTime);
        foreach (var student in studentBrains)
        {
            if (Vector2.Distance(transform.position, student.transform.position) <= shoutRadius)
            {

                student.OnNameShouted(transform.position);
            }
        }

    }
    IEnumerator say(string calledName)
    {
        yield return new WaitForSeconds(sayTime);
        foreach (var student in studentBrains)
        {
            if (Vector2.Distance(transform.position, student.transform.position) <= sayRadius)
            {
                student.OnNameSaid(calledName, transform.position);
            }
        }

    }
    IEnumerator mexico()
    {
        yield return new WaitForSeconds(reactionTime);
        foreach (var student in studentBrains)
        {
            if (Vector2.Distance(transform.position, student.transform.position) <= rattleRadius)
            {
                student.OnRattled(transform.position);
            }
        }

    }


}
