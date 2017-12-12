using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RealityManager : MonoBehaviour
{
	public static event Action<int> RealitySet;

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			Debug.Log ("Reality 1");
			if (RealitySet != null) {
				RealitySet (1);
			}
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			Debug.Log ("Reality 2");
			if (RealitySet != null) {
				RealitySet (2);
			}
		} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
			Debug.Log ("Reality 3");
			if (RealitySet != null) {
				RealitySet (3);
			}
		}
	}
}
