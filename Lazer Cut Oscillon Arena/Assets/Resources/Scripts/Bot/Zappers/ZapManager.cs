using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZapManager : MonoBehaviour {
    enum ZapState { Idle, Seeking, Striking }
    ZapState state = ZapState.Idle;

    public GameObject nodePrefab;

    public static List<Zapper> zaps = new List<Zapper>();
    static int nextZapperID = 0;

    public float zapTime = 10f;
    static float zapCount = 0f;

    public float nodeTime = 0.1f;
    public static float nodeCount = 0f;

    [Range(0, 100)]
    public int nodeSplitChance = 30;

    public float nodeStep = 0.1f;

    public int nodeSteps = 10;
    int nodeStepCount = 0;

    public float strikeTime = 0.5f;

    public static bool nodeExtend = false;
    public static bool nodesDie = false;

    public static float nodeStrikeRadius = 0.5f;
    public float splitDegrees = 30f;
    public static float splitRad;

    public LayerMask nodeMask;

    public Color seekColor;
    public Color strikeColor;

    #region SINGLETON
    static bool created = false;
    public static ZapManager Instance;
    #endregion

    void Awake() {
        if (!created) {
            Instance = this;
            created = true;
        }
        splitRad = splitDegrees * Mathf.Deg2Rad;
    }
	
	// Update is called once per frame
	void Update () {
        if (nodeExtend) { nodeExtend = false; }
        if (nodesDie) { nodesDie = false; }

        if (state == ZapState.Idle && zapCount > zapTime) {
            foreach (Zapper zap in zaps) {
                zap.Zap();
            }
            zapCount = 0f;
            state = ZapState.Seeking;
        }
        else if (state == ZapState.Idle)
            zapCount += Time.deltaTime;

        if (state == ZapState.Seeking && nodeStepCount >= nodeSteps) {
            state = ZapState.Idle;
            nodeStepCount = 0;
            nodesDie = true;
        }
        else if (state == ZapState.Seeking && nodeCount > nodeTime) {
            nodeExtend = true;
            nodeCount = 0f;
            nodeStepCount++;
        }
        else if (state == ZapState.Seeking)
            nodeCount += Time.deltaTime;

        /*
        if (state == ZapState.Striking && strikeCount > strikeTime) {
            state = ZapState.Idle;
            strikeCount = 0f;
        }
        else if (state == ZapState.Striking)
            strikeCount += Time.deltaTime;
        */
	}

    public static Vector3 GetZapDirect(Vector3 pos) {
        Vector3 zapDirect = new Vector3();
        foreach (Zapper zap in zaps) {
            GameObject obj = zap.gameObject;
            Vector3 diff = (obj.transform.position - pos);

            zapDirect += (diff * (100 - diff.magnitude));
        }

        return zapDirect.normalized;
    }

    public static void RegisterZap(Zapper _zap) {
        zaps.Add(_zap);
        _zap.id = nextZapperID++;
    }

    public static bool RemoveZap(Zapper _zap) {
        return zaps.Remove(_zap);
    }
}
