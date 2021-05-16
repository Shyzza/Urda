

using UnityEngine;
using System.Collections;



public class SampleMessageListener : MonoBehaviour
{

    public GameObject gm;
    GameObject obj;
    public GameObject player;
    public static string global ;
    private bool alive = false; 

    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        /*       Debug.Log(msg);
         *    
        */
        global = msg;
        if ((msg == "0;29175EB2" || msg == "0;49795CB2") && alive == false)
        {
            gm.GetComponent<GameManager>().SpawnPlayer(msg);
            alive = true;
        }


    }

    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        if (success)
            Debug.Log("Connection established");
        else
            Debug.Log("Connection attempt failed or disconnection detected");
    }
}
