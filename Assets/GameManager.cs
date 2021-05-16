using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject[] PlayerPrefab;
    public GameObject GameCanvas;
    public GameObject Serial;
    public GameObject SceneCamera;
    public Dictionary<string, string> players = new Dictionary<string, string>();
    private bool Off = false;
    public GameObject disconnectUI;


    public void Awake()
    {
        players.Add("0;29175EB2", "wizard");
        players.Add("0;49795CB2", "wizard");
        //foreach (GameObject playerPrefab in PlayerPrefab)
        //{
        //    players.Add(playerPrefab.name, playerPrefab);
        //}

        //players.Add("0;29175EB2", PlayerPrefab[0]);
        //players.Add("49795CB2", PlayerPrefab[1]);
       // Debug.Log(players["Player"].name);
        GameCanvas.SetActive(true);
        Serial.SetActive(true);

    }

    void Update()
    {
        CheckInput();
  /*      if (Input.GetKeyDown(KeyCode.W))
        {
           SpawnPlayer("0;49795CB2");
        }    
        if (Input.GetKeyDown(KeyCode.D))
        {
           SpawnPlayer("0;29175EB2");
        }*/
    }

    private void CheckInput() {
        if (Off && Input.GetKeyDown(KeyCode.Escape))
        {
            disconnectUI.SetActive(false);
            Off = false;
        } 
        else if (!Off && Input.GetKeyDown(KeyCode.Escape))
        {
            disconnectUI.SetActive(true);
            Off = true;
        }
    }


        public void SpawnPlayer(string id)
        {
        var playerName = players[id];
        float randomValue = Random.Range(-1f, 1f);
        if (PhotonNetwork.isMasterClient == true)
        PhotonNetwork.Instantiate(playerName, new Vector3(569.63f, 248.132f, -5f), Quaternion.identity, 0);
        else
        PhotonNetwork.Instantiate(playerName, new Vector3(584.39f, 249.1f, -5f), Quaternion.identity, 0);
        GameCanvas.SetActive(false);
        Serial.SetActive(false);
        }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("MainMenu");
    }

    [PunRPC]
    public void Disconnec()
    {
        disconnectUI.SetActive(true);
        Off = true;
    }
}
