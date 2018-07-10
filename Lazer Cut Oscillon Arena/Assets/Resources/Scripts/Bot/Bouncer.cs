using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), typeof(Killable))]
public class Bouncer : MonoBehaviour, IObserver {

    SpriteRenderer sprite;

    public float speed;
    Rigidbody2D rb;

    Vector2 oldVelocity;

    Killable kill;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Random.insideUnitCircle.normalized * speed;

        kill = GetComponent<Killable>();
        kill.AddObserver(this);

        sprite = GetComponentInChildren<SpriteRenderer>();
	}

	void FixedUpdate () {
        oldVelocity = rb.velocity;
        if (rb.velocity.magnitude > speed) {
            rb.velocity = rb.velocity.normalized * speed;
        }
	}

    void OnCollisionEnter2D(Collision2D collision) {
        ContactPoint2D[] points = collision.contacts;
        if (points.Length > 0) {
            ContactPoint2D cp = collision.contacts[0];
            rb.velocity = Vector2.Reflect(oldVelocity, cp.normal);
            rb.velocity += cp.normal;
        }
    }

    public void Ping(Dictionary<string, object> qwargs) {
        sprite.color = Color.red;
        speed += 20f;

        rb.velocity = rb.velocity.normalized * speed;
    }
}
