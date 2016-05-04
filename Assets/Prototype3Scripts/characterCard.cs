using UnityEngine;
using System.Collections;

// character stats
public class characterCard : MonoBehaviour {

    public static int attack;
    public static int special;
    public static string charName;

    public characterCard() { // constructor
        attack = 0;
        special = 0;
        charName = "";
    }

    // setters
    public void setAttack(int a) {
        attack = a;
    }

    public void setSpecial(int s)
    {
        special = s;
    }

    public void setName(string a)
    {
        charName = a;
    }

    // getters
    public int getAttack() {
        return attack;
    }

    public int getSpecial()
    {
        return special;
    }

    public string getName()
    {
        return charName;
    }

}
