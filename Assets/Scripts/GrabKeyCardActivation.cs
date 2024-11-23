using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabKeyCardActivation : MonoBehaviour
{
    [SerializeField]
    GameObject _sleepingGuardAvatar, _sleepingGuardCutScene;

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            _sleepingGuardCutScene.SetActive(true);
            GameManager.Instance.HasCard = true;
        }
    }
}
