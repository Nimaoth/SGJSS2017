using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosScript : MonoBehaviour {

    public Vector3 mousePos;
    public GameObject startButton;
    public GameObject exitButton;

    public LayerMask mask;

	// Use this for initialization
	void Start () {
        //startButton = GameObject.Find("Canvas(1)/startButton");
        //exitButton = GameObject.Find("Canvas(1)/exitButton");
    }
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, mask))
        {
            mousePos = hit.point;
        }
        if(Input.anyKeyDown)
            StartCoroutine(popButton());
    }

    IEnumerator popButton()
    {
        yield return new WaitForSeconds(3.0f);
        startButton.SetActive(true);
        exitButton.SetActive(true);
    }
}
