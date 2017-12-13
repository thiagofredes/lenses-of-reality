using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPointsController : MonoBehaviour
{
	[System.Serializable]
	public class PatrolPoints
	{
		public Transform[] points;
	}

	public PatrolPoints[] points;

	private static PatrolPointsController _instance;

	void Awake ()
	{
		_instance = this;
	}

	public static Transform GetPointFor (int enemy, int pointNum)
	{
		return _instance.points [enemy].points [pointNum];
	}

	public static int GetNumPointsFor (int enemy)
	{
		return _instance.points [enemy].points.Length;
	}
}
