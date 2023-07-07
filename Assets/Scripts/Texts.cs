
using UnityEngine;

public class Texts : MonoBehaviour
{
    public string insufficient_materials = "(Not Enough Materials)", insufficient_highscore = "(Not Enough Mastery)", purchased = "(Purchased)";
    
    public string MaterialTextHandling(string _value)
    {
        return "(Materials : " + _value + ")";
    }
    public string MasteryTextHandling(string _value)
    {
        return "(Mastery : " + _value + ")";
    }

}
