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


	public Transform GetPointFor (int enemy, int pointNum)
	{
		return points [enemy].points [pointNum];
	}

	public int GetNumPointsFor (int enemy)
	{
		return points [enemy].points.Length;
	}
}
