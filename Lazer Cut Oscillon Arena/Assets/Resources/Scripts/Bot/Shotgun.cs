using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : BulletShooter {

    [Tooltip("Number of bullet fired in one shot")]
    [Range(1, 100)]
    public int blastcount = 5;

    [Tooltip("Value in degrees between 0 and 360. Arc centered on transform.up")]
    [Range(0f, 360f)]
    public float blastArc = 25f;

    protected override void Shoot() {
        if (blastcount == 0) { return;  }

        float step = blastArc / blastcount;
        float angleCount = 0f;

        bool even = blastcount % 2 == 0;

        int toBlast = blastcount;

        if (!even) {
            SpawnBullet(transform.rotation);
            toBlast--;
        }

        float faceDeg = transform.rotation.eulerAngles.z;

        for (int i = 0; i < toBlast; i+=2) {
            angleCount += step;

            SpawnBullet(Quaternion.Euler(0f, 0f, angleCount + faceDeg));
            SpawnBullet(Quaternion.Euler(0f, 0f, (-angleCount) + faceDeg));
        }
    }

    void SpawnBullet(Quaternion direction) {
        GameObject newBullet = Instantiate(bullet, transform.position, direction);

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
