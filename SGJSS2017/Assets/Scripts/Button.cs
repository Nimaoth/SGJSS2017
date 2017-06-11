using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour {

    public RectTransform rt;
    private Vector3 end;
    private float originalY;
    private float originalRZ;

    private float randomP;
    private float randomR;

    private bool rising = false;

    private float startTime;

    public bool selectOnStart = false;
    public float gap = 0.2f;

	// Use this for initialization
	void Start () {
        if (selectOnStart)
            GetComponent<UnityEngine.UI.Button>().Select();

        originalY = rt.position.y;

        originalRZ = rt.rotation.eulerAngles.z;

        randomP = Random.Range(1f, 2f);
        randomR = Random.Range(1f, 2f);
    
        //StartCoroutine(rise());

        end = new Vector3(rt.position.x, rt.position.y + gap, rt.position.z);
        rt.Translate(new Vector3(0, -30, 0), Space.World);

        rising = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (rising)
        {
            if (rt.position.y < end.y - gap)
            {
                rt.position = Vector3.Lerp(rt.position, end, Time.deltaTime * 10.0f);
            }
            else
            {
                rising = false;
                startTime = Time.time;
            }
        }
        else
        {
            Vector3 p = new Vector3(rt.position.x, originalY + Mathf.Sin((Time.time - startTime) * randomP) * 0.5f, rt.position.z);
            rt.position = p;

            Quaternion q = new Quaternion();
            Vector3 eulerR = new Vector3(rt.rotation.eulerAngles.x, rt.rotation.eulerAngles.y, originalRZ + Mathf.Sin((Time.time - startTime) * randomR) * 3);
            q.eulerAngles = eulerR;
            rt.rotation = q;
        }
	}

    public void load()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void quit()
    {
        Application.Quit();
    }

    public void menu()
    {
        SceneManager.LoadScene("ViolaScene");
    }
}
