using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour
{
    private static GameObject tile; // Tile prefab
    public static Tile[,] map; // Actual tile map
    private int mapMaxX;
    private int mapMaxY;
    private float minXPos;
    private float minYPos;

    public Tile lastTileClicked; // Tiles themselves set this
    public Tile selectedTile;
    private static GameObject tileSelector;
    private GameObject selectRef;
    public Map()
    {
        mapMaxX = 20;
        mapMaxY = 10;
        map = new Tile[mapMaxX, mapMaxY];
        Bounds mWorldBound = new Bounds(Vector3.zero, Vector3.one);
        Camera camera = Camera.main;
        mWorldBound.center = camera.transform.position;
        mWorldBound.size = new Vector2(camera.orthographicSize * 2, camera.orthographicSize * 2);
        minXPos = mWorldBound.min.x - 2;
        minYPos = mWorldBound.min.y;

        lastTileClicked = null;
        selectedTile = null;
    }

    public Map(int width, int height)
    {
        mapMaxX = width;
        mapMaxY = height;
        map = new Tile[mapMaxX, mapMaxY];
        Bounds mWorldBound = new Bounds(Vector3.zero, Vector3.one);
        Camera camera = Camera.main;
        mWorldBound.center = camera.transform.position;
        mWorldBound.size = new Vector2(camera.orthographicSize * 2, camera.orthographicSize * 2);
        minXPos = mWorldBound.min.y;
        minYPos = mWorldBound.min.x;
    }

    public Map(int width, int height, float minX, float minY)
    {
        mapMaxX = width;
        mapMaxY = height;
        minXPos = minX;
        minYPos = minY;
        map = new Tile[mapMaxX, mapMaxY];
    }

	// Use this for initialization
	void Start () 
    {
        if (tile == null)
        {
            tile = Resources.Load("Prefabs/Tile") as GameObject;
        }

        if (tileSelector == null)
        {
            tileSelector = Resources.Load("Prefabs/TileSelector") as GameObject;
        }
        selectRef = null;

        drawMap();
	}
	
    public Tile getTile(int x, int y)
    {
        return map[x, y];
    }

    // Using getTile, then .gameObject functions the same
    public GameObject getTileObject(int x, int y)
    {
        return map[x, y].gameObject;
    }

    // Removes every Tile under this map
    public void destroyMap()
    {
        for(int x = 0; x < mapMaxX; x++)
        {
            for(int y = 0; y < mapMaxY; y++)
            {
                Destroy(map[x, y].gameObject);
            }
        }
    }

    // To be called only AFTER destroying the map, if nessessary
    public void drawMap()
    {
        for (int x = 0; x < mapMaxX; x++)
        {
            for (int y = 0; y < mapMaxY; y++)
            {
                map[x, y] = (Instantiate(tile) as GameObject).GetComponent<Tile>();
                map[x, y].transform.parent = gameObject.transform;
                map[x, y].transform.position = new Vector2(minXPos + x + (map[x, y].transform.localScale.x / 2f), //Start drawing tiles at the upper left corner
                                                           minYPos + y + (map[x, y].transform.localScale.y / 2f));
            }
        }
    }

	// Update is called once per frame
	void Update () 
    {
        if (lastTileClicked != null)
        {
            selectedTile = lastTileClicked;
            if (selectRef != null)
            {
                DestroyObject(selectRef);
            }
            selectRef = Instantiate(tileSelector) as GameObject;
            selectRef.transform.position = selectedTile.transform.position;
            SpriteRenderer sr = selectRef.GetComponent<SpriteRenderer>();
            sr.enabled = true;
            lastTileClicked = null;
        }
	}
}
