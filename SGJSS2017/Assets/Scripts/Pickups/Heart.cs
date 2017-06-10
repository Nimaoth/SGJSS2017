using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {

    public Material Mat;
    public Renderer Renderer;

    private Material material;

    private void Start()
    {
        material = new Material(Mat);
        Renderer.material = material;
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Game.Instance.Speed * Time.deltaTime;
        transform.Translate(0, offset, 0);

        // destroy if out of screen
        if (transform.position.y > 50)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player1" || other.tag == "player2")
        {
            StartCoroutine(FadeOut());

            Player p = other.transform.parent.GetComponent<Player>();
            p.Heal();
        }
    }

    private IEnumerator FadeOut()
    {
        float startTime = Time.time;

        float speed = 10;
        while (speed > 0.75f)
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
            speed *= 0.9f;

            Color c = material.color;
            c.a *= 0.9f;
            material.color = c;

            yield return null;
        }

        Destroy(gameObject);
        yield return null;
    }
}
