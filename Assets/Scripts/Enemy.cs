using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float attackDistance = 1.5f;

    private NavMeshAgent _navMeshAgent;
    private Transform _target;

    protected override void Awake()
    {
        base.Awake();

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = movementSpeed;
    }

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            _target = player.transform;
        }
    }

    private void Update()
    {
        if (_target == null)
        {
            _navMeshAgent.ResetPath();
            return;
        }

        float distance = Vector3.Distance(
            transform.position,
            _target.position
        );

        if (distance > attackDistance)
        {
            ChasePlayer();
        }
        else
        {
            StopAndPrepareAttack();
        }
    }

    private void ChasePlayer()
    {
        _navMeshAgent.isStopped = false;
        _navMeshAgent.SetDestination(_target.position);
        Debug.Log(
    $"Velocity: {_navMeshAgent.velocity} | " +
    $"Position: {transform.position}"
);
    }

    private void StopAndPrepareAttack()
    {
        _navMeshAgent.isStopped = true;
        _navMeshAgent.ResetPath();

        Vector3 direction = _target.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}