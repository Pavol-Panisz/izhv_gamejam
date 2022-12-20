using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITeacher : MonoBehaviour
{
    [Header("Name selection related")]
    public GameObject menuSelectionRoot;
    public Transform whereToCreateButton;
    public GameObject buttonPrefab;


    // TODO LEFT OFF 11:30 TUESDAY
    // CreateButtons() that creates the buttons based on a received list from LevelLogic 
    // that gives the list during Start()

    public void OnClickedSayName()
    {
        
    }

    public void OnClickedShoutName()
    {

    }

    [ContextMenu("Add debug name button")]
    public void DebugAddButton()
    {
        AddNameButton("SomeName");
    }

    private void AddNameButton(string studentName)
    {
        GameObject btn = GameObject.Instantiate(buttonPrefab, whereToCreateButton);
        Transform textObj = btn.transform.GetChild(0);
        textObj.GetComponent<TextMeshProUGUI>().text = studentName;
    }
}
