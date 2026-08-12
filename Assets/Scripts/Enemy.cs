using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float attackDistance = 1.5f;

    [Header("Off-Mesh Link (прыжок вниз)")]
    [Tooltip("Сколько секунд будет длиться визуальный прыжок/падение")]
    [SerializeField] private float linkTraverseDuration = 0.6f;

    private NavMeshAgent _navMeshAgent;
    private Transform _target;
    private Animator _animator;
    private bool _traversingLink;

    protected override void Awake()
    {
        base.Awake();

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = movementSpeed;
        _animator = GetComponent<Animator>();
        _navMeshAgent.autoTraverseOffMeshLink = false;
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
          if (_navMeshAgent.isOnOffMeshLink)
        {
            if (!_traversingLink)
            {
                StartCoroutine(TraverseOffMeshLink());
            }
            return;
        }

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
        _animator.SetFloat("Speed",_navMeshAgent.speed);
    }

    private void ChasePlayer()
    {
        _navMeshAgent.isStopped = false;
        _navMeshAgent.SetDestination(_target.position);
        SpeedLerp(3f);
    }

    private void StopAndPrepareAttack()
    {
        _navMeshAgent.isStopped = true;
        _navMeshAgent.ResetPath();
        SpeedLerp(0f);

        Vector3 direction = _target.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
    private IEnumerator TraverseOffMeshLink()
    {
        _traversingLink = true;
 
        OffMeshLinkData linkData = _navMeshAgent.currentOffMeshLinkData;
        Vector3 startPos = transform.position;
        Vector3 endPos = linkData.endPos + Vector3.up * _navMeshAgent.baseOffset;
 
        float elapsed = 0f;
 
        while (elapsed < linkTraverseDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / linkTraverseDuration;
            transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }
 
        transform.position = endPos;
 
        _navMeshAgent.CompleteOffMeshLink();
        _traversingLink = false;
    }
    private void SpeedLerp(float targetSpeed)
    {
        float speedOffset = 0.1f;

        if (_navMeshAgent.speed < targetSpeed - speedOffset ||
                _navMeshAgent.speed > targetSpeed + speedOffset)
            {
               _navMeshAgent.speed = Mathf.Lerp(_navMeshAgent.speed, targetSpeed,
                    Time.deltaTime * 10f);

                _navMeshAgent.speed = Mathf.Round(_navMeshAgent.speed * 1000f) / 1000f;
            }
            else
            {
                _navMeshAgent.speed = targetSpeed;
            }
    }
}