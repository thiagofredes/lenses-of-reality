using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SnowBullet : RealityItem
{
	public float speed = 10f;

	public float life = 10f;

	public int damage = 1;

	public Collider collider;

	public Renderer renderer;

	public ParticleSystem particles;


	void OnDestroy ()
	{
		base.OnDisable ();
	}

	void Update ()
	{
		this.transform.Translate (this.transform.forward * speed * Time.deltaTime, Space.World);
		life -= Time.deltaTime;
		if (life <= 0f) {
			GameObject.Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter (Collider other)
	{
		PlayerController playerController = other.GetComponent<PlayerController> ();
		if (playerController != null) {
			playerController.Damage (damage);
		}
		GameObject.Destroy (this.gameObject);
	}

	protected override void OnGamePaused ()
	{
		base.OnGamePaused ();
		gamePaused = true;
		this.enabled = false;
		this.particles.Pause ();
	}

	protected override void OnGameEnded (bool success)
	{
		base.OnGameEnded (success);
		gameEnded = true;
		this.enabled = false;
		this.particles.Pause ();
	}

	protected override void OnGameResumed ()
	{
		base.OnGameResumed ();
		gamePaused = false;
		this.enabled = true;
		this.particles.Play ();
	}

	protected override void OnRealitySet (int reality)
	{
		if (this != null) {
			if (reality == this.reality) {
				if (!activatedOnThisReality) {
					this.renderer.enabled = true;
					this.collider.enabled = true;
					this.particles.Play ();
					activatedOnThisReality = true;
					this.enabled = true;
				}
			} else {
				if (activatedOnThisReality) {
					this.renderer.enabled = false;
					this.collider.enabled = false;
					this.particles.Stop ();
					activatedOnThisReality = false;
					this.enabled = false;
				}
			}
		}
	}
}
