using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public GameObject coinPrefab;
    public AudioClip coinSound;
    private bool isCoinTossed;

    [SerializeField]
    GameObject _darrenAvatar, _gameOverCutScene;
    [SerializeField]
    GameObject[] _cutScenes;

    private NavMeshAgent _agent;
    private Animator _anim;
    private Vector3 _target;
    // Start is called before the first frame update
    void Start()
    {
        CheckAndInitialize();
    }

    // Update is called once per frame
    void Update()
    {
        //SetPlayerVisibility();
        UpdatePlayerBehaviour();
    }

    void CheckAndInitialize()
    {
        if (_agent == null)
        {
            _agent = GetComponent<NavMeshAgent>();
        }
        else
        {
            Debug.Log("NavMeshAgent is not found in the Player script!");
        }

        if (_anim == null)
        {
            _anim = GetComponentInChildren<Animator>();
        }
        else
        {
            Debug.Log("Animation Clip is missing in the Player script!");
        }

        if (_darrenAvatar == null)
        {
            Debug.Log("Darren is missing in the Player script!");
        }
        else
        {
            _darrenAvatar.gameObject.SetActive(true);
        }

        if (_gameOverCutScene == null)
        {
            Debug.Log("Game Over cut scene is missing in the Player script!");
        }
    }

    void UpdatePlayerBehaviour()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Cast a ray from the mouse position
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                Debug.Log("Hit: " + hitInfo.point);
                _agent.SetDestination(hitInfo.point);
                _anim.SetBool("Walk", true);
                _target = hitInfo.point;
            }
        }

        float distance = Vector3.Distance(transform.position, _target);
        if (distance < 1.0f)
        {
            _anim.SetBool("Walk", false);
        }

        if (Input.GetMouseButtonDown(1) && isCoinTossed == false)
        {
            _anim.SetTrigger("Throw");
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                isCoinTossed = true;
                Instantiate(coinPrefab, hitInfo.point, Quaternion.identity);
                AudioSource.PlayClipAtPoint(coinSound, transform.position);
                SendAIToCoin(hitInfo.point);
            }
        }
    }

    /*void SetPlayerVisibility()
    {
        foreach(var cutscene in _cutScenes)
        {
            if (cutscene.gameObject.activeSelf)
            {
                _darrenAvatar.gameObject.SetActive(false);
            }
        }
    }*/

    void SendAIToCoin(Vector3 coinPos)
    {
        GameObject[] guards = GameObject.FindGameObjectsWithTag("Guard");
        foreach (var guard in guards)
        {
            NavMeshAgent currentAgent = guard.GetComponent<NavMeshAgent>();
            GuardAI currentGuard = guard.GetComponent<GuardAI>();
            Animator currentAnim = guard.GetComponent<Animator>();

            currentGuard.isCoinTossed = true;
            currentAgent.SetDestination(coinPos);
            currentAnim.SetBool("Walk", true);
            currentGuard.coinPos = coinPos;
        }
    }
}
