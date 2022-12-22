using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menickobuttonik : MonoBehaviour
{
    public string scene_teleport1;
    public string scene_teleport2;
    public void onClick()
    {
        SceneManager.LoadScene(scene_teleport1, LoadSceneMode.Single);
    }
    public void onClicktutorial()
    {
        SceneManager.LoadScene(scene_teleport2, LoadSceneMode.Single);
    }
}
