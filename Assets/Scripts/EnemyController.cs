using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : RealityItem
{
	public NavMeshAgent navMeshAgent;

	public CharacterController characterController;

	public int number;

	public Animator animator;

	[HideInInspector]
	public bool allowChaseByListening;

	private EnemyState currentState;


	void Start ()
	{
		allowChaseByListening = true;
		this.currentState = new Patrolling (this);
	}

	void Update ()
	{
		this.currentState.Update ();
	}

	public void SetState (EnemyState newState)
	{
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
		animator.speed = 0f;
		gamePaused = true;
	}

	protected override void OnGameEnded (bool success)
	{
		animator.speed = 0f;
		gameEnded = true;
	}

	protected override void OnGameResumed ()
	{
		animator.speed = 1f;
		gamePaused = false;
	}

	protected override void OnRealitySet (int reality)
	{
		if (reality == this.reality)
			this.currentState.Resume ();
		else
			this.currentState.Pause ();
	}
}
