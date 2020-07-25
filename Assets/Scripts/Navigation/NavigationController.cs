using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PathingDebug))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AttackNav))]
[RequireComponent(typeof(TravelNav))]
[RequireComponent(typeof(WanderNav))]
public class NavigationController : Navigation {

[SerializeField] AttackNav attackNav = null;
[SerializeField] TravelNav travelNav = null;
[SerializeField] WanderNav wanderNav = null;
public AttackNav AttackNav { get => attackNav; set => attackNav = value; }

private NavMeshPath navPath ;
private GameObject objective = null;

private void Awake() {
    Agent = gameObject.GetComponent<NavMeshAgent>();
    attackNav = gameObject.GetComponent<AttackNav>();        
    travelNav = gameObject.GetComponent<TravelNav>();        
    wanderNav = gameObject.GetComponent<WanderNav>();
}

void Start() {
    navPath = new NavMeshPath();
    if (attackNav != null) attackNav.Nc = this;
    if (wanderNav != null) wanderNav.Nc = this;
    if (travelNav != null) {
    travelNav.Nc = this;
    objective = AIUtilities.GetNearestGameObject(gameObject, travelNav.TargetTag, xray:true);
    }
}
private void Update() {
    StartCoroutine(MonsterLogic());
    if (Animator != null) Animator.SetFloat("Speed", Agent.velocity.magnitude);
}

IEnumerator MonsterLogic() {
    GameObject target = AIUtilities.GetNearestGameObject(gameObject, attackNav.target, Range, Fov);

    if (target != null) { 
    
    //Debug.Log("Out1"); 
    travelNav.Moving = false; 
    wanderNav.StopWander(); 
    Agent.SetDestination(target.transform.position); 
    
    } else if (attackNav != null && attackNav.Target != "" && !attackNav.Active) { 

    //Debug.Log("Out2"); 
    travelNav.Moving = false; 
    wanderNav.StopWander(); 
    attackNav.StartAttacking(); 
    
    } else if (objective != null && !travelNav.Moving && !attackNav.Active) { 

    //Debug.Log("Out3");
    wanderNav.StopWander(); 
    travelNav.StartTravel();  
    
    } else if (!wanderNav.Active && !travelNav.Moving && !attackNav.Active) { 

    //Debug.Log("Out4"); 
    wanderNav.StartWander(); 
    travelNav.Moving = false; 

    } else { Debug.Log("No other nav options"); }
yield return null; }

private void OnDestroy() {
    StopCoroutine(MonsterLogic());
}

}
