using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinStateActivation : MonoBehaviour
{
    public GameObject winCutScene;
    private void OnTriggerEnter(Collider c)
    {
        if(c.tag == "Player")
        {
            if (GameManager.Instance.HasCard == true)
            {
                winCutScene.SetActive(true);
            }
            else
            {
                Debug.Log("You must grab the key card!");
            }
        }
    }
}
