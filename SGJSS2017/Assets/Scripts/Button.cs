using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour {

    public RectTransform rt;
    private Vector3 end;
    private Vector3 start;
    private float originalY;
    private float originalRZ;

    float randomP;
    float randomR;

	// Use this for initialization
	void Start () {

        originalY = rt.position.y;

        originalRZ = rt.rotation.eulerAngles.z;

        randomP = Random.Range(1f, 2f);
        randomR = Random.Range(1f, 2f);
    
        StartCoroutine(rise());

    }
	
	// Update is called once per frame
	void Update () {
            Vector3 p = new Vector3(rt.position.x, originalY + Mathf.Sin(Time.time * randomP) * 4, rt.position.z);
            rt.position = p;

            Quaternion q = new Quaternion();
            Vector3 eulerR = new Vector3(rt.rotation.eulerAngles.x, rt.rotation.eulerAngles.y, originalRZ + Mathf.Sin(Time.time * randomR) * 3);
            q.eulerAngles = eulerR;
            rt.rotation = q;
	}

    IEnumerator rise()
    {
        end = new Vector3(rt.position.x, rt.position.y, rt.position.z);
        rt.Translate(new Vector3(0, -60, 0), Space.World);
        start = new Vector3(rt.position.x, rt.position.y, rt.position.z);
        int step = 0;
        while (rt.position.y < end.y)
        {
            yield return new WaitForSeconds(0.01f);
            rt.position = Vector3.Lerp(start,end, 0.02f * step);
            step++;
        }
    }

    public void load()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void quit()
    {
        Debug.Log("test");
        Application.Quit();
    }
}
