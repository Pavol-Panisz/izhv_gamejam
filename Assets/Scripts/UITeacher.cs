using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEditor;
using UnityEngine.SceneManagement;

public class UITeacher : MonoBehaviour
{
    [Header("Name selection related")]
    public GameObject menuSelectionRoot;
    public GameObject endMenu;
    public Transform whereToCreateButton;
    public GameObject buttonPrefab;
    [Space]
    public SoundEffect shoutSoundEffect;
    public SoundEffect saySoundEffect;

    [Space]
    [Header("Other")]
    public teacher_skills teacherLogic;

    public turnonoff spinac;

    [Tooltip("When choosing a name, the following BUTTONS (not gameobjects) will be deactivated, so you cant click them for example")]
    public Selectable[] deactivateWhileSelectingName;

    public GameObject bus;

    enum SpeechCommand { None, Say, Shout };
    SpeechCommand lastSpeechCommand = SpeechCommand.None;

    private void Awake()
    {
        if (!saySoundEffect || !shoutSoundEffect) { 
            Debug.LogError("You forgot to assign at least one of the soundEffect fields. " +
                "The corresponding scripts should be located in the canvas on the say/shout buttons"); 
        }

    }

    void Update()
    {
        if (Vector3.Distance(bus.transform.position,transform.position)<2f)
        {
            endMenu.SetActive(true);
        }    
    }

    // Creates the buttons and then deactivates the name selection panel, as this method
    // should only be called once on Start and the panel should be invisible initially
    public void InitializeNameSelection(List<string> studentNames)
    {
        foreach (string str in studentNames) { AddNameButton(str); }

        menuSelectionRoot.SetActive(false);
    }
    
    public void OnClickedRattle()
    {
        teacherLogic.Rattle();
    }

    public void OnClickedToggleMute()
    {
        spinac.toggleAudio();
    }

    public void OnClickedSayName()
    {
        // deactivate certain buttons and activate the name selection menu
        foreach (var obj in deactivateWhileSelectingName) { obj.interactable = false; }
        menuSelectionRoot.SetActive(true);

        lastSpeechCommand = SpeechCommand.Say;
    }

    public void OnClickedShoutName()
    {
        // deactivate certain buttons and activate the name selection menu
        foreach (var obj in deactivateWhileSelectingName) { obj.interactable = false; }
        menuSelectionRoot.SetActive(true);

        lastSpeechCommand = SpeechCommand.Shout;
    }

    // when you press a name button this gets called
    public void SetNameToSay(string str) 
    {


        if (lastSpeechCommand == SpeechCommand.Say) {
            teacherLogic.SayName(str);
            saySoundEffect.PlaySound();
        } else if (lastSpeechCommand == SpeechCommand.Shout) {
            teacherLogic.ShoutName(name);
            shoutSoundEffect.PlaySound();
        }
        CloseNameSelection();
    }
    

    // this gets also called when you press X while selecting a student name
    public void CloseNameSelection() 
    {
        lastSpeechCommand = SpeechCommand.None;
        // REactivate certain buttons and DEactivate the name selection menu
        foreach (var obj in deactivateWhileSelectingName) { obj.interactable = true; }
        menuSelectionRoot.SetActive(false);

    }

    public void EndGoToMenu()
    {
        SceneManager.LoadScene("menuScene", LoadSceneMode.Single);
        menuSelectionRoot.SetActive(false);

    }

    //[MenuItem("CUSTOM DEBUG/add name selection button")]
    

    public void AddNameButton(string studentName)
    {
        GameObject btn = GameObject.Instantiate(buttonPrefab, whereToCreateButton);
        Transform textObj = btn.transform.GetChild(0);
        textObj.GetComponent<TextMeshProUGUI>().text = studentName;

        Button btnScript = btn.GetComponent<Button>();
        btnScript.onClick.AddListener(delegate { SetNameToSay(studentName); });
            
    }
}
