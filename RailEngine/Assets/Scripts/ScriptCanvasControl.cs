﻿using UnityEngine;
using System.Collections;

public class ScriptCanvasControl : MonoBehaviour {

    public ScriptEngine engine;
    public float mouseSensitivity = 10f;
    public GameObject reticle;
    public GameObject handPrefab;
    public bool canShoot = true;

    Camera mainCamera;
    

	// Use this for initialization
	void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mainCamera = Camera.main;
        mouseSensitivity = engine.playerCharacter.trackingSpeed;
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
            float h = mouseSensitivity * .1f * Input.GetAxis("Mouse X");
            float v = mouseSensitivity * .1f * Input.GetAxis("Mouse Y");
            mainCamera.transform.Rotate(-v, h, 0);
            float z = mainCamera.transform.eulerAngles.z;
            mainCamera.transform.Rotate(0, 0, -z);

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
                Vector2 mouseMove = 5 * new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
                if (mouseMove.magnitude > mouseSensitivity * 100 * Time.deltaTime)
                {
                    mouseMove.Normalize();
                    mouseMove = mouseMove * 100 * mouseSensitivity * Time.deltaTime;
                }
                reticle.transform.Translate(mouseMove);
            }
            else
            {
                Debug.Log("No reticle set for the canvas.");
            }
        }

        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            //Debug.Log("Mouse Click");
            Ray ray = mainCamera.ScreenPointToRay(reticle.transform.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                Debug.DrawLine(mainCamera.transform.position, hit.point, Color.green, 10f);
                GameObject projectile = Instantiate(handPrefab, engine.transform.position, Quaternion.identity) as GameObject;
                projectile.transform.LookAt(hit.point);
                projectile.transform.parent = engine.transform;
                ScriptHand hand = projectile.GetComponent<ScriptHand>();
                hand.playerData = engine.playerCharacter;
                hand.player = engine.gameObject;
                hand.targetingScript = this;
                canShoot = false;
            }
        }
    }
}
