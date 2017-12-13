using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : RealityItem
{
	public NavMeshAgent navMeshAgent;

	public CharacterController characterController;

	public Collider collider;

	public Animator animator;

	public Renderer[] renderers;

	public int number;

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

	private void SetObjectState (bool state)
	{
		this.characterController.enabled = state;
		this.navMeshAgent.enabled = state;
		this.collider.enabled = state;
		this.animator.speed = System.Convert.ToSingle (state);
		foreach (Renderer rend in renderers) {
			rend.enabled = state;
		}
	}

	protected override void OnRealitySet (int reality)
	{
		if (reality == this.reality) {
			SetObjectState (true);
			this.currentState.Resume ();
		} else {
			this.currentState.Pause ();
			SetObjectState (false);
		}
	}
}
