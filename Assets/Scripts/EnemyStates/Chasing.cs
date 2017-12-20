using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasing : EnemyState
{
	private Coroutine chaseCoroutine;

	private GameObject playerRef;

	public Chasing (EnemyController enemy)
	{
		//Debug.Log ("Chasing");
		this.enemy = enemy;
		this.enemy.allowChaseByListening = false;
		playerRef = GameObject.FindGameObjectWithTag ("Player");
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

	public override void Update ()
	{
		if (!enemy.gamePaused && !enemy.gameEnded) {
			if (Vector3.Distance (enemy.transform.position, playerRef.transform.position) < 5f) {
				enemy.StopCoroutine (chaseCoroutine);
				enemy.navMeshAgent.ResetPath ();
				enemy.navMeshAgent.velocity = Vector3.zero;
				enemy.SetState (new Grabbing (this.enemy));
			}
		}
	}

	private IEnumerator Chase ()
	{
		YieldInstruction endOfFrame = new WaitForEndOfFrame ();
		Vector3 lookVector;
		while (true) {
			if (!enemy.gamePaused && !enemy.gameEnded) {
				enemy.navMeshAgent.SetDestination (playerRef.transform.position);
				enemy.navMeshAgent.updateRotation = false;
				while (enemy.navMeshAgent.pathPending) {
					yield return endOfFrame;
				}
				if (enemy.navMeshAgent.desiredVelocity != Vector3.zero) {
					lookVector = enemy.navMeshAgent.desiredVelocity;
				} else {
					lookVector = playerRef.transform.position - enemy.navMeshAgent.transform.position;
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
