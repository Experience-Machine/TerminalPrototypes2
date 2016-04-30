using UnityEngine;
using System.Collections;

public class TileBehavior : MonoBehaviour
{
    private Renderer renderer;
    // Use this for initialization
    void Start () 
    {
        renderer = GetComponent<Renderer>();
        renderer.enabled = false;
    }
       
    // Update is called once per frame
    void Update () 
    {
        
    }

    void OnMouseEnter()
    {
        renderer.enabled = true;
        renderer.material.color = Color.white;
    }
    void OnMouseExit()
    {
        renderer.enabled = false;
    }
}