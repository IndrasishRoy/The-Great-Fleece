using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAI : MonoBehaviour
{
    [SerializeField]
    private int _currentTarget;
    public List<Transform> wayPoints;
    private NavMeshAgent _agent;
    private bool _reverse, _targetReached;
    private Animator _anim;
    public bool isCoinTossed;
    public Vector3 coinPos;

    // Start is called before the first frame update
    void Start()
    {
        CheckAndInitialize();
    }

    // Update is called once per frame
    void Update()
    {
        GuardMovement();
    }

    void CheckAndInitialize()
    {
        if (_agent == null)
        {
            _agent = GetComponent<NavMeshAgent>();
        }
        else
        {
            Debug.Log("NavMeshAgent is not found in GaurdAI script!");
        }

        if (_anim == null) {

            _anim = GetComponent<Animator>();
        }
        else
        {
            Debug.Log("Guard animation is not attached!");
        }
    }

    void GuardMovement()
    {
        if (wayPoints.Count > 0 && wayPoints[_currentTarget] != null && isCoinTossed == false)
        {
            _agent.SetDestination(wayPoints[_currentTarget].position);
            float distance = Vector3.Distance(transform.position, wayPoints[_currentTarget].position);

            if (distance < 1)
            {
                _anim.SetBool("Walk", false);
            }
            else
            {
                _anim.SetBool("Walk", true);
            }

            if (distance < 1.0f && _targetReached == false)
            {
                if ((_currentTarget == 0 || _currentTarget == wayPoints.Count - 1))
                {
                    _targetReached = true;
                    StartCoroutine(WaitBeforeMoving());
                }
                else
                {
                    if (_reverse == true)
                    {
                        _currentTarget--;
                        if (_currentTarget <= 0)
                        {
                            _reverse = false;
                            _currentTarget = 0;
                        }

                    }
                    else
                    {
                        _currentTarget++;
                    }
                }
            }
        }
        else
        {
            float distance = Vector3.Distance(transform.position, coinPos);

            if (distance < 2.5f)
            {
                _anim.SetBool("Walk", false);
            }
        }
    }

    IEnumerator WaitBeforeMoving()
    {
        Debug.Log("Guards waiting for 2 seconds");

        if (_currentTarget == 0)
        {
            yield return new WaitForSeconds(Random.Range(2, 3));
        }
        else if (_currentTarget == wayPoints.Count - 1)
        {
            yield return new WaitForSeconds(Random.Range(2, 3));
        }

        if (_reverse == true)
        {
            _currentTarget--;
            if (_currentTarget == 0)
            {
                _reverse = false;
                _currentTarget = 0;
            }
        }
        else if (_reverse == false)
        {
            _currentTarget++;
            if (_currentTarget == wayPoints.Count)
            {
                _reverse = true;
                _currentTarget--;
            }
        }

        _targetReached = false;
    }
}
