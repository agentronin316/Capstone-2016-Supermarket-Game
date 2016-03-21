using UnityEngine;
using System.Collections;

public class ScriptHand : MonoBehaviour {

    public ScriptCharacterData playerData;
    public GameObject player;
    public ScriptCanvasControl targetingScript;
    int errorCount = 0;

    float distance;
    //ScriptItem grabbedItem;
    bool extending = true;

	// Update is called once per frame
	void Update ()
    {
        if (playerData == null)
        {
            if (errorCount > 2)
            {
                Debug.Log("Null Player Data on Hand, destoying Hand");
                Destroy(gameObject);
            }
            else
            {
                errorCount++;
                Debug.Log("Null Player Data on Hand, " + errorCount);
            }
        }
        else
        {
            if (extending)
            {
                distance = (transform.position - player.transform.position).magnitude;
                if (distance > playerData.armLength)
                {
                    extending = false;
                }
                else
                {
                    transform.Translate(Vector3.forward * Time.deltaTime * playerData.reachSpeed);
                }
            }
            else
            {
                distance = (transform.position - player.transform.position).magnitude;
                if (distance < .5f)
                {
                    //player.GetComponent<ScriptEngine>().inventory.Add(grabbedItem);
                    targetingScript.canShoot = true;
                    Destroy(gameObject);
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, player.transform.position, playerData.reachSpeed / distance * Time.deltaTime);
                }
            }
        }
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag != "Player")
        {
            extending = false;
        }
        if (other.transform.tag == "Item")
        {
            //if (grabbedItem == null)
            //{
            //    grabbedItem = other.gameObject.GetComponent<ScriptShelfItem>().itemDetails;
            //    if (grabbedItem == null)
            //    {
            //        Debug.Log("Item at " + other.transform.position + " has no ScriptItem attached.");
            //    }
            //}
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != "Player")
        {
            extending = false;
        }
        if (other.transform.tag == "Item")
        {
            //if (grabbedItem == null)
            //{
            //    grabbedItem = other.gameObject.GetComponent<ScriptShelfItem>().itemDetails;
            //    if (grabbedItem == null)
            //    {
            //        Debug.Log("Item at " + other.transform.position + " has no ScriptItem attached.");
            //    }
            //}
        }
    }
}