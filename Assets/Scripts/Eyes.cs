using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameOverCutscene;

    private void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            Debug.Log("Caught the Player(Darren)");
            _gameOverCutscene.SetActive(true);
        }
    }
}
