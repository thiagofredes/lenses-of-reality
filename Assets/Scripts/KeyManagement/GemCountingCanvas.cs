using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemCountingCanvas : MonoBehaviour
{

	public CanvasGroup canvasGroup;

	public Text gemCount;

	public CanvasGroup[] gemImages;

	public float screenTime = 3f;

	private Coroutine updateCountCoroutine;

	private float screenTimeCounter = 0f;


	void Start ()
	{
		updateCountCoroutine = null;
		canvasGroup.alpha = 0f;
		for (int i = 0; i < gemImages.Length; i++) {
			gemImages [i].alpha = 0f;
		}
	}

	private void SetImageAlpha (int image, float alpha)
	{
		for (int i = 0; i < gemImages.Length; i++) {
			if (i == image - 1) {
				gemImages [i].alpha = alpha;
			} else {
				gemImages [i].alpha = 0f;
			}
		}
	}

	public void UpdateCount (int reality, int count, int gemTotal)
	{
		gemCount.text = count.ToString () + "/" + gemTotal.ToString ();
		if (updateCountCoroutine == null) {			
			updateCountCoroutine = StartCoroutine (UpdateCoroutine (reality, count, gemTotal));
		} else {
			screenTimeCounter = screenTime;
		}
	}

	private IEnumerator UpdateCoroutine (int image, int count, int gemTotal)
	{
		float t = 0;
		SetImageAlpha (image, 1f);
		while (t < 0.1f) {
			t += Time.deltaTime;
			canvasGroup.alpha = t / 0.1f;
			yield return new WaitForEndOfFrame ();
		}
		screenTimeCounter = screenTime;
		while (screenTimeCounter > 0f) {
			screenTimeCounter -= Time.deltaTime;
			yield return new WaitForEndOfFrame ();
		}
		canvasGroup.alpha = 0f;
		updateCountCoroutine = null;
		yield return null;
	}
}
