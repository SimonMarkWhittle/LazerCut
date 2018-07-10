using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBeam : BeamShoot {

    public float interval = 1f;
    float intervalCount = 0f;

    public float shootTime = 1f;
    float shootCount = 0f;

	// Update is called once per frame
	void Update () {
		if (intervalCount > interval) {
            On();
            intervalCount = 0f;
        }
        else if (!on) {
            intervalCount += Time.deltaTime;
        }

        if (on && shootCount > shootTime) {
            Off();
            shootCount = 0f;
        }
        else if (on) {
            shootCount += Time.deltaTime;
        }
	}
}
