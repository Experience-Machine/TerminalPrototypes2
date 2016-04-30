using UnityEngine;
using UnityEngine.SceneManagement;  
        // for SceneManager


public class Level1GameState : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
        Debug.Log("Level1: Wakes up!!");
	}
	
	// Update is called once per frame
	void Update () {
		/*if (Input.GetKey (KeyCode.Escape)) {
            SceneManager.LoadScene("MenuLevel");    // this must be call from an object in this level!
			FirstGameManager.TheGameState.SetCurrentLevel("MenuLevel");
            FirstGameManager.TheGameState.PrintCurrentLevel();
        }*/
	}
}