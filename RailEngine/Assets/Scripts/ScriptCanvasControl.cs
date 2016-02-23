using UnityEngine;
using System.Collections;

public class ScriptCanvasControl : MonoBehaviour {

    public ScriptEngine engine;

	// Use this for initialization
	void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (engine.freeLook)
        {
            if (engine.lookChange)
            {
                Cursor.lockState = CursorLockMode.Locked;
                engine.lookChange = false;
            }
            //get mouse movement and use it to rotate the camera
        }
        else
        {
            if (engine.lookChange)
            {
                Cursor.lockState = CursorLockMode.Confined;
                engine.lookChange = false;
            }
        }
        //move reticle icon on canvas to mouse position
	}
}
