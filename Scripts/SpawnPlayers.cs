using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    public GameObject hero;
   // public float minX, minY, maxX, maxY;
    void Start()
    {
        Vector2 position = new Vector2(-4.4f, -0.72f);
        
        // Vector2 randomPosition = new Vector2(Random.Range(minX, minY),Random.Range(maxX, maxY));
        PhotonNetwork.Instantiate("Hero 2", position, Quaternion.identity);
       
    }
   
    public void Leave()
    {
        SceneManager.LoadScene(2);
        PhotonNetwork.LeaveRoom();
        
    }
    public override void OnLeftRoom()
    {

        SceneManager.LoadScene(2);
    }

}
