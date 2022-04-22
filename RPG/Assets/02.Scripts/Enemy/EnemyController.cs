using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public AI ai;
    public bool aiJump;
    public float aiBehaviorTimeMax = 5f;
    public float aiBehaviorTimeMin = 1f;
    public float aiBehaviorTime;
    public float aiBehaviorTimer;

    public float moveSpeed = 2f;
    public float jumpForce = 5f;
    private Vector3 dir;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        ai++;    
    }

    private void Update()
    {
        PlayAI();
    }

    public void PlayAI()
    {
        switch (ai)
        {
            case AI.DecideRandomBehavior:
                ai = (AI)Random.Range(2, 4);
                aiBehaviorTimer = aiBehaviorTime = Random.Range(aiBehaviorTimeMin, aiBehaviorTimeMax);
                dir = Random.insideUnitSphere;
                dir = new Vector3 (dir.x, 0, dir.z).normalized;
                aiJump = Random.Range(0.0f, 1.0f) < 0.3f ? true : false;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                break;
            case AI.Rest:
                if (aiBehaviorTimer < 0)
                    ai = AI.DecideRandomBehavior;
                else
                    aiBehaviorTimer -= Time.deltaTime;
                break;
            case AI.Move:
                if (aiBehaviorTimer < 0)
                    ai = AI.DecideRandomBehavior;
                else
                {
                    Vector3 deltaMove = dir * moveSpeed * Time.deltaTime;
                    Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
                    rb.rotation = rotation;
                    rb.position += deltaMove;
                    aiBehaviorTimer -= Time.deltaTime;
                }                    
                break;
            case AI.FollowTarget:
                break;
            case AI.AttackTarget:
                break;
            default:
                break;
        }
    }

    public enum AI
    {
        Idle,
        DecideRandomBehavior,
        Rest,
        Move,
        FollowTarget,
        AttackTarget,
    }
}
