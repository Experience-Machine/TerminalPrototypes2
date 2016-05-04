using UnityEngine;
using System.Collections;

// handle changing the name
public class specialCard : MonoBehaviour {

    void OnMouseDown()
    {
        // check which game card is being clicked
        if (gameObject.name == "special1")
        {
            characterCard.special = 1;
        }

        if (gameObject.name == "special2")
        {
            characterCard.special = 2;
        }

    }
}
