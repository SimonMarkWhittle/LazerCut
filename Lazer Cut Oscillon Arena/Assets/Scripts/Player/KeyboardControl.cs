using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardControl : MonoBehaviour {

    public float maxVelocity;
    public float baseSpeed;
    public float dashSpeed;
    Rigidbody2D rb;

    bool dash = false;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
            dash = true;
        else if (Input.GetKeyUp(KeyCode.Space))
            dash = false;

        float xVeloc = Input.GetAxis("Horizontal");
        float yVeloc = Input.GetAxis("Vertical");

        Vector2 veloc = new Vector2(xVeloc, yVeloc);
        if (veloc.magnitude > 1f)
            veloc.Normalize();

        if (dash)
            rb.velocity = veloc * maxVelocity * dashSpeed;
        else
            rb.velocity = veloc * maxVelocity * baseSpeed;
    }
}
