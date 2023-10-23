using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private const string CURRENT_STATE = "currentState";
    public enum State
    {
        None=0,
        Idle=1,
        Move=2,
        Fire=3,
        Dead=4
    }

    public float MovementSpeed=3.5f;
    public float walkingRadius = 20;
    public float WaitXSecondOnIdle = 3f;
    [ReadOnlyInspector] public State currentState;
    [ReadOnlyInspector] public Transform TargetPlayer;
    [ReadOnlyInspector] public Animator _animator;
    [ReadOnlyInspector] public NavMeshAgent _navMeshAgent;
    [ReadOnlyInspector] public FieldOfView _fov;
    private float runingTimer;

    [ContextMenu("GetComponents")]
    public void GetComponents()
    {
        _animator = GetComponentInChildren<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _fov = GetComponent<FieldOfView>();
    }

    private void Start()
    {
        runingTimer = WaitXSecondOnIdle;
        currentState = State.Idle;
        _navMeshAgent.speed = MovementSpeed;
    }

    private void OnEnable()
    {
        _fov.EnemyDetected += OnEnemyDetect;
        _fov.EnemyUndetected += OnEnemyUndetect;
    }

    private void OnEnemyUndetect()
    {
        ChangeActiveState(State.Idle);
    }

    private void OnDisable()
    {
        _fov.EnemyDetected -= OnEnemyDetect;
        _fov.EnemyUndetected -= OnEnemyUndetect;
    }

    private void OnEnemyDetect()
    {
        ChangeActiveState(State.Fire);
    }

    private void Update()
    {

        switch (currentState)
        {
            case State.Move:
                Movement();
                break;
            case State.Fire:
                TakeAim();
                break;
            case State.Dead:
                break;
            case State.Idle:
                Idle();
                break;
        }

        

        //if (currentState != State.Fire && _fov.canSeePlayer )
        //{
        //    currentState = State.Fire;
        //    _animator.SetInteger(CURRENT_STATE, (int)currentState);

        //}
        //else if (currentState != State.Idle && !_fov.canSeePlayer)
        //{
        //    currentState = State.Idle;
        //    _animator.SetInteger(CURRENT_STATE,(int)currentState);
        //}

    }

    public void ChangeActiveState(State pState)
    {
        currentState= pState;
        _animator.SetInteger(CURRENT_STATE, (int)currentState);
        if (_navMeshAgent.hasPath)
        {
            _navMeshAgent.isStopped = true;
            _navMeshAgent.ResetPath();
        }


    }

    private void Idle()
    {
        runingTimer -= Time.deltaTime;
        if (runingTimer < 0)
        {
            ChangeActiveState(State.Move);
            runingTimer = Random.Range(1, WaitXSecondOnIdle);
        }
    }

    private void TakeAim()
    {
        transform.LookAt(TargetPlayer, transform.up);
        
    }

    public void Movement()
    {
        if (!_navMeshAgent.hasPath)
        {
            FindRandomPlaceOnNavmesh();
        }

    }

    public void FindRandomPlaceOnNavmesh()
    {
        Vector3 randomDirection = Random.insideUnitSphere * walkingRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, walkingRadius, 1);
        _navMeshAgent.SetDestination( hit.position);
    }

    

}
