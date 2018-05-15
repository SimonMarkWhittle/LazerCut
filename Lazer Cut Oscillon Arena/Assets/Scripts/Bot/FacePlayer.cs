using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour {

    GameObject player;
    public float maxDegrees = 0f;
    float maxRad;

	// Use this for initialization
	void Start () {
        player = GameManager.Instance.player;

        maxRad = (Mathf.PI / 180f) * maxDegrees;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (!player) { return; }

        Vector3 direction = (player.transform.position - transform.position).normalized;

        if (maxRad == 0f) {
            transform.up = direction;
        }
        else {
            /*
            Vector3 newLook = Vector3.RotateTowards(transform.up, direction, maxRad * Time.deltaTime, 0f);
            transform.up = new Vector3(0, 0, newLook.z);
            */
            transform.Rotate(new Vector3(0, 0, (Vector2.SignedAngle(transform.up, direction))).normalized * 50 * Time.deltaTime); 
            //Instead of 50 include some rotation speed var
            //Also, include some logic to prevent jittering
        }
    }
}
