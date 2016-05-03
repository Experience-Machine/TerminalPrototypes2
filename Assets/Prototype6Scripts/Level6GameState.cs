using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;  
        // for SceneManager


public class Level6GameState : MonoBehaviour 
{
    public Map map;
    private GameObject mapObject; // This is for storage

    private EnemyBehaviour enemyBeh;
    private GameObject enemyObject;
    private Tile[] tileRange;

    bool setColor;

	// Use this for initialization
	void Start () 
    {
        Debug.Log("Level1: Wakes up!!");
        mapObject = new GameObject("Map");
        map = mapObject.AddComponent<Map>();
        setColor = true;

        if (enemyBeh == null || enemyObject == null)
        {
            enemyObject = Resources.Load("Prefabs/Enemy") as GameObject;
            enemyBeh = (Instantiate(enemyObject) as GameObject).GetComponent<EnemyBehaviour>();
        }

        enemyBeh.gameObject.transform.position = new Vector2(-3.5f, -1.5f);

        //Randomly generate some collideable tiles
        for (int i = 0; i < 10; i++) 
        {
            float randomX = Random.Range(0f, 10f);
            float randomY = Random.Range(0f, 10f);

            Tile t = map.getTile((int)randomX, (int)randomY);
       
            t.setCollideable(true);
            t.hasUnit = true;
            t.defaultColor = Color.green;
            map.setTileColor(t, Color.green);
        }

    }

	// Update is called once per frame
	void Update () 
    {

	}
}