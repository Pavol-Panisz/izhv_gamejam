using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomcolor_start : MonoBehaviour
{
    public Color32 bruh;
    public SpriteRenderer m_SpriteRenderer;
    float x;
    float y;
    float z;
    void Start()
    {
        x = Random.Range(0f,1f);
        y = Random.Range(0f, 1f);
        z = Random.Range(0f, 1f);
        Color bruh = new Color(
          Random.Range(x, 1f),
          Random.Range(y, 1f),
          Random.Range(z, 1f)
          );

        m_SpriteRenderer.color = bruh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
