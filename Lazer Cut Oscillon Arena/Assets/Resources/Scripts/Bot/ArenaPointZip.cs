using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ArenaPointZip : MonoBehaviour {

    Transform spot;

    Vector3 dest = new Vector3();

    bool atDest = true;

    public float speed = 1f;

    public float waitTime = 1f;
    float waitCount = 0f;

    public bool holdOnMove = false;

    bool startedOff;

    Rigidbody2D rb;

    BulletShooter shooter;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        shooter = GetComponent<BulletShooter>();
        if (shooter)
            startedOff = !shooter.enabled;
	}
	
	// Update is called once per frame
	void Update () {
        if (atDest && waitCount > waitTime) {
            //dest = Random.insideUnitCircle.normalized * 15f;
            spot = BotManager.Instance.GetSpot();
            dest = spot.position;
            atDest = false;
            if (holdOnMove && shooter && !startedOff)
                shooter.enabled = false;
        }
        else if (!atDest){
            Vector3 diff = (dest - transform.position);
            if (diff.magnitude <= speed * Time.deltaTime) {
                rb.velocity = diff;
            }
            else {
                rb.velocity = diff.normalized * speed;
            }

            if (Vector3.Distance(transform.position, dest) < 0.1) {
                atDest = true;
                waitCount = 0f;
                rb.velocity = new Vector3(0f, 0f, 0f);
                BotManager.Instance.ReturnSpot(spot);
                spot = null;
            }
        }
        else {
            waitCount += Time.deltaTime;
            if (shooter && !shooter.enabled && !startedOff) {
                shooter.enabled = true;
            }
        }
        // Debug.Log("Wait Count" + waitCount);
    }

    public void Zip() {

    }
}
