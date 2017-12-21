using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : RealityItem
{
	public enum StartingState
	{
		PATROLLING,
		CHASING,
		SHOOTING
	}

	public PatrolPointsController patrolPointsController;

	public NavMeshAgent navMeshAgent;

	public CharacterController characterController;

	public Collider collider;

	public Renderer[] renderers;

	public ParticleSystem particles;

	public int number;

	public int damage;

	public int life;

	public bool killable;

	public float timeBetweenShots;

	public float shotDistance;

	public Transform shotOrigin;

	public StartingState startingState;

	public GameObject bullet;

	[HideInInspector]
	public bool allowChaseByListening;

	[HideInInspector]
	public PlayerController playerRef;

	private EnemyState currentState;


	void Start ()
	{
		playerRef = GameObject.FindObjectOfType<PlayerController> ();
		allowChaseByListening = true;
		switch (startingState) {
		case StartingState.PATROLLING:
			SetState (new Patrolling (this));
			break;

		case StartingState.CHASING:
			SetState (new Chasing (this));
			break;

		case StartingState.SHOOTING:
			SetState (new Shooting (this));
			break;
		}
	}

	void Update ()
	{
		this.currentState.Update ();
	}

	public void SetState (EnemyState newState)
	{
		if (this.currentState != null)
			this.currentState.OnExit ();
		this.currentState = newState;
		this.currentState.OnEnter ();
	}

	public void Damage ()
	{
		SetState (new Dizzy (this));
	}

	protected override void OnGamePaused ()
	{
		gamePaused = true;
	}

	protected override void OnGameEnded (bool success)
	{
		gameEnded = true;
		this.currentState.Pause ();
		particles.Pause ();
	}

	protected override void OnGameResumed ()
	{
		gamePaused = false;
	}

	private void SetObjectState (bool state)
	{
		this.characterController.enabled = state;
		this.navMeshAgent.enabled = state;
		this.collider.enabled = state;
		if (state) {
			particles.Play ();
		} else {
			particles.Stop ();
		}
		foreach (Renderer rend in renderers) {
			rend.enabled = state;
		}
	}

	protected override void OnRealitySet (int reality)
	{
		if (reality == this.reality) {
			if (!activatedOnThisReality) {
				SetObjectState (true);
				this.currentState.Resume ();
				activatedOnThisReality = true;
			}
		} else {
			if (activatedOnThisReality) {
				this.currentState.Pause ();
				SetObjectState (false);
				activatedOnThisReality = false;
			}
		}
	}

	public void Shoot (Vector3 direction)
	{
		shotOrigin.transform.rotation = Quaternion.LookRotation (direction);
		Instantiate (bullet, shotOrigin.position, shotOrigin.rotation);
	}
}
