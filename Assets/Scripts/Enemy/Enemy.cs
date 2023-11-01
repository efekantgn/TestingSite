using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Vitals;

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
    [ReadOnlyInspector] public EnemyWeaponData WeaponData;
    [ReadOnlyInspector] public Health Health;
    private float runingTimer;

    

    [ContextMenu("GetComponents")]
    public void GetComponents()
    {
        Health = GetComponent<Health>();
        _animator = GetComponentInChildren<Animator>();
        WeaponData = GetComponentInChildren<EnemyWeaponData>();
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


    private void OnDisable()
    {
        _fov.EnemyDetected -= OnEnemyDetect;
        _fov.EnemyUndetected -= OnEnemyUndetect;
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
    }
    private void OnEnemyUndetect()
    {
        ChangeActiveState(State.Idle);
    }

    private void OnEnemyDetect()
    {
        ChangeActiveState(State.Fire);
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
        Vector3 lookPos = new Vector3(TargetPlayer.position.x, transform.position.y, TargetPlayer.position.z);
        transform.LookAt(lookPos, transform.up);
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

    public void DamageMe(float pAmount)
    {
        Health.Decrease(pAmount);
        if (Health.Value <= 0)
        {
            ChangeActiveState(State.Dead);
            GetComponent<CapsuleCollider>().enabled = false;
            _navMeshAgent.isStopped = true;
            Destroy(transform.gameObject,GetCurrentAnimatorTime(_animator,"Death"));
        }
    }
    public float GetCurrentAnimatorTime(Animator targetAnim,string pClipName)
    {
        
        AnimationClip[] animationClips = targetAnim.runtimeAnimatorController.animationClips;
        float totaltime=-1;
        foreach (var clip in animationClips)
        {
            if(clip.name.Equals(pClipName)) totaltime = clip.length;

        }
        return totaltime;
    }

}
