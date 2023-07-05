using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Texts : MonoBehaviour
{

    public string insufficient_materials = "(Not Enough Materials)";
    public string insufficient_highscore = "(Not Enough Highscore)";
    public string purchased = "(Purchased)";
    public string MaterialTextHandling(string _value)
    {
        return "(Materials : " + _value + ")";
    }
    public string HighscoreTextHandling(string _value)
    {
        return "(Highscore : " + _value + ")";
    }

}
