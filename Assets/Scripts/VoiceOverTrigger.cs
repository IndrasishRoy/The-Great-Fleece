using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceOverTrigger : MonoBehaviour
{
    [SerializeField]
    AudioClip _clipToPlay;

    private void OnTriggerEnter(Collider c)
    {
        if(c.tag == "Player")
        {
            AudioManager.Instance.PlayVoiceOver(_clipToPlay);
            //AudioSource.PlayClipAtPoint(ClipToPlay, Camera.main.transform.position);
        }
    }
}
