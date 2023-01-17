using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter : MonoBehaviour
{
    Color mouseOverColor = Color.red;
    Color originalColor;

    SpriteRenderer s_Renderer;

    // Start is called before the first frame update
    void Start()
    {
        //Fetch the mesh renderer component from the GameObject
        s_Renderer = GetComponent<SpriteRenderer>();
        //Fetch the original color of the GameObject
        originalColor = s_Renderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        //Debug.Log("Mouse is over GameObject.");
        s_Renderer.color = mouseOverColor;
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        //Debug.Log("Mouse is no longer on GameObject.");
        s_Renderer.color = originalColor;
    }
}

//Attach this script to a GameObject to have it output messages when your mouse hovers over it.
