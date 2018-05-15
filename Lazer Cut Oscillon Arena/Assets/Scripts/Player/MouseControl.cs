using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour {

    Camera cam;

    public float maxForce;

    public float offset;

    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame

    /*
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        // pointer.SetTarget(mousePos);
        pointer.TargetPos = (mousePos - pointer.ForcePoint); // * dashMultiplier;
        pointer.Forces();


            public Vector2 ForcePoint {
        get { return body.GetRelativePoint(forcePoint); }
    }


        
    public void Forces() {
        Vector2 force = Vector2.ClampMagnitude(targetPos * multiplier, clamp) *  CanMove;

        if (body.GetRelativePointVelocity(forcePoint).magnitude < maxSpeed) {
             body.AddForceAtPosition(force, ForcePoint, ForceMode2D.Impulse);
        }
    }

    */

    void Update () {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 localOffset = (Vector2)transform.up * offset;
        Vector2 worldOffset = rb.worldCenterOfMass + localOffset;

        // Vector2 offsetMouse = mousePos + localOffset;

        Vector2 force = mousePos - worldOffset;

        // Debug.Log("Local offset:" + localOffset);
        // Debug.Log("World offset:" + worldOffset);
        // Debug.Log("Force:" + force);
        Debug.Log("Center of mass" + rb.worldCenterOfMass);


        rb.AddForceAtPosition(force, localOffset);

	}
}
