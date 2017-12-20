using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealityItem : BaseGameObject
{
	public int reality;

	protected bool activatedOnThisReality;

	void OnEnable ()
	{
		base.OnEnable ();
		RealityManager.RealitySet += OnRealitySet;
		activatedOnThisReality = this.gameObject.activeInHierarchy;
	}

	void Disable ()
	{
		base.OnDisable ();
		RealityManager.RealitySet -= OnRealitySet;
	}

	protected virtual void OnRealitySet (int reality)
	{
	}
}
