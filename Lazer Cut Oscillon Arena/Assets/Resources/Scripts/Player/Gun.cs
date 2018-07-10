using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public Team team = Team.players;

    public GameObject bullet;
    public float cooldown = 1f;
    float timer = 0f;

    public Color color = Color.blue;

    public float offset = 0.5f;
    public float shootSpeed = 20f;

    Collider2D thisCollider;
    // Rigidbody2D rb;

    // Use this for initialization
    void Start() {
        thisCollider = GetComponent<Collider2D>();
        // rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        if (timer < cooldown)
            timer += Time.deltaTime;
        else if (Input.GetMouseButtonDown(0)){
            Vector3 thisOffset = offset * transform.up;
            GameObject newBullet = Instantiate(bullet, transform.position + thisOffset, transform.rotation);

            if (thisCollider) {
                Collider2D newCollider = newBullet.GetComponent<Collider2D>();
                Physics2D.IgnoreCollision(thisCollider, newCollider);
            }

            Bullet bulletScript = newBullet.GetComponent<Bullet>();
            bulletScript.color = color;
            bulletScript.speed = shootSpeed;
            bulletScript.team = team;

            /* 
            if (rb) {
                Rigidbody2D body = newBullet.GetComponent<Rigidbody2D>();
                body.velocity += rb.velocity;
            }
            */

            timer = 0f;
        }
    }
}
