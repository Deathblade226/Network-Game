using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationController : Navigation {

[SerializeField] AttackNav attackNav = null;
[SerializeField] TravelNav travelNav = null;
[SerializeField] WanderNav wanderNav = null;
public AttackNav AttackNav { get => attackNav; set => attackNav = value; }

private NavMeshPath navPath ;
private GameObject objective = null;

void Start() {
    navPath = new NavMeshPath();
    if (attackNav != null) attackNav.Nc = this;
    if (wanderNav != null) wanderNav.Nc = this;
    if (travelNav != null) {
    travelNav.Nc = this;
    objective = AIUtilities.GetNearestGameObject(gameObject, travelNav.TargetTag, xray:true);
    }
    StartCoroutine(MonsterLogic());
}
private void Update() {
    if (Animator != null) Animator.SetFloat("Speed", Agent.velocity.magnitude);        
}

IEnumerator MonsterLogic() { 
    if (attackNav != null && attackNav.Target != "" && !attackNav.Active) { travelNav.Moving = false; wanderNav.StopWander(); attackNav.StartAttacking(); 
    } else if (objective != null && !travelNav.Moving && !attackNav.Active) { wanderNav.StopWander(); travelNav.StartTravel();  
    } else if (!wanderNav.Active && !travelNav.Moving && !attackNav.Active) { wanderNav.StartWander(); travelNav.Moving = false; }
    else { Debug.Log("No other nav options"); }
yield return null; }

}
