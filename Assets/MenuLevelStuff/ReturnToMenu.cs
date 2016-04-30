using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour {

    // Use this for initialization
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("MenuLevel");    // this must be call from an object in this level!
            FirstGameManager.TheGameState.SetCurrentLevel("MenuLevel");
            FirstGameManager.TheGameState.PrintCurrentLevel();
        }
    }
}