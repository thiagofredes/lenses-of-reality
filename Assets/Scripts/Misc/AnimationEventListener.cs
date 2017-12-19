using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventListener : MonoBehaviour
{
	[System.Serializable]
	public class AnimationEventAction
	{
		public string eventName;
		public UnityEvent eventAction;
	}

	public AnimationEventAction[] actions;

	private int numActions;

	void Awake ()
	{
		numActions = actions.Length;
	}

	public void AnimationEvent (string eventName)
	{
		for (int ev = 0; ev < numActions; ev++) {
			if (eventName.Equals (actions [ev].eventName)) {
				actions [ev].eventAction.Invoke ();
			}
		}
	}

}
