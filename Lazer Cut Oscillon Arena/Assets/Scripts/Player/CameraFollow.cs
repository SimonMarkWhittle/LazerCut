using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float bound = 10f;

    public float smoothTime = 0.2f;
    private Vector3 vel = Vector3.zero;
    private Vector3 smoothPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!target) { return;  }

        Vector3 targetPos = target.position;

        if (targetPos.magnitude > bound) {
            Vector3 boundPos = targetPos.normalized * bound;
            smoothPosition = new Vector3(boundPos.x, boundPos.y, transform.position.z);
        }
        else {
            smoothPosition = target.TransformPoint(0, 0, -10);
        }

        transform.position = Vector3.SmoothDamp(transform.position, smoothPosition, ref vel, smoothTime);

        // transform.up = target.up;
    }
}
