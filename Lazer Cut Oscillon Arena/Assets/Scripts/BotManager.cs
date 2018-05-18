using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotManager : MonoBehaviour {

    public List<Transform> botSpots = new List<Transform>(32);

    #region SINGLETON

    static BotManager instance;
    public static BotManager Instance { get { return instance; } }

    #endregion

    void Awake() {
        instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Transform GetSpot() {
        /*
        int range = realSpots.Count;
        Transform spot = realSpots[Random.Range(0, range)];
        realSpots.Remove(spot);
        */
        int range = botSpots.Count;
        Transform spot = botSpots[Random.Range(0, range)];
        botSpots.Remove(spot);
        return spot;
    }

    public void ReturnSpot(Transform _spot) {
        // realSpots.Add(_spot);
        botSpots.Add(_spot);
    }
}
