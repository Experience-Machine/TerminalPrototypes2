using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{

    public bool collideable;
    private static Map map;
    public SpriteRenderer tileRenderer;

    public int x, y;

    // The present color of the tile. This can be temporarily change
    //  by mouseover, but the tile returns to 'curent color' after
    //  mouse-out.
    public Color currentColor;
    public Color defaultColor;
    private static Color highlightColor = new Color(.3f,.3f,.3f,.15f);

    //private static GameObject tileSelector;
    //private GameObject selectRef;
    // Use this for initialization
    void Start () 
    {
        if(map == null)
        {
            map = GameObject.Find("Map").GetComponent<Map>();
        }

        //collideable = false;
        if (tileRenderer == null)
        {
            tileRenderer = gameObject.GetComponent<SpriteRenderer>();
        }

        //defaultColor = Color.grey;
        //currentColor = defaultColor;
        //tileRenderer.color = currentColor;

    }

    void Awake()
    {
        if (map == null)
        {
            map = GameObject.Find("Map").GetComponent<Map>();
        }
        if (tileRenderer == null)
        {
            tileRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
        defaultColor = Color.grey;
        currentColor = defaultColor;
        tileRenderer.color = currentColor;
        collideable = false;
    }

    // Update is called once per frame
    void Update () 
    {
        
    }

    void OnMouseEnter()
    {
        //renderer.enabled = true;
        if(tileRenderer != null)
            tileRenderer.color = currentColor + highlightColor;

        //Debug.Log(selectRef.transform.position.ToString() + " collideable: " + collideable);

    }
    void OnMouseExit()
    {
        //renderer.enabled = true;
        tileRenderer.color = currentColor;

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
        //Debug.Log("Tile " + transform.position.ToString() + " clicked");
        map.lastTileClicked = this;
    }

    //Getters/Setters
    public bool isCollideable() { return collideable; }
    public void setCollideable(bool collide) { collideable = collide; }
}