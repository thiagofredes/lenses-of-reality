using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Running : PlayerState
{
	private Vector3 counterMovement;

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

			player.animator.SetFloat ("Forward", movement.normalized.magnitude);
			player.characterController.Move (player.movementSpeed * Time.deltaTime * (movement.normalized + counterMovement.normalized));
		}
	}

    public override void LateUpdate()
    {
        player.characterController.Move(counterMovement.normalized * player.movementSpeed * Time.deltaTime);
    }

    public override void OnTriggerEnter (Collider other)
	{
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy)
        {
            RaycastHit hitThat;
            RaycastHit hitThis;
            Vector3 hitDirection = other.transform.position - player.transform.position;

            Physics.Raycast(player.transform.position, hitDirection, out hitThat);
            Physics.Raycast(other.transform.position, -hitDirection, out hitThis);

            counterMovement = hitThat.point - hitThis.point;
            counterMovement.z = 0f;

            if (Vector3.Dot(counterMovement.normalized, hitDirection.normalized) > 0f)
            {
                counterMovement *= -1f;
            }

            player.Damage(enemy.damage);
        }
	}

	public override void OnTriggerExit (Collider other)
	{
		counterMovement = Vector3.zero;
	}
}
