using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

	public bool lookAway = false;

	public bool allignDirection = false;

	// Update is called once per frame
	void Update ()
	{
		Vector3 lookVector;

		if (allignDirection) {
			lookVector = ThirdPersonCameraController.CameraForward;
			if (lookAway) {
				this.transform.rotation = Quaternion.LookRotation (lookVector);
			} else {
				this.transform.rotation = Quaternion.LookRotation (-lookVector);
			}
		} else {
			lookVector = ThirdPersonCameraController.CameraForwardProjectionOnGround;
			if (lookAway) {
				this.transform.rotation = Quaternion.LookRotation (lookVector);
			} else {
				this.transform.rotation = Quaternion.LookRotation (-lookVector);
			}
		}
	}
}
