using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Sword : MonoBehaviour, IDamager {
    public Team team = Team.players;

    public float damage;
    Collider2D thisCollider;

    public float cooldown = 1f;
    float timer = 0f;


	// Use this for initialization
	void Start () {
        thisCollider = GetComponent<Collider2D>();
        Collider2D parentCollider = GetComponentInParent<Collider2D>();
        if (parentCollider)
            Physics2D.IgnoreCollision(parentCollider, thisCollider);
	}
	
	// Update is called once per frame
	void Update () {
        if (timer < cooldown)
            timer += Time.deltaTime;
        else if (Input.GetMouseButtonDown(1)) {
            thisCollider.enabled = true;
        }
        else {
            thisCollider.enabled = false;
        }
	}

    public float GetDamage() {
        return damage;
    }

    public Team GetTeam() {
        return team;
    }
}
