using UnityEngine;
using System.Collections;

public class ScriptShelfItem : MonoBehaviour {

    [HideInInspector]
    public ScriptItem itemDetails;
    public ItemNames itemName;
    public float size;
    public int points;

    void Start()
    {
        itemDetails = new ScriptItem();
        itemDetails.name = itemName;
        itemDetails.points = points;
        itemDetails.size = size;
    }
}
