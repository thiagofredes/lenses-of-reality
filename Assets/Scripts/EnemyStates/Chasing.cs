using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Chasing : EnemyState
{
	private Coroutine chaseCoroutine;

	public Chasing (EnemyController enemy)
	{
		this.enemy = enemy;
		this.enemy.allowChaseByListening = false;
		enemy.navMeshAgent.stoppingDistance = 5f;
		chaseCoroutine = this.enemy.StartCoroutine (Chase ());
	}

	public override void OnEnter ()
	{
		base.OnEnter ();
	}

	public override void OnExit ()
	{
		enemy.StopCoroutine (chaseCoroutine);
		enemy.navMeshAgent.ResetPath ();
		enemy.navMeshAgent.velocity = Vector3.zero;
	}

	public override void Pause ()
	{
		enemy.StopCoroutine (chaseCoroutine);
		enemy.navMeshAgent.ResetPath ();
	}

	public override void Resume ()
	{
		chaseCoroutine = this.enemy.StartCoroutine (Chase ());
	}

	public override void Update ()
	{

	}

	private IEnumerator Chase ()
	{
		YieldInstruction endOfFrame = new WaitForEndOfFrame ();
		Vector3 lookVector;
		while (true) {
			if (!enemy.gamePaused && !enemy.gameEnded) {
				enemy.navMeshAgent.SetDestination (enemy.playerRef.transform.position);
				enemy.navMeshAgent.updateRotation = false;
				while (enemy.navMeshAgent.pathPending) {
					yield return endOfFrame;
				}
				if (enemy.navMeshAgent.desiredVelocity != Vector3.zero) {
					lookVector = enemy.navMeshAgent.desiredVelocity;
				} else {
					lookVector = enemy.playerRef.transform.position - enemy.navMeshAgent.transform.position;
				}
				lookVector.y = 0f;
				enemy.transform.rotation = Quaternion.Slerp (enemy.transform.rotation, Quaternion.LookRotation (lookVector), Time.deltaTime * enemy.navMeshAgent.angularSpeed);
				yield return endOfFrame;
			} else {
				enemy.navMeshAgent.ResetPath ();
				yield return endOfFrame;
			}
		}	
	}
}
