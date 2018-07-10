using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team { players, bots };

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class Bullet : MonoBehaviour, IDamager {

    public float speed = 10f;
    public float damage = 1f;

    public Color color = Color.red;

    public Team team = Team.bots;

    protected Rigidbody2D rb;
    SpriteRenderer render;

	// Use this for initialization
	protected virtual void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity += (Vector2)transform.up * speed;

        render = GetComponent<SpriteRenderer>();
        render.color = color;
	}

    void OnTriggerEnter2D(Collider2D other) {
        Destroy(this.gameObject);
    }

    public float GetDamage() {
        return damage;
    }

    public Team GetTeam() {
        return team;
    }
}
