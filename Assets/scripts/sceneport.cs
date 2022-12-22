using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneport : MonoBehaviour
{
    public string scene;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 2f)
        {
            SceneManager.LoadScene(scene);
        }
    }
}
