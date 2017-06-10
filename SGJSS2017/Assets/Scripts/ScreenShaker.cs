﻿using UnityEngine;
using System.Collections;

public class ScreenShaker : MonoBehaviour
{
	private float duration;
	private float magnitude;
    private Transform screenShakeParent;

    private void Awake()
    {
        GameObject newObj = new GameObject("ScreenShake CameraParent");
        screenShakeParent = newObj.transform;

        transform.parent = screenShakeParent;
    }

	public void Shake(float magnitude, float duration)
	{
		StopAllCoroutines();
		this.duration = duration;
		this.magnitude = magnitude;
		StartCoroutine(ShakeCoroutine());
	}

	private IEnumerator ShakeCoroutine()
	{
		float elapsed = 0.0f;

		Vector3 origin = Vector3.zero;

		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;

			float percentComplete = elapsed / duration;
			float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

			// map value to [-1, 1]
			float x = Random.value * 2.0f - 1.0f;
			float y = Random.value * 2.0f - 1.0f;
			x *= magnitude * damper;
			y *= magnitude * damper;

			screenShakeParent.position = new Vector3(x, y, origin.z);
			yield return null;
		}

		screenShakeParent.position = origin;
	}
}
