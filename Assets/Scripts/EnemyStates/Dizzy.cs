using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dizzy : EnemyState
{

	float dizzyTime = 3f;

	PlayerController playerRef;

	public Dizzy (EnemyController enemy)
	{
		this.enemy = enemy;
		dizzyTime = 3f;
		playerRef = GameObject.FindObjectOfType<PlayerController> ();
	}

	public override void Update ()
	{
		if (!enemy.gamePaused && !enemy.gameEnded) {
			dizzyTime -= Time.deltaTime;
			if (dizzyTime <= 0f) {
				enemy.SetState (new Chasing (this.enemy));
			}
		}
	}

	public override void OnEnter ()
	{
		dizzyTime = 3f;
	}

	public override void OnExit ()
	{
		base.OnExit ();
	}
}
