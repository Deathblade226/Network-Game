﻿using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerSync : MonoBehaviourPun, IPunObservable {    


[SerializeField] Animator m_animator = null;
[SerializeField] Transform m_camTarget = null;
[SerializeField] TMP_Text m_namePlate = null;
[SerializeField] PhotonView pv = null;
// list of the scripts that should only be active for the local player (ex. PlayerController, MouseLook etc.)    
public MonoBehaviour[] m_localScripts;    
// list of the GameObjects that should only be active for the local player (ex. Camera, AudioListener etc.)    
public GameObject[] m_localObjects;    
// values that will be synced over network    
Vector3 m_currentPosition;    
Quaternion m_currentRotation;

private void Awake() {
    m_namePlate.text = pv.Owner.NickName;        
}

void Start()    {        
    if (photonView.IsMine) {
    // player is local
    Camera.main.GetComponent<FollowCamera>().Target = m_camTarget;
    } else {
    // player is Remote, deactivate the scripts and object that should only be enabled for the local player            
    for (int i = 0; i < m_localScripts.Length; i++) { m_localScripts[i].enabled = false; }
    for (int i = 0; i < m_localObjects.Length; i++) { m_localObjects[i].SetActive(false); }
    }
}
public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
    if (stream.IsWriting) {
    // local player, send the data            
    stream.SendNext(transform.position);
    stream.SendNext(transform.rotation);
    } else {            
    // network player, receive data            
    m_currentPosition = (Vector3)stream.ReceiveNext();
    m_currentRotation = (Quaternion)stream.ReceiveNext();        
    }    
} 
void Update() { 
    if (!photonView.IsMine) {
    // update remote player (smooth this, this looks good, at the cost of some accuracy)   
    transform.position = Vector3.Lerp(transform.position, m_currentPosition, Time.deltaTime * 5);
    m_animator.SetFloat("Speed X", (transform.position.x - m_currentPosition.x));
    m_animator.SetFloat("Speed Z", (transform.position.z - m_currentPosition.z));
    transform.rotation = Quaternion.Lerp(transform.rotation, m_currentRotation, Time.deltaTime * 5);        
    }    
    if (photonView.IsMine) {
    if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Escape)) { 
    Cursor.lockState = ( Cursor.lockState == CursorLockMode.Locked ) ? CursorLockMode.None : CursorLockMode.Locked; 
    }
    }
}

}