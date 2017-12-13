using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbing : EnemyState
{
	float grabTime = 3f;

	PlayerController playerRef;

	public Grabbing (EnemyController enemy)
	{
		//Debug.Log ("Grabbing");
		this.enemy = enemy;
		playerRef = GameObject.FindObjectOfType<PlayerController> ();
		grabTime = 3f;
	}

	public override void Update ()
	{
		if (!enemy.gamePaused && !enemy.gameEnded) {
			float distance = Vector3.Distance (this.enemy.transform.position, playerRef.transform.position);
			float angle = Vector3.Angle (this.enemy.transform.forward, playerRef.transform.position - enemy.transform.position);
			if (distance > 7f || angle > 60f) {
				enemy.SetState (new Chasing (this.enemy));
			} else {
				grabTime -= Time.deltaTime;
				if (grabTime <= 0f) {
					GameManager.EndGame (false);
				}
			}
		}
	}

	public override void OnEnter ()
	{
		grabTime = 3f;
	}

	public override void OnExit ()
	{
		base.OnExit ();
	}
}
