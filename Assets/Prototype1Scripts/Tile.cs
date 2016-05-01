using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{

    private bool collideable;
    private static Map map;
    public SpriteRenderer tileRenderer;

    //private static GameObject tileSelector;
    //private GameObject selectRef;
    // Use this for initialization
    void Start () 
    {
        if(map == null)
        {
            map = GameObject.Find("Map").GetComponent<Map>();
        }

        collideable = false;
        if (tileRenderer == null)
        {
            tileRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
        //renderer.enabled = true;
        tileRenderer.color = Color.grey;
        /*
        if (tileSelector == null)
        {
            tileSelector = Resources.Load("Prefabs/TileSelector") as GameObject;
        }
        selectRef = null;
        */
    }
       
    // Update is called once per frame
    void Update () 
    {
        
    }

    void OnMouseEnter()
    {
        //renderer.enabled = true;
        tileRenderer.color = Color.cyan;

        //Debug.Log(selectRef.transform.position.ToString() + " collideable: " + collideable);

    }
    void OnMouseExit()
    {
        //renderer.enabled = true;
        tileRenderer.color = Color.grey;

        //Destroy(selectRef);
        //selectRef = null;
        
    }

    void OnMouseDown()
    {
        /*
        selectRef = Instantiate(tileSelector) as GameObject;
        selectRef.transform.position = transform.position; 
        SpriteRenderer sr = selectRef.GetComponent<SpriteRenderer>();
        sr.enabled = true;
        */
        Debug.Log("Tile " + transform.position.ToString() + " clicked");
        map.lastTileClicked = this;
    }

    //Getters/Setters
    public bool isCollideable() { return collideable; }
    public void setCollideable(bool collide) { collideable = collide; }
}