using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyState
{

	protected EnemyController enemy;

	public virtual void Update ()
	{
	}

	public virtual void OnEnter ()
	{
	}

	public virtual void OnExit ()
	{
	}

	public virtual void Pause ()
	{
		
	}

	public virtual void Resume ()
	{
		
	}
}
