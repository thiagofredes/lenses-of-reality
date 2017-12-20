using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilate : MonoBehaviour
{
	public float oscilationAmplitude = 2f;

	private float t;

	// Use this for initialization
	void Start ()
	{
		t = 0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		t = (t + 6f * Time.deltaTime) % (2f * Mathf.PI);
		this.transform.position += Vector3.up * oscilationAmplitude * Mathf.Sin (t) * Time.deltaTime;
	}
}
