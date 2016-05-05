using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIBehavior : MonoBehaviour {

    public Sprite sprite;
    public string[] availableActions;
    public int maxHealth;
    public int curHealth;
    // Use this for initialization
    void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void setContent(Sprite s, ArrayList availableActions, int maxHealth, int minHealth, string characterName)
    {
        //Note from Tom: I couldn't seem to get the image updating based on my sample character script
        //I'm not sure at all how to fix it.

        //GameObject charPortrait = GameObject.Find("CharacterUI(Clone)/Character Info/Image");

        //Image i = charPortrait.GetComponent<Image>();
        //i.sprite = Resources.Load("Resources/SharedTextures/hero", typeof(Sprite)) as Sprite;

        GameObject textComp = GameObject.Find("CharacterUI(Clone)/Character Info/Panel (1)/CharName");
        Text charName = textComp.GetComponent<Text>();
        charName.text = characterName;

        GameObject healthBar = GameObject.Find("CharacterUI(Clone)/Character Info/Panel (2)/HealthBar/Panel");
        RectTransform transform = healthBar.GetComponent<RectTransform>();
        float size = (minHealth / maxHealth) * 240;
        //This needs to be ultimately changed, I'm not sure why the below transformation refuses to take a variable
        //for size
        transform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, size, 100);
        Image image = healthBar.GetComponent<Image>();
        image.color = UnityEngine.Color.red;

    }
}
