using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{
    [SerializeField] private string VersioName = "0.1";
    [SerializeField] private GameObject UsernameMeniu = null;
    [SerializeField] private GameObject ConnectPanel = null;

    [SerializeField] private InputField UsernameInput = null;
    [SerializeField] private InputField CreateGameInput = null;
    [SerializeField] private InputField JoinGameInput = null;

    [SerializeField] private GameObject StartButton = null;

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings(VersioName);
    }

    private void Start()
    {
        UsernameMeniu.SetActive(true);
    }

    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Connected");

    }

    public void ChangeUserNameInput()
    {
        if (UsernameInput.text.Length >= 3)
        {
            StartButton.SetActive(true);
        }
        else
        {
            StartButton.SetActive(false);
        }
    }

    public void SetUserName()
    {
        UsernameMeniu.SetActive(false);
        PhotonNetwork.playerName = UsernameInput.text;
    }

    public void CreateGame()
    {
        PhotonNetwork.CreateRoom(CreateGameInput.text, new RoomOptions() { MaxPlayers = 2 }, null);
    }

    public void JoinGame()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom(JoinGameInput.text, roomOptions, TypedLobby.Default);
    }

    private void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("MainGame");
    }
}
