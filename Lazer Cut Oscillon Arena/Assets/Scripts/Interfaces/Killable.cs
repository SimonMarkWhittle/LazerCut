using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Killable : MonoBehaviour, IObservable {
    public Team team = Team.bots;

    public float health = 2f;
    protected float maxHealth = 2f;

    public GameObject deathParticles;

    List<IObserver> observers = new List<IObserver>();

    public Color hurtColor;
    SpriteRenderer sprite;

    void Awake() {
        GameManager.Instance.CheckIn(team);
    }

	// Use this for initialization
	protected virtual void Start () {
        maxHealth = health;
        sprite = GetComponentInChildren<SpriteRenderer>();

        if (!sprite)
            sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		if (health <= 0) {
            Die();
        }
	}

    public void Die() {
        GameManager.Instance.CheckOut(team);
        Debug.Log("Die idiot");
        if (deathParticles) {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject);
    }

    public void Damage(float damage) {
        health -= damage;
        if (sprite && team != Team.players) {
            Color old = sprite.color;
            sprite.color = new Color(old.r / 2f, old.g / 2f, old.b / 2f);
        }

        if (observers.Count != 0)
            PingObservers(new Dictionary<string, object> { { "damage", damage } });
    }

    void GetBoink(IDamager damager) {
        if (damager != null && damager.GetTeam() != team) {
            Debug.Log("ow");
            Damage(damager.GetDamage());
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        GetBoink(other.gameObject.GetComponent<IDamager>());
    }

    void OnCollisionEnter2D(Collision2D collision) {
        GetBoink(collision.collider.gameObject.GetComponent<IDamager>());
    }

    public void AddObserver(IObserver _observer) {
        observers.Add(_observer);
    }

    public bool RemoveObserver(IObserver _observer) {
        return observers.Remove(_observer);
    }

    public void PingObservers(Dictionary<string, object> qwargs) {
        foreach (IObserver observer in observers) {
            observer.Ping(qwargs);
        }
    }
}
