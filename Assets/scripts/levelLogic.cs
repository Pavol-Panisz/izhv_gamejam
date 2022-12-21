using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class levelLogic : MonoBehaviour
{
    public GameObject[] students;
    public int NumberOfStudents=5;
    public float spawnRadius;

    public Transform approximateMapCenter;

    public string[] myNames = new string[] { "Viktor", "Jakub", "Adam", "Mario", "Palo", "Brano", "Daniel", "Igor" };
    public Location[] locations;


    [Tooltip("The gameobject that will house all students so they dont spam the hierarchy")]
    public Transform studentsParent;

    public UITeacher teacherUI;
    public teacher_skills teacherSkills;

    public StudentAttributeGroup[] possibleAttributeGroups;

    private void Awake()
    {
        if (!teacherUI) { Debug.LogError("You forgot to assign teacherUI! Put UITeacherLogic from Canvas over there"); }
        if (!teacherSkills) { Debug.LogError("You forgot to assign teacherSkills!"); }

        if (!approximateMapCenter)
        {
            Debug.LogError("You forgot to assign approximateMapCenter! in levelLogic");
        }
    }

    void Start()
    {
        var accumulatedStudents = new student_reactions[NumberOfStudents];

        for (int i = 0; i < NumberOfStudents; i++)
        {
            int p = Random.Range(0, students.Length); // index of student variant to spawn

            // the random group of attributes this student will be assigned
            var attributeGroup = possibleAttributeGroups[Random.Range(0, possibleAttributeGroups.Length)];

            GameObject myBoi = Instantiate(students[p], Vector3.zero, Quaternion.identity, studentsParent); // instantiate one of the 5 student variants
            student_reactions studentBrain = myBoi.GetComponent<student_reactions>();

            if (!studentBrain) { Debug.LogError("golly-gee! student " + myNames + " has no brain!"); }

            SetAttributes(studentBrain, attributeGroup);
            studentBrain.myName = myNames[Random.Range(0, myNames.Length)];
            teacherUI.AddNameButton(studentBrain.myName);

            // calculate position
            Vector3 spawnPos;
            if (studentBrain.isLocated == false) {
                spawnPos = RandomPoint(approximateMapCenter.transform.position, spawnRadius);
            } else { // if likes a location
                spawnPos = RandomPoint(studentBrain.likedLocation.allPositions[0], studentBrain.likedLocation.radius);
            }
            studentBrain.myLegs.Warp(spawnPos);

            accumulatedStudents[i] = studentBrain; // add the brain to the accumulated ones
        }

        // now that all students have been created, send the brains to the teacher so he can use em during distance
        // checks and when calling their methods (usually following the naming convention OnX)
        teacherSkills.AssignStudents(accumulatedStudents);
    }

    // Yeah I know this could be done better
    private void SetAttributes(student_reactions brain, StudentAttributeGroup group)
    {
        foreach (var attribute in group.group)
        {
            if (attribute == StudentAttributes.blind) { brain.isBlind = true; }
            else if (attribute == StudentAttributes.deaf) { brain.isDeaf = true; }
            else if (attribute == StudentAttributes.halfDeaf) { brain.isHalfDead = true; }
            else if (attribute == StudentAttributes.ignorant) { brain.isIgnorant = true; }
            else if (attribute == StudentAttributes.located) {
                brain.isLocated = true;
                SetStudentLocation(brain); 
            }
            else if (attribute == StudentAttributes.loudScared) { brain.isLoudScared = true; }
            else if (attribute == StudentAttributes.rattled) { brain.isRattle = true; }
        }
    }

    public void SetStudentLocation(student_reactions brain)
    {
        // ok so this is really stupid. Basically, let me explain it like this:
        // Imagine you have 3 chocolate booths in your level.
        // Youd have a Location called maybe chocolate with the 3 positions in the allPositions array.
        //
        // Ok but then you need to tell the student, which concrete position out of these 3 he should stay at.
        // So you give him a Location object but the array now only has just 1 element, chosen out of those 3
        // potential ones.
        // Its a bit stupid but whatever, its 2:30 am

        var locationGroup = locations[Random.Range(0, locations.Length)];
        Vector3 concreteLocation = locationGroup.allPositions[Random.Range(0, locationGroup.allPositions.Length)];

        brain.likedLocation = new Location();
        brain.likedLocation.name = locationGroup.name;
        brain.likedLocation.allPositions = new Vector3[1];
        brain.likedLocation.allPositions[0] = concreteLocation;
        brain.likedLocation.radius = locationGroup.radius;
    }

    // ripped straight from the unity docs :)
    Vector3 RandomPoint(Vector3 center, float range)
    {
        Vector3 result = Vector3.zero; 

        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return result;
            }
        }
        return result;
    }

}


public enum StudentAttributes { none, blind, deaf, halfDeaf, rattled, loudScared, located, ignorant }

[System.Serializable]
public class StudentAttributeGroup
{
    public StudentAttributes[] group = new StudentAttributes[2];
}

[System.Serializable]
public class Location
{
    [Header("The meno je len orientacne")]
    public string name;
    
    [Space] [Header("For example if you have 2 chocolate booths, youd add both their locations here")] 
    public Vector3[] allPositions;
    public float radius; // in what radius the student can spawn
}