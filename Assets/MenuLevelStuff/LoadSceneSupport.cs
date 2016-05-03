using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// for SceneManager

public class LoadSceneSupport : MonoBehaviour {

	public string LevelName = null;

    public Button mPrototype1Button;
    public Button mPrototype2Button;
    public Button mPrototype3Button;
    public Button mPrototype4Button;
    public Button mPrototype5Button;
    public Button mPrototype6Button;

    // Use this for initialization
    void Start () {
        // Workflow assume:
        //      mLevelOneButton: is dragged/placed from UI
        mPrototype1Button = GameObject.Find("Prototype1Button").GetComponent<Button>();
        mPrototype2Button = GameObject.Find("Prototype2Button").GetComponent<Button>();
        mPrototype3Button = GameObject.Find("Prototype3Button").GetComponent<Button>();
        mPrototype4Button = GameObject.Find("Prototype4Button").GetComponent<Button>();
        mPrototype5Button = GameObject.Find("Prototype5Button").GetComponent<Button>();
        mPrototype6Button = GameObject.Find("Prototype6Button").GetComponent<Button>();
        
        mPrototype1Button.onClick.AddListener(Button1Service);
        mPrototype2Button.onClick.AddListener(Button2Service);
        mPrototype3Button.onClick.AddListener(Button3Service);
        mPrototype4Button.onClick.AddListener(Button4Service);
        mPrototype5Button.onClick.AddListener(Button5Service);
        mPrototype6Button.onClick.AddListener(Button6Service);
    }

    #region Button service function
        private void Button1Service() {
            LoadScene("Prototype1");
        }

        private void Button2Service()
        {
            LoadScene("Prototype2");
        }

        private void Button3Service()
        {
            LoadScene("Prototype3");
        }

        private void Button4Service()
        {
            LoadScene("Prototype4");
        }

        private void Button5Service()
        {
            LoadScene("Prototype5");
        }

        private void Button6Service()
        {
            LoadScene("Prototype6");
        }
    #endregion

    // Update is called once per frame
    void Update () {
	
	}
    
	void LoadScene(string theLevel) {
        SceneManager.LoadScene(theLevel);
		FirstGameManager.TheGameState.SetCurrentLevel(theLevel);
		FirstGameManager.TheGameState.PrintCurrentLevel();
	}
}