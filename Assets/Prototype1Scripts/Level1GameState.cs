using UnityEngine;
using UnityEngine.SceneManagement;  
        // for SceneManager


public class Level1GameState : MonoBehaviour 
{
    public Map map;
    private GameObject mapObject; // This is for storage

    bool setColor;

	// Use this for initialization
	void Start () 
    {
        Debug.Log("Level1: Wakes up!!");
        mapObject = new GameObject("Map");
        map = mapObject.AddComponent<Map>();
        setColor = true;
	}

	// Update is called once per frame
	void Update () 
    {
        if (setColor && Time.time > 2f)
        {
            Color cyC = new Color(0, 1f, 1f, .3f);
            Tile[] tiles = map.getRangeTiles(3, 3, 3);
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i].currentColor = cyC;
                tiles[i].tileRenderer.color = cyC;
            }
            setColor = false;
        }

        /* Example code for manipulating Tiles
       int randX = (int)Random.Range(0, 10f);
       int randY = (int)Random.Range(0, 10f);

       Tile t = map.getTile(randX, randY);
       t.tileRenderer.color = Random.ColorHSV();
       t.gameObject.transform.name = "MODIFIED";
       t.setCollideable(false);
       */
	}
}