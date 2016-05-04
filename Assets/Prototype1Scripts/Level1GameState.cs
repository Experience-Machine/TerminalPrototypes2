using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;  
        // for SceneManager


public class Level1GameState : MonoBehaviour 
{
    public Map map;
    private GameObject mapObject; // This is for storage

    private CharacterBehaviour characterBeh;
    private GameObject characterObject;
    private Tile[] tileRange;

    bool setColor;

	// Use this for initialization
	void Start () 
    {
        Debug.Log("Level1: Wakes up!!");
        mapObject = new GameObject("Map");
        map = mapObject.AddComponent<Map>();
        setColor = true;
       
        if(characterBeh == null || characterObject == null)
        {
            characterObject = Resources.Load("Prefabs/Character") as GameObject;
            characterBeh = (Instantiate(characterObject) as GameObject).GetComponent<CharacterBehaviour>();
        }

        characterBeh.gameObject.transform.position = new Vector2(-3.5f, -1.5f);
        characterBeh.setState(CharacterBehaviour.CharacterState.Selected);

        //Randomly generate some collideable tiles
        for (int i = 0; i < 10; i++) 
        {
            float randomX = Random.Range(0f, 10f);
            float randomY = Random.Range(0f, 10f);

            Tile t = map.getTile((int)randomX, (int)randomY);
       
            t.setCollideable(true);
            t.defaultColor = Color.green;
            map.setTileColor(t, Color.green);
        }

    }

	// Update is called once per frame
	void Update () 
    {
        if (characterBeh.getState() == CharacterBehaviour.CharacterState.Idle)
        {
            characterBeh.setState(CharacterBehaviour.CharacterState.Selected);
        }
            // Generate initial range of 3, starting from Character, whose position will be 
            //  at tile 3, 3
            /*
            if (setColor && Time.time > 2f)
            {
                Color cyC = new Color(0, 1f, 1f, .3f);
                characterBeh.gameObject.transform.position = map.getTile(3, 3).transform.position;
                tileRange = map.getRangeTiles(3, 3, 3);
                for (int i = 0; i < tileRange.Length; i++)
                {
                    tileRange[i].currentColor = cyC;
                    tileRange[i].tileRenderer.color = cyC;
                }
                setColor = false;
            }
             * /
            // Example code for manipulating Tiles
           /*int randX = (int)Random.Range(0, 10f);
           int randY = (int)Random.Range(0, 10f);

           Tile t = map.getTile(randX, randY);
           t.tileRenderer.color = Random.ColorHSV();
           t.gameObject.transform.name = "MODIFIED";
           t.setCollideable(false);*/

        }
}