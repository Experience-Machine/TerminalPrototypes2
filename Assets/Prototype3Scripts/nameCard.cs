using UnityEngine;
using System.Collections;

// handle changing the name
public class nameCard : MonoBehaviour {

    void OnMouseDown()
    {
        // check which game card is being clicked
        if (gameObject.name == "at")
        {
            characterCard.charName = "Alan Turing";
        }

        if (gameObject.name == "jvn")
        {
            characterCard.charName = "John Von Neumann";
        }

    }
}
