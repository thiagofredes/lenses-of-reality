using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Running : PlayerState
{
	public Running (PlayerController player)
	{
		this.player = player;
	}

	public override void Update ()
	{
		if (!player.gamePaused && !player.gameEnded) {
			float horizontal = Input.GetAxis ("Horizontal");
			float vertical = Input.GetAxis ("Vertical");
			Vector3 movement = ThirdPersonCameraController.CameraForwardProjectionOnGround * vertical + ThirdPersonCameraController.CameraRightProjectionOnGround * horizontal;


			if (!player.IsGrounded ()) {
				this.player.SetState (new Falling (this.player));
			}

			if (Input.GetKey (KeyCode.Space)) {			
				this.player.SetState (new Jumping (this.player));
			}

			if (movement.magnitude < 0.1f)
				player.transform.rotation = Quaternion.LookRotation (player.transform.forward);
			else
				player.transform.rotation = Quaternion.LookRotation (movement);

			player.animator.SetFloat ("movement", movement.normalized.magnitude);
			player.characterController.Move (player.movementSpeed * Time.deltaTime * movement.normalized);
		}
	}
}
