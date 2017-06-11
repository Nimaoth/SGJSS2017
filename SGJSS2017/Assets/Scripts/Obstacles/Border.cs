using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour {

    public BorderPrefabManager Manager;
    private bool created = false;
    
	// Update is called once per frame
	void Update()
    {
        float move = Game.Instance.Speed * Time.deltaTime;
        transform.Translate(0, move, 0);

        if (!created && transform.position.y > -6)
        {
            Create();
            created = true;
        }

        if (transform.position.y > 13)
        {
            Destroy(gameObject);
        }
	}

    private void Create()
    {
        if (Manager.Prefabs.Count > 0)
        {
            int i = Random.Range(0, Manager.Prefabs.Count);
            GameObject prefab = Manager.Prefabs[i];

            Vector3 pos = transform.position + new Vector3(0, -10, 0);
            GameObject newObj = Instantiate(prefab, pos, transform.rotation, transform.parent);
            newObj.name = "Border";
            newObj.transform.localScale = transform.localScale;
        }
    }

     void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            Transform player = collision.transform.parent;
            Transform playerTransform = player.transform;
            Player pl = player.GetComponent<Player>();
            Score score = pl.Score;

            score.resetMultiplier();
        }
    }
}
