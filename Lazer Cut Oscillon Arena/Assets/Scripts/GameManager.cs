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



    public LayerMask playerMask;
    public LayerMask botMask;

    void Awake() {
        instance = this;
        doneMessage.enabled = false;
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

    public LayerMask GetMask(Team team) {
        if (team == Team.bots)
            return playerMask;
        else if (team == Team.players)
            return botMask;
        else
            return new LayerMask();
    }
}
