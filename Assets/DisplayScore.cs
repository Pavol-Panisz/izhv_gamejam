using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    public TextMeshProUGUI percentil;
    public levelLogic mojeDzeci;

    int followingkids;
    int mykids;
    
    void Update()
    {
        followingkids = 0;
        foreach (var mojStudent in mojeDzeci.AllStudents){
            if (mojStudent.isFollowingTeacher == true)
            {
                mykids++;
            }
        }
        foreach (var nasledovnik in mojeDzeci.AllStudents)
        {
            if (nasledovnik.isFollowingTeacher == true)
            {
                followingkids++;
            }
        }
        percentil.text = followingkids.ToString() + " + " + mykids.ToString();
    }
}
