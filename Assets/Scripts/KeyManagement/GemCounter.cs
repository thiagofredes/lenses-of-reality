using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GemCounter : MonoBehaviour
{
	public int reality;

	public GemCountingCanvas gemCanvas;

	public event Action AllGemsTaken;

	public int numGems;

	private int gemCount;


	void Awake ()
	{
		gemCount = 0;
	}

	public void CountGem ()
	{
		gemCount++;
		gemCanvas.UpdateCount (reality, gemCount, numGems);
		if (gemCount == numGems) {
			if (AllGemsTaken != null) {
				AllGemsTaken ();
			}
		}
	}
}
