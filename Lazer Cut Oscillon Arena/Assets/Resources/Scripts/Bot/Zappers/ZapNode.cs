using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer), typeof(Collider2D))]
public class ZapNode : MonoBehaviour {

    protected bool isRoot = false;
    bool isLeaf = true;

    ZapNode parent;
    Zapper root;

    ZapNode strikePartner;

    ZapNode[] children = new ZapNode[2];

    LineRenderer line;

    bool striking = false;
    float strikeCount = 0f;

    RaycastHit2D hit;


    void Awake() {
        line = GetComponent<LineRenderer>();
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (!striking && isLeaf && ZapManager.nodeExtend) {
            CheckStrike();
            if (!striking)
                Extend();
        }

        if (!striking && ZapManager.nodesDie) {
            Destroy(gameObject);
        }

        if (striking && strikeCount > ZapManager.Instance.strikeTime) {
            Destroy(gameObject);  // dies after strike prolly just need this
            // end strike
        }
        else if (striking) {
            strikeCount += Time.deltaTime;
            // raycast for player and zap 'em

        }
    }

    void Extend() {
        isLeaf = false;

        Vector3 zapDirect = ZapManager.GetZapDirect(transform.position);

        float roll = Random.Range(0, 100);
        if (roll <= ZapManager.Instance.nodeSplitChance) {
            // make two child nodes
            Debug.Log("oh baby a double");
            Vector3 direct1 = Vector3.RotateTowards(zapDirect, transform.right, ZapManager.splitRad, 0f);
            Vector3 direct2 = Vector3.RotateTowards(zapDirect, -transform.right, ZapManager.splitRad, 0f);
            MakeNode(direct1);
            MakeNode(direct2);
        }
        else {
            // make one child node
            MakeNode(zapDirect);
        }
    }

    void MakeNode(Vector3 direct) {
        GameObject prefab = ZapManager.Instance.nodePrefab;

        GameObject newNodeObj = Instantiate(prefab,
            transform.position + direct * ZapManager.Instance.nodeStep,
            Quaternion.identity, transform);

        ZapNode newNode = newNodeObj.GetComponent<ZapNode>();
        newNode.SetParent(this);
    }

    void CheckStrike() {
        // check if can strike

        GameObject[] nodes = ProximityDetect.FindBots(transform.position,
            ZapManager.nodeStrikeRadius,
            ZapManager.Instance.nodeMask);

        foreach (GameObject nObj in nodes) {
            ZapNode nZap = nObj.GetComponent<ZapNode>();
            if (nZap.root.id != root.id) {
                strikePartner = nZap;
                StrikeLine();
                Strike();
                break;
            }
        }

    }

    void StrikeLine() {
        /* 
        strikeLine = gameObject.AddComponent<LineRenderer>();
        strikeLine.SetPosition(0, transform.position);
        strikeLine.SetPosition(1, strikePartner.transform.position);
        strikeLine.startColor = ZapManager.Instance.strikeColor;
        strikeLine.endColor = ZapManager.Instance.strikeColor;
        */

        GameObject prefab = ZapManager.Instance.nodePrefab;

        GameObject newNodeObj = Instantiate(prefab, transform.position,
            Quaternion.identity, transform);

        ZapNode newNode = newNodeObj.GetComponent<ZapNode>();
        newNode.SetParent(strikePartner);
        newNode.Strike();
    }

    public void SetParent(ZapNode _node) {
        parent = _node;
        SetupLine(_node.gameObject);
        root = parent.root;
    }

    public void SetRoot(Zapper _zap) {
        root = _zap;
    }

    public void MakeRoot(Zapper _zap) {
        isRoot = true;
        SetRoot(_zap);
        SetupLine(_zap.gameObject);
    }

    public void SetupLine(GameObject obj) {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, obj.transform.position);
        line.startColor = ZapManager.Instance.seekColor;
        line.endColor = ZapManager.Instance.seekColor;
    }

    void Strike() {
        Debug.Log("STRIKE");
        striking = true;
        strikeCount = 0f;
        if (parent) {
            Debug.Log("ParentStrike");
            parent.Strike();
        }
        line.startColor = ZapManager.Instance.strikeColor;
        line.endColor = ZapManager.Instance.strikeColor;
    }
}
