using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public UnityEngine.UI.Button[] buttons;
    private int selected = 0;
    private bool moving = false;

    private void Start()
    {
        if (buttons.Length > 0)
            buttons[0].Select();
    }

    private void Update()
    {
        if (buttons.Length > 0 && !moving)
        {
            int input = (int)Input.GetAxis("VerticalMenu");
            if (input != 0)
                StartCoroutine(moveSelectionCo(input));
            buttons[selected].Select();
        }
    }

    private IEnumerator moveSelectionCo(int input)
    {
        moving = true;

        selected = ((selected + input) % buttons.Length + buttons.Length) % buttons.Length;

        yield return new WaitForSeconds(0.2f);

        moving = false;
    }
}
