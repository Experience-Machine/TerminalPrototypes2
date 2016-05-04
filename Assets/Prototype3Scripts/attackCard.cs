using UnityEngine;
using System.Collections;

// handle changing the name
public class attackCard : MonoBehaviour {

    // Use this for initialization
    void OnMouseDown()
    {
        // check which game card is being clicked
        if (gameObject.name == "attack1")
        {
            characterCard.attack = 1;
        }

        if (gameObject.name == "attack2")
        {
            characterCard.attack = 2;
        }

    }
}
