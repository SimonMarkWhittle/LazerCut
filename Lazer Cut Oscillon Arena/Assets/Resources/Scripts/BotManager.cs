using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Gate {
    floor, wall,
    outerFloor, innerFloor,
    N, S, E, W, NE, NW, SE, SW,
    RN, LN, RS, LS,
    TE, BE, TW, BW,
    TNW, BNW, TNE, BNE,
    TSW, BSW, TSE, BSE,
    FN, FS, FE, FW,
    FNE, FNW, FSE, FSW,
    FNNE, FENE, FNNW, FWNW,
    FESE, FSSE, FWSW, FSSW
}

public enum Bot {
    seeker, seekerSwarm,
    exploder, exploderSwarm,
    shooter, tracker,
    shotgun, sniper,
    beamer, bouncy,
}

public class BotManager : MonoBehaviour {

    public List<Transform> botSpots = new List<Transform>(32);

    #region SINGLETON

    static BotManager instance;
    public static BotManager Instance { get { return instance; } }

    #endregion

    public SerializableDictionary<Bot, GameObject> botPrefabs;
    public SerializableDictionary<Gate, Transform> gateTransforms;

    // public BotsDict botPrefabs;
    // public GatesDict gateTransforms;

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

    public void SpawnBot(Bot _bot, Gate _gate) {
        GameObject prefab = botPrefabs[_bot];
        Transform location = gateTransforms[_gate];
        Instantiate(prefab, location.position, Quaternion.identity);
    }
}

public class Phase {

    List<BotSpawn> spawns = new List<BotSpawn>();



}

public class BotSpawn {
    bool done = false;
    bool Done { get { return done; } }

    Bot bot;

    Gate gate;

    bool spawnReady = false;

    public void Spawn() {
        if (!spawnReady) { return; }
    }

}

public class MultiSpawn : BotSpawn {



}
