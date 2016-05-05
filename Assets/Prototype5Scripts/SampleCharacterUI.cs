using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SampleCharacterUI : MonoBehaviour {

    // Use this for initialization

    public GameObject charUI = null;
    public GameObject charUIInstance = null;
    public ArrayList availableActions;
    public int maxHealth;
    public int curHealth;
    public string characterName;
    public Sprite s;

    void Start () {
        if (charUI == null)
        {
            charUI = Resources.Load("Prefabs/CharacterUI") as GameObject;
        }

        availableActions = new ArrayList();
        availableActions.Add("Thread");
        availableActions.Add("GarbageCollection");

        maxHealth = 50;
        curHealth = 22;
        characterName = "Hi I'm a Sample Character";

        s = Resources.Load("Resources/SharedTextures/hero", typeof(Sprite)) as Sprite;
}
	
	// Update is called once per frame
	void Update () {

    }

    void OnMouseDown()
    {
       if (charUIInstance == null)
       {
            charUIInstance = Instantiate(charUI) as GameObject;
            UIBehavior script = charUIInstance.GetComponent<UIBehavior>();
            script.setContent(s, availableActions, maxHealth, curHealth, characterName);
       }
       else
        {
            Destroy(charUIInstance);
            charUIInstance = null;
        }
            
    }
}
