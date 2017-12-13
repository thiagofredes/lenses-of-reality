using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : BaseGameObject
{

	public float rotationSpeed = 2f;

	public float oscilationAmplitude = 2f;

	public AudioSource audioSource;

	private float t;

	private Collider collider;

	void Awake ()
	{
		t = 0f;
		collider = GetComponent<Collider> ();
	}

	void Update ()
	{
		if (!gamePaused && !gameEnded) {
			t = (t + 6f * Time.deltaTime) % (2f * Mathf.PI);
			this.transform.position += Vector3.up * oscilationAmplitude * Mathf.Sin (t) * Time.deltaTime;
			this.transform.Rotate (0f, rotationSpeed, 0f, Space.World);
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
