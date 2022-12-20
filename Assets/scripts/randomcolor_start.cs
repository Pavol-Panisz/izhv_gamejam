using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomcolor_start : MonoBehaviour
{
    public Color32 bruh;
    public SpriteRenderer m_SpriteRenderer;
    int x;
    int y;
    int z;
    void Start()
    {
        Color bruh = new Color(
          Random.Range(0.4f, 0.8f),
          Random.Range(0.4f, 0.8f),
          Random.Range(0.4f, 0.8f)
          );

        m_SpriteRenderer.color = bruh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
