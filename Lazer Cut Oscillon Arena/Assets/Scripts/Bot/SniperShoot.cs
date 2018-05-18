using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SniperShoot : MonoBehaviour {

    // GameObject player;

    public float damage = 2f;

    public float aimTime = 0.5f;
    float aimCount = 0f;

    bool shooting = false;
    public float shootDelay = 0.1f;
    float shootCount = 0f;

    RaycastHit2D hit;

    public float offset = 0.5f;

    public LayerMask mask;

    LineRenderer line;

    // Use this for initialization
    void Start () {
        // player = GameManager.Instance.player;
        // mask = GameManager.Instance.playerMask;
        line = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (aimCount > aimTime) {
            shooting = true;
            line.startColor = Color.blue;
            line.endColor = Color.blue;
            aimCount = 0f;
        }

        if (shooting && shootCount < shootDelay) {
            shootCount += Time.deltaTime;
        }
        else if (shooting) {
            shootCount = 0f;
            shooting = false;
            Shoot();
        }
	}

    void FixedUpdate() {
        if (shooting) { return; }

        hit = Physics2D.Raycast(transform.position, transform.up, 100f, mask);

        GameObject go = hit.collider ? hit.collider.gameObject : null;
        if (go && go.CompareTag("Player")) {
            aimCount += Time.fixedDeltaTime;
            line.startColor = Color.green;
            line.endColor = Color.green;
        }
        else {
            aimCount -= Time.fixedDeltaTime * 0.5f;
            line.startColor = Color.red;
            line.endColor = Color.red;
        }

        ShowLine(hit.distance);
    }

    void ShowLine(float dist) {
        // Vector3 the_line = transform.up * dist;
        line.SetPosition(0, new Vector3(0f, offset, 0f));
        line.SetPosition(1, new Vector3(0f, dist, 0f));
    }

    void Shoot() {
        hit = Physics2D.Raycast(transform.position, transform.up, 100f, mask);
        GameObject go = hit.collider ? hit.collider.gameObject : null;
        if (go && go.CompareTag("Player")) {
            Killable kill = go.GetComponent<Killable>();
            kill.Damage(damage);
        }
    }
}
