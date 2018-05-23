using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public class SniperShoot : MonoBehaviour {

    // GameObject player;

    enum ShootState { seeking, aiming, shooting};
    
    public float damage = 2f;

    public float aimTime = 0.5f;
    float aimCount = 0f;

    public float shootDelay = 0.1f;
    float shootCount = 0f;

    ShootState state = ShootState.seeking;
    ShootState State { get { return state; } set { state = value; LineColor(state); } }

    RaycastHit2D hit;

    public float offset = 0.6f;

    public LayerMask mask;

    LineRenderer line;

    FacePlayer facePlayer;

    public Text debugText;

    // Use this for initialization
    void Start () {
        // player = GameManager.Instance.player;
        // mask = GameManager.Instance.playerMask;
        line = GetComponent<LineRenderer>();
        facePlayer = GetComponent<FacePlayer>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        CheckHit();

		if (state == ShootState.aiming && aimCount > aimTime) {
            State = ShootState.shooting;
            aimCount = 0f;
            /* if (facePlayer)
                facePlayer.enabled = false; */
        }
        else if (state == ShootState.aiming) {
            aimCount += Time.deltaTime;
        }
        else if (aimCount > 0f) {
            aimCount -= Time.deltaTime * 0.5f;
            Mathf.Clamp(aimCount, 0f, aimTime);
        }

        if (state == ShootState.shooting && shootCount < shootDelay) {
            shootCount += Time.deltaTime;
        }
        else if (state == ShootState.shooting) {
            shootCount = 0f;
            aimCount = 0f;
            Shoot();
            State = ShootState.seeking;
            /* if (facePlayer)
                facePlayer.enabled = true; */
        }

    }

    void FixedUpdate() {
    }

    void CheckHit() {
        hit = Physics2D.Raycast(transform.position + (transform.up * offset), transform.up, 200f, mask);

        GameObject go = hit.collider ? hit.collider.gameObject : null;

        ShowLine(hit.distance);

        if (state != ShootState.shooting && go && go.CompareTag("Player")) {
            State = ShootState.aiming;
        }
        else if (state != ShootState.shooting) {
            State = ShootState.seeking;
        }
    }

    void ShowLine(float dist) {
        // Vector3 the_line = transform.up * dist;
        line.SetPosition(0, new Vector3(0f, offset, 0f));
        line.SetPosition(1, new Vector3(0f, dist + offset, 0f));
    }

    void LineColor(ShootState _state) {
        Color toSet;
        switch (_state) {
            case ShootState.aiming:
                toSet = Color.green;
                debugText.text = "Aiming";
                break;
            case ShootState.shooting:
                toSet = Color.blue;
                debugText.text = "Shooting";
                break;
            case ShootState.seeking:
            default:
                toSet = Color.red;
                debugText.text = "Seeking";
                break;
        }
        line.startColor = toSet;
        line.endColor = toSet;
    }

    void Shoot() {
        hit = Physics2D.Raycast(transform.position + (transform.up * offset), transform.up, 200f, mask);
        GameObject go = hit.collider ? hit.collider.gameObject : null;
        if (go && go.CompareTag("Player")) {
            Killable kill = go.GetComponent<Killable>();
            kill.Damage(damage);
        }
    }
}
