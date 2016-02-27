using UnityEngine;
using System.Collections;

public class ScriptCanvasControl : MonoBehaviour {

    public ScriptEngine engine;
    public float mouseSensitivity = 10f;
    //public float minimumX, maximumX, minimumY, maximumY;

    

	// Use this for initialization
	void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (engine.freeLook)
        {
            if (engine.lookChange)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = true;
                engine.lookChange = false;
            }
            float h = mouseSensitivity * Input.GetAxis("Mouse X");
            float v = mouseSensitivity * Input.GetAxis("Mouse Y");
            Camera.main.transform.Rotate(-v, h, 0);
            float z = Camera.main.transform.eulerAngles.z;
            Camera.main.transform.Rotate(0, 0, -z);

        }
        else
        {
            if (engine.lookChange)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                engine.lookChange = false;
            }
        }
        //move reticle icon on canvas to mouse position
	}
}
