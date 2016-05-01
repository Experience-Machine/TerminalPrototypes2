using UnityEngine;
using System.Collections;

public class TileBehavior : MonoBehaviour
{
    private SpriteRenderer renderer;
   
    // Use this for initialization
    void Start () 
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.enabled = true;
        renderer.color = Color.grey;

        
    }
       
    // Update is called once per frame
    void Update () 
    {
        
    }

    void OnMouseEnter()
    {
        
        renderer.enabled = true;
        renderer.color = Color.cyan;
 
    }
    void OnMouseExit()
    {
        renderer.enabled = true;
        renderer.color = Color.grey;
        
    }

    void OnMouseDown()
    {

    }
}