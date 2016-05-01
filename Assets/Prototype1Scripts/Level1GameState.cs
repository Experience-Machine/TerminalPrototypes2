using UnityEngine;
using UnityEngine.SceneManagement;  
        // for SceneManager


public class Level1GameState : MonoBehaviour 
{
    private static GameObject tile; // Tile prefab
    public static GameObject[,] map; // Actual tile map
    public int mapMaxX = 20;
    public int mapMaxY = 10;

    private Bounds mWorldBound;  // this is the world bound
    public Vector2 mWorldMin;	// Better support 2D interactions
    public Vector2 mWorldMax;
    public Vector2 mWorldCenter;
    private Camera mMainCamera;
	// Use this for initialization
	void Start () 
    {
        Debug.Log("Level1: Wakes up!!");

        mMainCamera = Camera.main;
        mWorldBound = new Bounds(Vector3.zero, Vector3.one);
        UpdateWorldWindowBound();

        if(tile == null)
        {
            tile = Resources.Load("Prefabs/Tile") as GameObject;
        }
        map = new GameObject[mapMaxX, mapMaxY];
        for (int x = 0; x < mapMaxX; x++)
        {
            for (int y = 0; y < mapMaxY; y++)
            {
                map[x, y] = Instantiate(tile) as GameObject;
                map[x, y].transform.position = new Vector2(mWorldBound.min.x + x + (map[x,y].transform.localScale.x / 2f), //Start drawing tiles at the upper left corner
                                                           mWorldBound.min.y + y + (map[x, y].transform.localScale.y / 2f));
               
            }
        }
	}

    public void UpdateWorldWindowBound()
    {
        // get the main 
        if (null != mMainCamera)
        {
            float maxY = mMainCamera.orthographicSize;
            float maxX = mMainCamera.orthographicSize * mMainCamera.aspect;
            float sizeX = 2 * maxX;
            float sizeY = 2 * maxY;
            float sizeZ = Mathf.Abs(mMainCamera.farClipPlane - mMainCamera.nearClipPlane);

            // Make sure z-component is always zero
            Vector3 c = mMainCamera.transform.position;
            c.z = 0.0f;
            mWorldBound.center = c;
            mWorldBound.size = new Vector3(sizeX, sizeY, sizeZ);

            mWorldCenter = new Vector2(c.x, c.y);
            mWorldMin = new Vector2(mWorldBound.min.x, mWorldBound.min.y);
            mWorldMax = new Vector2(mWorldBound.max.x, mWorldBound.max.y);
        }
    }

	// Update is called once per frame
	void Update () 
    {
		/*if (Input.GetKey (KeyCode.Escape)) {
            SceneManager.LoadScene("MenuLevel");    // this must be call from an object in this level!
			FirstGameManager.TheGameState.SetCurrentLevel("MenuLevel");
            FirstGameManager.TheGameState.PrintCurrentLevel();
        }*/
	}
}