using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public int PlayerCount;

    public GameObject PlayerPrefab;
    public Material[] Materials;

	void Start()
    {
        if (PlayerCount < 1 || PlayerCount > 4)
            Debug.LogError("Only 1-4 players supported!");
        PlayerCount = Mathf.Clamp(PlayerCount, 1, 4);

        Transform playerUI = GameObject.FindWithTag("PlayerUI").transform;

        float w = PlayerCount - 1;
		for (int i = 0; i < PlayerCount; i++)
        {
            // enable ui
            playerUI.GetChild(i).gameObject.SetActive(true);

            // spawn player
            Vector3 pos = transform.position;
            pos.x += (i - w * 0.5f) * 2;
            GameObject player = Instantiate(PlayerPrefab, pos, Quaternion.identity, transform);

            // assign material
            Renderer rend = player.GetComponentInChildren<Renderer>();
            rend.material = Materials[i];
        }
	}
}
