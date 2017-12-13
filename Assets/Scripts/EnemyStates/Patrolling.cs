using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : EnemyState
{
	int currentPatrolPoint = 0;

	private Coroutine patrollingCoroutine;


	public Patrolling (EnemyController enemy)
	{		
		this.enemy = enemy;
		patrollingCoroutine = enemy.StartCoroutine (Patrol ());
	}

	public override void OnEnter ()
	{
		
	}

	public override void OnExit ()
	{
		enemy.StopCoroutine (patrollingCoroutine);
		enemy.navMeshAgent.ResetPath ();
	}

	public override void Pause ()
	{
		enemy.StopCoroutine (patrollingCoroutine);
		enemy.navMeshAgent.ResetPath ();
	}

	public override void Resume ()
	{
		patrollingCoroutine = enemy.StartCoroutine (Patrol ());
	}

	public override void Update ()
	{
		base.Update ();
	}

	private IEnumerator Patrol ()
	{
		YieldInstruction endOfFrame = new WaitForEndOfFrame ();
		while (true) {
			if (!enemy.gamePaused && !enemy.gameEnded) {
				enemy.navMeshAgent.SetDestination (PatrolPointsController.GetPointFor (enemy.number, currentPatrolPoint).position);
				while (enemy.navMeshAgent.pathPending) {
					yield return endOfFrame;
				}
				while (Vector3.Distance (enemy.navMeshAgent.transform.position, PatrolPointsController.GetPointFor (enemy.number, currentPatrolPoint).position) > 0.5f) {
//					Debug.Log (Vector3.Distance (enemy.navMeshAgent.transform.position, PatrolPointsController.GetPointFor (enemy.number, currentPatrolPoint).position));
					if (!enemy.gamePaused && !enemy.gameEnded) {
						yield return endOfFrame;
					} else {
						enemy.navMeshAgent.ResetPath ();
						enemy.navMeshAgent.velocity = Vector3.zero;
						break;
					}
				}
				if (enemy.gamePaused && !enemy.gameEnded) {
					continue;
				} else {
					currentPatrolPoint = (currentPatrolPoint + 1) % PatrolPointsController.GetNumPointsFor (enemy.number);
				}
			} else {
				enemy.navMeshAgent.ResetPath ();
				enemy.navMeshAgent.velocity = Vector3.zero;
				yield return endOfFrame;
			}
		}
	}
}
