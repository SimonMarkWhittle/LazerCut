using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    #region SINGLETON

    static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    #endregion

    public GameObject player;

    int players = 0;
    int bots = 0;

    bool done = false;

    public Text doneMessage;

    public List<Transform> botSpots = new List<Transform>(16);
    List<Transform> realSpots = new List<Transform>();

    public LayerMask playerMask;
    public LayerMask botMask;

    void Awake() {
        instance = this;
        doneMessage.enabled = false;
        for (int i = 0; i < 8; i++) {
            realSpots.Add(botSpots[i]);
        }
    }

    void Update() {
        if (players == 0 || bots == 0) {
            done = true;
            doneMessage.enabled = true;
        }

        if (done && Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene("Tests");
        }
        else if (done && Input.GetKeyDown(KeyCode.Q)) {
            Application.Quit();
        }
    }

    public void CheckIn(Team team) {
        switch(team) {
            case Team.players:
                players++;
                break;
            case Team.bots:
                bots++;
                break;
            default:
                break;
        }
    }

    public void CheckOut(Team team) {
        switch (team) {
            case Team.players:
                players--;
                break;
            case Team.bots:
                bots--;
                break;
            default:
                break;
        }
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

    public LayerMask GetMask(Team team) {
        if (team == Team.bots)
            return playerMask;
        else if (team == Team.players)
            return botMask;
        else
            return new LayerMask();
    }
}
