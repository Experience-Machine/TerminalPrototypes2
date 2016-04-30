using UnityEngine;
using UnityEngine.SceneManagement;
    // for SceneManager
using System.Collections;

public class Level2HeroControl : MonoBehaviour {	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseUp()
    {
        Debug.Log("Level 2: Lower Left clicked");
        SceneManager.LoadScene("LevelOne");
        FirstGameManager.TheGameState.SetCurrentLevel("LevelOne");
    }
	
}