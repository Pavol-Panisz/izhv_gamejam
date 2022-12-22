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
    int allKidsCount;

    private void Start()
    {
        //mojeDzeci.OnLeveLSetupComplete += () => { allKidsCount = mojeDzeci.AllStudents.Count; };
    }

    void Update()
    {
        mykids = 0;
        followingkids = 0;
        foreach (var mojStudent in mojeDzeci.AllStudents){
            if (mojStudent.isFollowingTeacher == true && mojStudent.isImpostor)
            {
                mykids++; // len impostori
            }
        }
        foreach (var nasledovnik in mojeDzeci.AllStudents)
        {
            if (nasledovnik.isFollowingTeacher == true && !nasledovnik.isImpostor)
            {
                followingkids++;
            }
        }
        percentil.text = " my kids picked up: " + followingkids.ToString() + "\n not my kids picked up: " + mykids.ToString();
        //percentil.text += "\n\nsuccess rate: " + ((int)(mykids / mojeDzeci.AllStudents.Count)) + "%";
    }
}
