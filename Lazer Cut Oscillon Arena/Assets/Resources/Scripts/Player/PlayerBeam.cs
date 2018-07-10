using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeam : BeamShoot {

    float charge = 1f;

    // max length of shot in seconds before charge depleted
    float shotLength = 2f;

    float shotRatio;

    // time in second to fully recharge
    float coolTime = 1f;

	// Use this for initialization
	void Start () {
        shotRatio = charge / shotLength;
	}
	
	// Update is called once per frame
	void Update () {
        if (!on && Input.GetMouseButtonDown(0))
        {
            On();
        }
        else if (!on)
        {
            charge += coolTime * Time.deltaTime;
        }
        else if (on)
        {
            charge -= shotRatio * Time.deltaTime;
        }
	}
}
