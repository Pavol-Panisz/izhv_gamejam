using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class turnonoff : MonoBehaviour
{
    bool isOn = true;
    public Button mojButton;
    public Sprite Hover;
    public Sprite notHover;

    void Update()
    {
        if (!isOn)
        {
            AudioListener.pause = true;
            mojButton.image.sprite = Hover;
        }
        else
        {
            AudioListener.pause = false;
            mojButton.image.sprite = notHover;
        }
    }

    public void toggleAudio()
    {
        isOn = !isOn;
    }
}
