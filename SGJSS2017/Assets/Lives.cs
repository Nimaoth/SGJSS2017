using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour {

    private bool boom;

    public Material Mat;
    private Material material;

	// Use this for initialization
	void Start () {
        material = new Material(Mat);
        GetComponent<Image>().material = material;
	}

    public void destroy()
    {
        boom = true;
        StartCoroutine(blink(false));
    }

    public void show(bool b)
    {
        StartCoroutine(blink(b));
    }

    private IEnumerator blink(bool show)
    {
        float blinkSpeed = 15f;

        float f = 0;
        float target = 5 * Mathf.PI;
        float off = show ? Mathf.PI : 0;

        Color c = material.color;
        while (f < target)
        {
            c.a = 0.5f * Mathf.Cos(f + off) + 0.5f;
            material.color = c;
            f += Time.deltaTime * blinkSpeed;

            yield return null;
        }

        c.a = show ? 1.0f : 0.0f;
        material.color = c;
        yield return null;
    }
}
