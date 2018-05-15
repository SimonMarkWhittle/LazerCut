using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrackerBullet : Bullet {

    Transform player;

    public float maxDegrees = 0f;
    float maxRad;

    bool tracking = true;

    protected override void Start() {
        base.Start();
        if (GameManager.Instance.player)
            player = GameManager.Instance.player.transform;
    }

    // Update is called once per frame
    void Update () {
		if (!player || !tracking) { return; }

        Vector3 difference = (player.transform.position - transform.position);

        if (difference.magnitude < 2f)
            tracking = false;

        Vector3 direction = difference.normalized;

        if (maxRad == 0f) {
            rb.velocity = direction * speed;
        }
        else {
            Vector3 newLook = Vector3.RotateTowards(rb.velocity, direction, maxRad, 0f);
            rb.velocity = newLook * speed;
        }

        
    }
}
