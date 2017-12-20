using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rotate), typeof(Oscilate))]
public class Gem : BaseGameObject
{
	public AudioSource audioSource;

	private Collider collider;

	private Oscilate oscilation;

	private Rotate rotateComponent;

	void Awake ()
	{
		collider = GetComponent<Collider> ();
		oscilation = GetComponent<Oscilate> ();
		rotateComponent = GetComponent<Rotate> ();
	}

	void Update ()
	{
		if (gamePaused || gameEnded) {
			oscilation.enabled = false;
			rotateComponent.enabled = false;
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (!gamePaused) {
			PlayerController player = other.GetComponent<PlayerController> ();

			if (player != null) {
				collider.enabled = false;
				StartCoroutine (PlaySound ());
			}
		}
	}

	private IEnumerator PlaySound ()
	{
		audioSource.Play ();
		while (audioSource.isPlaying) {
			yield return new WaitForEndOfFrame ();
		}
		Destroy (this.gameObject);
	}

	protected override void OnGamePaused ()
	{
		gamePaused = true;
	}

	protected override void OnGameEnded (bool success)
	{
		gameEnded = true;
	}

	protected override void OnGameResumed ()
	{
		gamePaused = false;
	}
}
