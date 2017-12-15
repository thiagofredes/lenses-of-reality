using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : PlayerState
{
	private float fallTime;

	public Falling (PlayerController player)
	{
		this.player = player;
		player.animator.SetTrigger ("Fall");
	}

	public override void OnEnter ()
	{
		fallTime = 0f;
	}

	public override void Update ()
	{
		if (!player.gamePaused && !player.gameEnded) {
			float horizontal = Input.GetAxis ("Horizontal");
			float vertical = Input.GetAxis ("Vertical");
			Vector3 movement = ThirdPersonCameraController.CameraForwardProjectionOnGround * vertical + ThirdPersonCameraController.CameraRightProjectionOnGround * horizontal;

			fallTime += 5f * Time.deltaTime;

			if (movement.magnitude < 0.1f)
				player.transform.rotation = Quaternion.LookRotation (player.transform.forward);
			else
				player.transform.rotation = Quaternion.LookRotation (movement);

			if (player.IsOnEnemyHead ()) {
				player.SetState (new Jumping (this.player));
			} else if (player.IsGrounded ()) {
				player.animator.SetTrigger ("OnGround");
				player.SetState (new Running (this.player));
			} 
				
			player.characterController.Move (Time.deltaTime * (-Vector3.up * 10f * fallTime + 0.75f * movement * player.UnscaledMovementSpeed));
		}
	}
}
