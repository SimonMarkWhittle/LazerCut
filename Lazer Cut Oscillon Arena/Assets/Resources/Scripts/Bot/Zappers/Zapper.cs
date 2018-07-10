using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zapper : MonoBehaviour {

    public int id = -1;

    public float damage = 1f;

    bool shouldZip = false;

    void Awake() {
        ZapManager.RegisterZap(this);
    }

	// Use this for initialization
	void Start () {
        // zaps.Add(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Zap() {

        Vector3 zapDirect = ZapManager.GetZapDirect(transform.position);

        GameObject prefab = ZapManager.Instance.nodePrefab;

        GameObject newNodeObj = Instantiate(prefab,
            transform.position + zapDirect * ZapManager.Instance.nodeStep,
            Quaternion.identity, transform);

        ZapNode newNode = newNodeObj.GetComponent<ZapNode>();
        newNode.MakeRoot(this);

        Debug.Log("zap zap");
    }

    void OnDestroy() {
        ZapManager.RemoveZap(this);
    }
}
