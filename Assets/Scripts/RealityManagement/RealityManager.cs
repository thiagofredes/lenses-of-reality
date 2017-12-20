using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RealityManager : BaseGameObject
{
	
	public static event Action<int> RealitySet;


	// Update is called once per frame
	void Update ()
	{
		if (!gamePaused && !gameEnded) {
			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				if (RealitySet != null) {
					RealitySet (1);
				}
			} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
				if (RealitySet != null) {
					RealitySet (2);
				}
			} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
				if (RealitySet != null) {
					RealitySet (3);
				}
			}
		}
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
