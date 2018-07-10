using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceMouse : MonoBehaviour {

    Camera cam;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
	}
	
	// Update is called once per frame
	void LateUpdate () {

        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(mouseScreenPos);
        Vector3 facePoint = new Vector3(mouseWorldPos.x, mouseWorldPos.y, transform.position.z);
        Vector3 direction = (facePoint - transform.position).normalized;

        transform.up = direction;
        // transform.LookAt(facePoint);
	}
}
