using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingTexture : MonoBehaviour
{
 public float  scrollSpeedX = 0.5f;
 public float  scrollSpeedY = 0.5f;
 public float offsetX, offsetY ;


    public float amplitudeX = 10.0f;
    public float amplitudeY = 5.0f;
    public float omegaX = 1.0f;
    public float omegaY = 5.0f;
    float index;


    public bool still;
 void Update()
    {
        if (!still)
        {
            offsetX += Time.deltaTime * scrollSpeedX;
            offsetY += Time.deltaTime * scrollSpeedY;
        }
        else
        {
            index += Time.deltaTime;
           // offsetY = Mathf.Abs(amplitudeY * Mathf.Sin(omegaY * index));
           // offsetX = Mathf.Abs(amplitudeX * Mathf.Sin(omegaX * index));
            offsetY = Mathf.Sin(omegaY * index);
            offsetX = Mathf.Sin(omegaX * index);
        }
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}
