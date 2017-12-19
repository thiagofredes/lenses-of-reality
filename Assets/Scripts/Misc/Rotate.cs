using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

	public Vector3 angleAxis;

	public float rotationSpeed;


	void Update ()
	{
		Vector3 rotationVector = angleAxis * rotationSpeed * Time.deltaTime;
		this.transform.Rotate (rotationVector.x, rotationVector.y, rotationVector.z);
	}
}
