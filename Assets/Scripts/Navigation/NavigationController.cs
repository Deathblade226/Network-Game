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
    attackNav.Nc = this;
    wanderNav.Nc = this;
    travelNav.Nc = this;
    navPath = new NavMeshPath();
    objective = AIUtilities.GetNearestGameObject(gameObject, travelNav.TargetTag, xray:true);
    StartCoroutine(MonsterLogic());
}
private void Update() {
    if (Animator != null) Animator.SetFloat("Speed", Agent.velocity.magnitude);        
}

IEnumerator MonsterLogic() { 
    if (attackNav.Target != "" && !attackNav.Active) { travelNav.Moving = false; wanderNav.StopWander(); attackNav.StartAttacking(); 
    } else if (objective != null && !travelNav.Moving && !attackNav.Active) { wanderNav.StopWander(); travelNav.StartTravel();  
    } else if (!wanderNav.Active && !travelNav.Moving && !attackNav.Active) wanderNav.StartWander(); travelNav.Moving = false;
yield return null; }

}
