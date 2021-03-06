﻿using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using Photon.Realtime;
using System.Linq;
using System;

public class RoomController : MonoBehaviourPunCallbacks
{
    // player instance prefab, must be located in the Resources folder    
    [SerializeField] GameObject m_playerPrefab;
    // player spawn point    
    [SerializeField] List<Transform> m_transforms;
    private List<Player> m_players = new List<Player>(PhotonNetwork.PlayerList.Length);

    private bool playerSpawned = false;

    void Start()
    {
        m_transforms = m_transforms.OrderBy(x => Guid.NewGuid()).ToList();
        // in case we started this scene with the wrong scene being active, simply load the menu scene        
        if (PhotonNetwork.CurrentRoom == null)
        {
            Debug.Log("Is not in the room, returning back to Lobby");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
            return;
        }
    }

    private void Update()
    {
        if(!playerSpawned)
        {
            Transform spawnPoint = m_transforms[new System.Random().Next(0, m_transforms.Count)];
            // spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate  
            PhotonNetwork.Instantiate(m_playerPrefab.name, spawnPoint.position, Quaternion.identity, 0);
            playerSpawned = true;
        }
    }
    void OnGUI()
    {
        if (PhotonNetwork.CurrentRoom == null) return;
        // leave this Room        
        if (GUI.Button(new Rect(5, 5, 125, 25), "Leave Room")) { PhotonNetwork.LeaveRoom(); }
        // show the Room name        
        GUI.Label(new Rect(135, 5, 200, 25), PhotonNetwork.CurrentRoom.Name);
        // show the list of the players connected to this Room        
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            // show if this player is a Master Client. There can only be one Master Client per Room so use this to define the authoritative logic etc.)            
            string isMasterClient = (PhotonNetwork.PlayerList[i].IsMasterClient ? ": MasterClient" : "");
            GUI.Label(new Rect(5, 35 + 30 * i, 200, 25), PhotonNetwork.PlayerList[i].NickName + isMasterClient);
        }
    }

    public override void OnLeftRoom()
    {
        // left the Room, return back to the GameLobby
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");

    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Joined");
    }



    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("PlayerLeft");

        base.OnPlayerLeftRoom(otherPlayer);
        m_players.Remove(otherPlayer);
    }

}