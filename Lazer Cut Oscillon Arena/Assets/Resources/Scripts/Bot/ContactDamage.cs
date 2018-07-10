using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ContactDamage : MonoBehaviour {  // ,IDamager

    public Team team = Team.bots;
    public float damage;

    public bool killOnContact = false;

    Killable self;

    void Start() {
        if (killOnContact)
            self = GetComponent<Killable>();

        if (killOnContact && !self) {
            throw new System.Exception("Must have Killable component to kill on contact");
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        GameObject obj = collision.collider.gameObject;
        Killable kill = obj.GetComponent<Killable>();

        if (kill && kill.team != team) {
            kill.Damage(damage);

            if (killOnContact) {
                self.Die();
            }
        }
    }
}
