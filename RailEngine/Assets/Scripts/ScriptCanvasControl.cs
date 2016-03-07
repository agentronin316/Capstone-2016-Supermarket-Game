using UnityEngine;
using System.Collections;

public class ScriptCanvasControl : MonoBehaviour {

    public ScriptEngine engine;
    public float mouseSensitivity = 10f;
    public GameObject reticle;

    

	// Use this for initialization
	void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (engine.freeLook)
        {
            if (engine.lookChange)
            {
                engine.lookChange = false;
                //move reticle icon on canvas to mouse position
                if (reticle != null)
                {
                    reticle.transform.position = Input.mousePosition;
                }
                else
                {
                    Debug.Log("No reticle set for the canvas.");
                }
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
                engine.lookChange = false;
            }
            //move reticle icon on canvas to mouse position
            if (reticle != null)
            {
                Vector2 mouseMove = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
                if (mouseMove.magnitude > mouseSensitivity)
                {
                    mouseMove.Normalize();
                    mouseMove = mouseMove * mouseSensitivity;
                }
                reticle.transform.Translate(mouseMove);
            }
            else
            {
                Debug.Log("No reticle set for the canvas.");
            }
        }

        if(Input.GetMouseButtonDown(0))
        {

        }
	}
}
