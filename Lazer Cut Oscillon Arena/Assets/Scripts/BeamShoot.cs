using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamShoot : MonoBehaviour {

    public GameObject emitterPrefab;
    BeamEmitter emitter;
    RaycastHit2D hit;

    public float damage;
    public float offset = 0.5f;

    protected bool on = false;

    public LayerMask mask;

	// Use this for initialization
	void Start () {
        GameObject go = Instantiate(emitterPrefab, transform);
        emitter = go.GetComponent<BeamEmitter>();
        if (!emitter) {
            throw new MissingComponentException();
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (on) {
            Fire();
        }
	}

    public void On() {
        emitter.On();
        on = true;
    }

    public void Fire() {
        Vector3 shootPos = transform.position + transform.up * offset;
        hit = Physics2D.Raycast(shootPos, transform.up, 200f, mask);

        if (hit.collider != null) {
            GameObject go = hit.collider.gameObject;
            Killable kill = go.GetComponent<Killable>();

            if (kill) {
                kill.Damage(damage * Time.deltaTime);
            }
            /*
            Rigidbody2D rb = go.GetComponent<Rigidbody2D>();

            if (rb) {
                rb.AddForce(transform.up * damage * Time.deltaTime);
            }
            */
        }
    }

    public void Off() {
        emitter.Off();
        on = false;
    }
}
