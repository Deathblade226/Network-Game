using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
[SerializeField] float m_speed = 5.0f; 
[SerializeField] float m_strafeSpeed = 5.0f; 
[SerializeField] float m_turnSpeed = 5.0f; 
[SerializeField] Animator m_animator = null;
[SerializeField] Rigidbody m_rb = null;

void FixedUpdate() { 

	Vector3 torque = Vector3.zero;
    torque.x = Input.GetAxis("Horizontal");
    Vector3 velocity = Vector3.zero;
    velocity.z = Input.GetAxis("Vertical");

	float mod = 1f + Input.GetAxis("Fire3");

	m_rb.AddRelativeForce(velocity * m_speed * ( velocity.z > 0 ? mod : 1 ) * Time.deltaTime, ForceMode.VelocityChange);
	m_rb.AddRelativeForce(torque * m_speed * Time.deltaTime, ForceMode.VelocityChange);
	//m_rb.AddRelativeTorque(torque * m_turnSpeed * Time.deltaTime);

	m_animator.SetFloat("Speed X", Input.GetAxis("Vertical") * ( velocity.z > 0 ? mod : 1 ));
	m_animator.SetFloat("Speed Z", Input.GetAxis("Horizontal") * mod);

	m_animator.SetBool("Moving", (velocity.z != 0 || torque.x != 0));

} 

}
