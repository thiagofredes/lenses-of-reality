using UnityEngine;
using System.Collections;

public class ThirdPersonCameraController : BaseGameObject
{

	public float sensivityX = 3f;

	public float sensivityY = 3f;

	public float maxAngleY = 150f;

	public float minAngleY = 30f;

	public bool invertX = false;

	public bool invertY = false;

	public static Vector3 CameraForward {
		get { return instance.cam.transform.forward; }
	}

	public static Vector3 CameraRight {
		get { return instance.cam.transform.right; }
	}

	public static Vector3 CameraForwardProjectionOnGround {
		get { return instance.cameraForward.normalized; }
	}

	public static Vector3 CameraRightProjectionOnGround {
		get { return instance.cameraRight.normalized; }
	}

	public static Vector3 CameraPosition {
		get { return instance.cam.transform.position; }
	}

	public Vector3 playerOffset;

	public float maxDistanceToPlayer;

	public float minDistanceToPlayer;

	public float cameraSkin = 0.5f;

	public string[] layersToIgnore;

	private float currentDistanceToPlayer;

	private GameObject playerRef = null;

	private Vector3 cameraDirection;

	private Vector3 lookAtPosition;

	private float currentX;

	private float currentY;

	private Vector3 cameraForward;

	private Vector3 cameraRight;

	private Camera cam;

	private static ThirdPersonCameraController instance;


	void Awake ()
	{
		instance = this;
		cam = GetComponentInChildren<Camera> ();
	}


	IEnumerator Start ()
	{
		while (playerRef == null) {
			playerRef = GameObject.FindGameObjectWithTag ("Player");
			yield return new WaitForEndOfFrame ();
		}
		currentDistanceToPlayer = maxDistanceToPlayer;
		this.transform.position = playerRef.transform.position + playerOffset - playerRef.transform.forward * currentDistanceToPlayer;
		lookAtPosition = playerRef.transform.position + playerOffset;
		ComputeCameraDirection (cam.transform.position);
		cam.transform.position += cameraDirection;
	}


	private void ComputeCameraDirection (Vector3 position)
	{
		cameraDirection = position - lookAtPosition;
		cameraDirection.Normalize ();
		cameraDirection *= currentDistanceToPlayer;
	}


	void Update ()
	{
		if (!gamePaused && !gamePaused) {
			float horizontalInput = Time.deltaTime * sensivityX * Input.GetAxis ("Mouse X");
			float verticalInput = Time.deltaTime * sensivityY * Input.GetAxis ("Mouse Y");

			currentX += invertX ? -horizontalInput : horizontalInput;
			currentY += invertY ? verticalInput : -verticalInput;
			currentY = Mathf.Clamp (currentY, minAngleY, maxAngleY);
		}
	}

	void LateUpdate ()
	{
		if (!gamePaused && !gameEnded) {
			this.transform.position = playerRef.transform.position + playerOffset;
			this.transform.rotation = Quaternion.Euler (currentY, currentX, 0f);
			lookAtPosition = this.transform.position;

			ComputeCameraDirection (cam.transform.position);
			cam.transform.position = CastRayFromPlayer (cam.transform.position);

			cameraForward = Vector3.ProjectOnPlane (cam.transform.forward, Vector3.up);
			cameraRight = Vector3.ProjectOnPlane (cam.transform.right, Vector3.up);
		}
	}

	// This method is responsible for avoiding collisions with walls and other objects
	private Vector3 CastRayFromPlayer (Vector3 cameraPosition)
	{
		RaycastHit hit;
		Vector3 speed = new Vector3 ();

		// If there was a hit, it means something is between player and camera
		if (Physics.Raycast (lookAtPosition, cameraDirection.normalized, out hit, maxDistanceToPlayer, ~LayerMask.GetMask (layersToIgnore))) {
			currentDistanceToPlayer = Mathf.Clamp (hit.distance - cameraSkin, minDistanceToPlayer, maxDistanceToPlayer);
		} else {
			currentDistanceToPlayer = maxDistanceToPlayer;
		}
		ComputeCameraDirection (cameraPosition);
		return Vector3.SmoothDamp (cameraPosition, lookAtPosition + cameraDirection, ref speed, Time.deltaTime);
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
