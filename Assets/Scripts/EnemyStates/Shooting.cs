using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : EnemyState
{
	private float nextTimeToShoot = 0f;

	public Shooting (EnemyController enemy)
	{
		this.enemy = enemy;
	}

	public override void Update ()
	{
		if (!enemy.gameEnded && !enemy.gamePaused) {
			Vector3 playerToEnemyVector = enemy.playerRef.transform.position + 2f * Vector3.up - enemy.transform.position;

			enemy.transform.rotation = Quaternion.LookRotation (Vector3.ProjectOnPlane (playerToEnemyVector, Vector3.up));
			if (Vector3.Distance (enemy.transform.position, enemy.playerRef.transform.position) < enemy.shotDistance && Time.time >= nextTimeToShoot) {
				enemy.Shoot (playerToEnemyVector);
				nextTimeToShoot += enemy.timeBetweenShots;
			}
		}
	}

	public override void OnEnter ()
	{
		base.OnEnter ();
		nextTimeToShoot += enemy.timeBetweenShots;
	}

	public override void OnExit ()
	{
		base.OnExit ();
	}

	public override void Pause ()
	{
		base.Pause ();
	}

	public override void Resume ()
	{
		base.Resume ();
	}
}
