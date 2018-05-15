using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour {

    public Team team = Team.bots;

    public GameObject bullet;
    public float shootrate = 1f;
    float shootcount = 0f;

    public float shootspeed = 10f;

    public Color color = Color.red;

    Collider2D thisCollider;
    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        thisCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (shootcount < shootrate)
            shootcount += Time.deltaTime;
        else {
            GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);

            if (thisCollider) {
                Collider2D newCollider = newBullet.GetComponent<Collider2D>();
                Physics2D.IgnoreCollision(thisCollider, newCollider);
            }

            Bullet bulletScript = newBullet.GetComponent<Bullet>();
            bulletScript.color = color;

            if (rb) {
                Rigidbody2D body = newBullet.GetComponent<Rigidbody2D>();
                body.velocity += rb.velocity;
            }

            bulletScript.speed = shootspeed;

            shootcount = 0f;
        }
	}
}
