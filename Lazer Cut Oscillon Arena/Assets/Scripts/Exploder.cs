using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour, IObserver {

    public Team team = Team.bots;

    public float damage = 2f;

    public float radius = 5f;

    public float explodeDelay = 1f;
    float explodeCounter = 0f;

    bool armed = false;

    public GameObject explodeParticles;

    LayerMask mask;

    SpriteRenderer sprite;
    Color original;

    Advance adv;

    // Use this for initialization
    void Start () {
        mask = GameManager.Instance.GetMask(team);

        sprite = GetComponentInChildren<SpriteRenderer>();
        if (!sprite)
            sprite = GetComponent<SpriteRenderer>();

        if (sprite) {
            Debug.Log("Found sprite!");
            original = sprite.color;
        }

        ProximityDetect detector = GetComponent<ProximityDetect>();
        if (detector)
            detector.AddObserver(this);

        adv = GetComponent<Advance>();
	}
	
    void Update () {
        if (armed && explodeCounter > explodeDelay) {
            GameObject[] targets = FindTargets();
            Explode(targets);
        }
        else if (armed) {
            explodeCounter += Time.deltaTime;

            if (sprite) {
                float newR = Mathf.Lerp(original.r, 1f, explodeCounter / explodeDelay);
                float newG = Mathf.Lerp(original.g, 1f, explodeCounter / explodeDelay);
                float newB = Mathf.Lerp(original.b, 1f, explodeCounter / explodeDelay);
                Color newC = new Color(newR, newG, newB);
                sprite.color = newC;
            }
        }
        if (armed && (explodeCounter / explodeDelay) >= 0.5f) {
            if (adv) {
                adv.Halt();
                Debug.Log("HALT!");
            }
        }
    }

    void Explode(GameObject[] targets) {
        foreach (GameObject target in targets) {
            Killable kill = target.GetComponent<Killable>();
            if (kill)
                kill.Damage(damage);
        }

        Instantiate(explodeParticles, transform.position, Quaternion.identity);

        Killable self = GetComponent<Killable>();
        if (self)
            self.Die();
    }

    GameObject[] FindTargets() {
        if (team == Team.bots)
            return ProximityDetect.FindPlayer(transform.position, radius);
        else if (team == Team.players)
            return ProximityDetect.FindBots(transform.position, radius, transform.forward, mask);
        else
            return new GameObject[] { };
    }

    public void Ping(Dictionary<string, object> qwargs) {   //  TODO: do we need the if?
        if (qwargs.ContainsKey("targets") && explodeDelay == 0f) {
            GameObject[] targets = qwargs["targets"] as GameObject[];
            Explode(targets);
        }
        else
            armed = true;
    }
}
