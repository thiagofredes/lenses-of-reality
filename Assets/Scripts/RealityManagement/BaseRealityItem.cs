using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRealityItem : RealityItem
{

	public Renderer renderer;

	public Collider collider;

	private void SetRendererAndCollider (bool status)
	{
		renderer.enabled = status;
		collider.enabled = status;
	}

	protected override void OnRealitySet (int reality)
	{
		if (this.reality == reality) {
			if (!activatedOnThisReality) {
				SetRendererAndCollider (true);
				activatedOnThisReality = true;
			}
		} else {
			if (activatedOnThisReality) {
				SetRendererAndCollider (false);
				activatedOnThisReality = false;
			}
		}
	}
}
