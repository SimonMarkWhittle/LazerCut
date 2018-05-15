using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class BeamEmitter : MonoBehaviour {

    ParticleSystem system;

	// Use this for initialization
	void Start () {
        system = GetComponent<ParticleSystem>();
        system.Pause();
	}

    public void On() {
        system.Play();
    }

    public void Off() {
        system.Stop();
    }
}
