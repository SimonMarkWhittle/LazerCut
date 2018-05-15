using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class DieAfterParticles : MonoBehaviour {

    ParticleSystem system;

	// Use this for initialization
	void Start () {
        system = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!system.isEmitting) {
            ParticleSystem.SubEmittersModule sub = system.subEmitters;

            bool done = true;
            for (int i = 0; i < sub.subEmittersCount; i++) {
                ParticleSystem subemitter = sub.GetSubEmitterSystem(i);
                done = done && subemitter.isEmitting;
            }

            if (done)
                Destroy(gameObject);
        }
	}
}
