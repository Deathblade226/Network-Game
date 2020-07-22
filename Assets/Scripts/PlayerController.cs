using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
[SerializeField] float m_speed = 5.0f; 
[SerializeField] Animator m_animator = null;

void Update() { 
	Vector3 forward = (Input.GetAxis("Vertical") * transform.forward * m_speed * Time.deltaTime);
	transform.Translate(forward); 
	Vector3 side = (Input.GetAxis("Horizontal") * transform.right * m_speed * Time.deltaTime);
	transform.Translate(side);
	m_animator.SetFloat("Speed", forward.magnitude);
} 

}
