using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour {

[SerializeField] NavMeshAgent agent = null;
[SerializeField] Animator animator = null;
[SerializeField] bool m_x = true;
[SerializeField] bool m_z = true;
[SerializeField] bool seeThroughWalls = false;
[SerializeField] float fov = 180.0f;
[SerializeField] float range = 10.0f;
public NavMeshAgent Agent { get => agent; set => agent = value; }
public Animator Animator { get => animator; set => animator = value; }
public bool X { get => m_x; set => m_x = value; }
public bool Z { get => m_z; set => m_z = value; }
public bool SeeThroughWalls { get => seeThroughWalls; set => seeThroughWalls = value; }
public float Fov { get => fov; set => fov = value; }
public float Range { get => range; set => range = value; }
}
