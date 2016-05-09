using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SampleCharacterUI : MonoBehaviour {

    // Use this for initialization

    public GameObject charUI = null;
    public GameObject charUIInstance = null;
    public float maxHealth;
    public float curHealth;
    public string characterName;
    public Sprite s;

    void Start () {
        if (charUI == null)
        {
            charUI = Resources.Load("Prefabs/CharacterUI") as GameObject;
        }

        maxHealth = 50f;
        curHealth = 22f;
        characterName = "Hi I'm a Sample Character";

        s = Resources.Load("SharedTextures/hero", typeof(Sprite)) as Sprite;
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
            script.setContent(s, maxHealth, curHealth, characterName);
       }
       else
        {
            Destroy(charUIInstance);
            charUIInstance = null;
        }
            
    }
}
