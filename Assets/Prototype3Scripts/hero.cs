using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class hero : MonoBehaviour {

    // text onscreen
    public Text nameOf; 
    public Text attackOf;
    public Text speicalOf;

    characterCard heroOne = new characterCard(); // make a new hero

    // Use this for initialization
    void Start () {
     
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(heroOne.getName() + " attack: " + heroOne.getAttack().ToString() + " special: " + heroOne.getSpecial().ToString());

        // set the text values
        nameOf.text = "Name: " + heroOne.getName();
        attackOf.text = "Attack: " + heroOne.getAttack().ToString();
        speicalOf.text = "Special: " + heroOne.getSpecial().ToString();
	}
}
