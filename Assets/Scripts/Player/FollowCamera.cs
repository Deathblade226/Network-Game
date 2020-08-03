using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

[SerializeField] Transform m_target = null;
[SerializeField] float m_distance = 5.0f;
[SerializeField] float m_smoothCam = 10.0f;

float Pitch { get; set; } = 20;
float Yaw { get; set; } = 0;

void Start() {
    Cursor.lockState = CursorLockMode.Locked;       
}

void Update() {

}

private void LateUpdate() {
    if (m_target != null) { 
    Quaternion rotation = m_target.rotation * Quaternion.AngleAxis(Yaw, Vector3.up) * Quaternion.AngleAxis(Pitch, Vector3.right);
    Vector3 newPosition = m_target.position + rotation * new Vector3(0,0, -m_distance);

    if (Physics.Raycast(m_target.position, newPosition - m_target.position, out RaycastHit hit)) {
    transform.position = hit.point;
    }
    transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * m_smoothCam);

    transform.LookAt(m_target);
    }
}

}
