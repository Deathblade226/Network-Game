using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
[SerializeField] float m_speed = 5.0f; 
[SerializeField] float m_turnSpeed = 5.0f; 
[SerializeField] Animator m_animator = null;

void Update() { 
	Vector3 forward = (Input.GetAxis("Vertical") * transform.forward * m_speed * Time.deltaTime);
	transform.Translate(forward);
	//transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * m_turnSpeed * Time.deltaTime));
	m_animator.SetFloat("Speed", Input.GetAxis("Vertical"));
} 

}
