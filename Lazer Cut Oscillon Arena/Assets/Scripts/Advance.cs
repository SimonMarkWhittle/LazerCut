using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Advance : MonoBehaviour {

    public float speed = 5f;

    Rigidbody2D rb;

    bool halted = false;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (!halted)
            rb.velocity = transform.up * speed;
	}

    public void Go() {
        halted = false;
    }

    public void Halt() {
        halted = true;
        rb.velocity = new Vector3();
    }
}
