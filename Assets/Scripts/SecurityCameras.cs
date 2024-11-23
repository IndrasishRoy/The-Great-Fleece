using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCameras : MonoBehaviour
{
    [SerializeField]
    GameObject _gameOverCutScene;
    public Animator anim;

    //private Color LightGreen = new Color(13, 69, 25, 10);
    //private Color LightRed = new Color(132, 14, 13, 10);
    private Color LightGreen = new Color(0.05f, 0.27f, 0.098f, 0.039f);
    private Color LightRed = new Color(0.51f, 0.0549f, 0.05f, 0.039f);

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            MeshRenderer render = GetComponent<MeshRenderer>();
            render.material.SetColor("_TintColor", LightRed);
            anim.enabled = false;
            StartCoroutine(AnimationRoutine());
            Debug.Log("Darren got detected by the security cameras!");
        }
    }

    IEnumerator AnimationRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        _gameOverCutScene.SetActive(true);
    }
}
