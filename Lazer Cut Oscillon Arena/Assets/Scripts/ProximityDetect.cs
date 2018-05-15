﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityDetect : MonoBehaviour, IObservable {

    public Team team = Team.bots;
    public float radius = 2f;

    Transform player;

    List<IObserver> observers = new List<IObserver>();

    LayerMask mask;

    // Use this for initialization
    void Start () {
        if (team == Team.bots && GameManager.Instance.player)
            player = GameManager.Instance.player.transform;

        mask = GameManager.Instance.GetMask(team);
    }
	
	// Update is called once per frame
	void Update () {
        if (team == Team.bots && player) {
            GameObject[] targets = FindPlayer(transform.position, radius);
            if (targets.Length > 0)
                PingObservers(new Dictionary<string, object> { { "targets", targets } });
        }
    }

    void FixedUpdate() {
        if (team == Team.players) {
            GameObject[] targets = FindBots(transform.position, radius, transform.forward, mask);
            if (targets.Length > 0)
                PingObservers(new Dictionary<string, object> { { "targets", targets } });
        }
    }

    public static GameObject[] FindPlayer(Vector3 _position, float _radius) {
        Transform player = GameManager.Instance.player.transform;
        float dist = (player.position - _position).magnitude;
        if (dist <= _radius) {
            return new GameObject[] { player.gameObject };
        }
        else
            return new GameObject[] { };
    }

    public static GameObject[] FindBots(Vector3 _position, float _radius, Vector3 _direction, LayerMask _mask) {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(_position, _radius, _direction, _mask);

        if (hits.Length > 0) {
            GameObject[] targets = new GameObject[hits.Length];
            for (int i = 0; i < hits.Length; i++) {
                targets[i] = hits[i].collider.gameObject;
            }
            return targets;
        }
        else
            return new GameObject[] { };
    }

    public void PingObservers(Dictionary<string, object> qwargs) {
        foreach (IObserver observer in observers) {
            observer.Ping(qwargs);
        }
    }

    public void AddObserver(IObserver _observer) {
        observers.Add(_observer);
    }

    public bool RemoveObserver(IObserver _observer) {
        return observers.Remove(_observer);
    }
}
