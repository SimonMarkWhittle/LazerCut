using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodyEyes : Killable {

    public float heal = 0.1f;

    public SpriteRenderer eyes;

	// Use this for initialization
	protected override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
        float alpha = 0f;
        if (health > 0f) {
            alpha = (maxHealth - health) / maxHealth;
        }

        eyes.color = new Color(eyes.color.r, eyes.color.g, eyes.color.b, alpha);
        // Debug.Log("Health: " + health + "alpha: " + alpha);

        base.Update();

        if (health < maxHealth) {
            health += heal * Time.deltaTime;
        }
        if (health > maxHealth)
            health = maxHealth;


    }
}
