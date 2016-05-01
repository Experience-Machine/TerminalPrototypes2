using UnityEngine;
using System.Collections;

public class CharacterBehaviour : MonoBehaviour 
{
    private Map map;
    private Tile[] movementRange;
    Color movementHighlight = new Color(0, 1f, 1f, .3f);

    int posX, posY;
	// Use this for initialization
	void Start () 
    {
        map = GameObject.Find("Map").GetComponent<Map>();
        posX = 3;
        posY = 3;
        move(posX, posY);
        movementRange = map.getRangeTiles(3, 3, 3);
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(map.selectedTile != null)
        {
            for(int i = 0; i < movementRange.Length; i++)
            {
                if(movementRange[i] == map.selectedTile)
                {
                    map.clearHighlights(movementRange);
                    move(map.selectedTile.x, map.selectedTile.y);
                    movementRange = map.getRangeTiles(posX, posY, 3);
                    map.highlightTiles(movementRange, movementHighlight);
                }
            }
        }
	}

    public void move(int x, int y)
    {
        posX = x;
        posY = y;
        transform.position = map.getTile(x, y).transform.position;
    }
}
