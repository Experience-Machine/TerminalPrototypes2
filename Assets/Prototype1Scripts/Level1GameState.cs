using UnityEngine;
using UnityEngine.SceneManagement;  
        // for SceneManager


public class Level1GameState : MonoBehaviour 
{
    public Map map;
    private GameObject mapObject; // This is for storage

	// Use this for initialization
	void Start () 
    {
        Debug.Log("Level1: Wakes up!!");
        mapObject = new GameObject("Map");
        map = mapObject.AddComponent<Map>();

	}

	// Update is called once per frame
	void Update () 
    {
        /* Example code for manipulating Tiles
        int randX = (int)Random.Range(0, 10f);
        int randY = (int)Random.Range(0, 10f);

        Tile t = map.getTile(randX, randY);
        t.tileRenderer.color = Random.ColorHSV();
        t.gameObject.transform.name = "MODIFIED";
        t.setCollideable(false);
        */

		/*if (Input.GetKey (KeyCode.Escape)) 
        {
            SceneManager.LoadScene("MenuLevel");    // this must be call from an object in this level!
			FirstGameManager.TheGameState.SetCurrentLevel("MenuLevel");
            FirstGameManager.TheGameState.PrintCurrentLevel();
        }*/
	}
}