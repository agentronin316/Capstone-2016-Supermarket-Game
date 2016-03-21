using UnityEngine;
using System.Collections;


public enum ItemNames
{
    BREAD,
    TURKEY,
    MILK,
    CRANBERRY_SAUCE
}
public class ScriptItem {

    public int points;
    public float size;
    public ItemNames name;

    public override string ToString()
    {
        switch (name)
        {
            case ItemNames.BREAD:
                return "Bread";
            case ItemNames.CRANBERRY_SAUCE:
                return "Cranberry Sauce";
            case ItemNames.MILK:
                return "Milk";
            case ItemNames.TURKEY:
                return "Turkey";
            default:
        return name.ToString();
        }
    }
}
