using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class levelLogic : MonoBehaviour
{
    public GameObject[] students;
    public int NumberOfMyStudents;
    public int NumberOfStudents=5;
    public GameObject[] standers;
    public int numberOfStanders=20;
    public float spawnRadius;

    [Space]
    public GameObject kidInfoTextPrefab;
    public Transform whereToSpawnEm;

    [Space]
    public List<student_reactions> MyStudents;
    public List<student_reactions> AllStudents;

    [Space]
    [Header("If no good random point is found, one of these will be that point")]
    public Transform fallbackRandomPointsParent;
    
    [Space]
    public bool isTutorialSpawner = false;

    public Transform approximateMapCenter;

    public string[] myNames = new string[] { "Viktor", "Jakub", "Adam", "Mario", "Palo", "Brano", "Daniel", "Igor" };
    public Location[] locations;

    public System.Action OnLeveLSetupComplete;

    [Tooltip("The gameobject that will house all students so they dont spam the hierarchy")]
    public Transform studentsParent;
    public Transform standersParent;

    public UITeacher teacherUI;
    public teacher_skills teacherSkills;

    public List <StudentAttributeGroup> possibleAttributeGroups;


    private void Awake()
    {
        MyStudents = new List<student_reactions>();
        AllStudents = new List<student_reactions>();

        if (!fallbackRandomPointsParent) { Debug.LogError("forgot to assign fallback random points in levelLogic"); }
        
        if (!teacherUI) { Debug.LogError("You forgot to assign teacherUI! Put UITeacherLogic from Canvas over there"); }
        if (!teacherSkills) { Debug.LogError("You forgot to assign teacherSkills!"); }

        if (!approximateMapCenter)
        {
            Debug.LogError("You forgot to assign approximateMapCenter! in levelLogic");
        }
    }

    void Start()
    {
        if (NumberOfStudents < possibleAttributeGroups.Count)
        {
            Debug.LogError("possibleAttributeGroups doesnt have enough attribute-combinations for all the students: " +
                possibleAttributeGroups.Count + " / " + NumberOfStudents);
        }

        var accumulatedStudents = new student_reactions[NumberOfStudents];

        int amountOfSpawnedMyStudents = 0;

        for (int i = 0; i < NumberOfStudents; i++) // sapwning ALL students
        {
            int p = Random.Range(0, students.Length); // index of student variant to spawn
            int type = Random.Range(0, possibleAttributeGroups.Count);
            // the random group of attributes this student will be assigned
            var attributeGroup = possibleAttributeGroups[type];
            possibleAttributeGroups.RemoveAt(type);
            GameObject myBoi = Instantiate(students[p], Vector3.zero, Quaternion.identity, studentsParent); // instantiate one of the 5 student variants
            student_reactions studentBrain = myBoi.GetComponent<student_reactions>();

            if (!studentBrain) { Debug.LogError("golly-gee! student " + myNames + " has no brain!"); }

            SetAttributes(studentBrain, attributeGroup);
            studentBrain.myName = myNames[Random.Range(0, myNames.Length)];
            teacherUI.AddNameButton(studentBrain.myName);

            // give a list of random points in case navmesh position finding fails
            studentBrain.fallbackRandomPoints = fallbackRandomPointsParent;

            // calculate position
            Vector3 spawnPos;
            if (studentBrain.isLocated == false) {
                spawnPos = RandomPoint(approximateMapCenter.transform.position, spawnRadius, fallbackRandomPointsParent);
            } else { // if likes a location
                spawnPos = RandomPoint(studentBrain.likedLocation.allPositions[0], studentBrain.likedLocation.radius, fallbackRandomPointsParent);
            }
            studentBrain.myLegs.Warp(spawnPos);

            if (isTutorialSpawner)
            {
                foreach (var attribute in attributeGroup.group)
                {
                    if (attribute == StudentAttributes.halfDeaf)
                    {
                        Debug.Log("som jeden polohluchy xd");
                        studentBrain.myLegs.Warp(new Vector3(9, 0, 0));
                    }
                    if (attribute == StudentAttributes.deaf)
                    {
                        Debug.Log("som jeden hluchy xd");
                        studentBrain.myLegs.Warp(new Vector3(21, 0, 0));
                    }
                    if (attribute == StudentAttributes.blind)
                    {
                        Debug.Log("som jeden sllepy xsdd");
                        studentBrain.myLegs.Warp(new Vector3(33, 0, 0));
                    }
                    if (attribute == StudentAttributes.located)
                    {
                        Debug.Log("som pri cokolade");
                        studentBrain.myLegs.Warp(new Vector3(45, 0, 0));
                    }
                    if (attribute == StudentAttributes.rattled)
                    {
                        Debug.Log("som jeden mexican");
                        studentBrain.myLegs.Warp(new Vector3(57, 0, 0));
                    }
                    if (attribute == StudentAttributes.loudScared)
                    {
                        Debug.Log("som vystraseny z kriku xd");
                        studentBrain.myLegs.Warp(new Vector3(69, 0, 0));
                    }
                    if (attribute == StudentAttributes.ignorant)
                    {
                        Debug.Log("soom jeden mampicista xd");
                        studentBrain.myLegs.Warp(new Vector3(81, 0, 0));
                    }
                }
            }

            studentBrain.isImpostor = true;
            AllStudents.Add(studentBrain);
            if (amountOfSpawnedMyStudents < NumberOfMyStudents)
            {
                studentBrain.isImpostor = false;
                MyStudents.Add(studentBrain);

                amountOfSpawnedMyStudents++;

                var infoText = Instantiate(kidInfoTextPrefab, whereToSpawnEm);
                var text = infoText.GetComponent<TextMeshProUGUI>();

                text.text = GetKidInfoText(studentBrain);
            }

            accumulatedStudents[i] = studentBrain; // add the brain to the accumulated ones
        }

        for(int i = 0; i < numberOfStanders; i++)
        {
            int p = Random.Range(0, standers.Length);
            Instantiate(standers[p], RandomPoint(approximateMapCenter.transform.position, spawnRadius, fallbackRandomPointsParent), Quaternion.identity, standersParent);
        }

        // now that all students have been created, send the brains to the teacher so he can use em during distance
        // checks and when calling their methods (usually following the naming convention OnX)
        teacherSkills.AssignStudents(accumulatedStudents);

        if (OnLeveLSetupComplete != null) { OnLeveLSetupComplete.Invoke(); }
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
    public static Vector3 RandomPoint(Vector3 center, float range, Transform fallbackRandomPointsParent)
    {


        Vector3 result = fallbackRandomPointsParent.GetChild(Random.Range(0, fallbackRandomPointsParent.childCount)).position;

        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return result;
            }
            else
            {
                i--;
            }
        }
        return result;
    }

    private string GetKidInfoText(student_reactions studentBrain)
    {
        bool hasAlreadyAttribute = false;
        string final = studentBrain.myName + " ";

        bool alreadyBlind = false;
        bool alreadyDeaf = false;
        bool alreadyHalfDeaf = false;
        bool alreadyIgnorant = false;
        bool alreadyLocated = false;
        bool alreadyLoudScared = false;
        bool alreadyRattle = false;

        if (!hasAlreadyAttribute && studentBrain.isBlind) { final +=           "has <b>no eyes</b>"; alreadyBlind = true; }
        else if (!hasAlreadyAttribute && studentBrain.isDeaf) { final +=       "has <b>no ears</b>"; alreadyDeaf = true; }
        else if (!hasAlreadyAttribute && studentBrain.isHalfDead) { final +=   "has <b>one ear</b>"; alreadyHalfDeaf = true; }
        else if (!hasAlreadyAttribute && studentBrain.isIgnorant) { final +=   "really <b>likes Earth</b>"; alreadyIgnorant = true; }
        else if (!hasAlreadyAttribute && studentBrain.isLocated) { final +=    "<b>loves " + studentBrain.likedLocation.name + "</b>"; alreadyLocated = true;  }
        else if (!hasAlreadyAttribute && studentBrain.isLoudScared) { final += "is <b>scared of shouting</b>"; alreadyLocated = true; }
        else if (!hasAlreadyAttribute && studentBrain.isRattle) { final +=     "came for <b>Mexican traditions</b>"; alreadyRattle = true; }

        hasAlreadyAttribute = true;

        if (hasAlreadyAttribute && studentBrain.isBlind && !alreadyBlind) { final += " and has <b>no eyes</b>";  }
        else if (hasAlreadyAttribute && studentBrain.isDeaf && !alreadyDeaf) { final += " and has <b>no ears</b>";  }
        else if (hasAlreadyAttribute && studentBrain.isHalfDead && !alreadyHalfDeaf) { final += " and has <b>one ear</b>";  }
        else if (hasAlreadyAttribute && studentBrain.isIgnorant && !alreadyIgnorant) { final += " and really <b>likes Earth</b>"; }
        else if (hasAlreadyAttribute && studentBrain.isLocated && !alreadyLocated) { final += " and <b> loves " + studentBrain.likedLocation.name + "</b>";  }
        else if (hasAlreadyAttribute && studentBrain.isLoudScared && !alreadyLocated) { final += " and is <b>scared of shouting</b>"; }
        else if (hasAlreadyAttribute && studentBrain.isRattle && !alreadyRattle) { final += " and came for <b>Mexican traditions</b>";  }

        return final;
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







