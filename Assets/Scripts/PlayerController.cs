using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
[SerializeField] float m_speed = 5.0f; 
[SerializeField] float m_turnSpeed = 5.0f; 
[SerializeField] Animator m_animator = null;
[SerializeField] Rigidbody m_rb = null;

void Update() { 

	Vector3 torque = Vector3.zero;
    torque.y = Input.GetAxis("Horizontal");
    Vector3 velocity = Vector3.zero;
    velocity.z = Input.GetAxis("Vertical");

	float mod = 1f;

	m_rb.AddRelativeForce(velocity * m_speed * mod * Time.deltaTime, ForceMode.VelocityChange);
	m_rb.AddRelativeTorque(torque * m_turnSpeed * Time.deltaTime);

	m_animator.SetFloat("Speed", Input.GetAxis("Vertical") * mod);
} 

}
