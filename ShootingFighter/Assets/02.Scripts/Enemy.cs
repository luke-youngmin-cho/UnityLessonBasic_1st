using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject destroyEffect;
    Transform tr;
    Vector3 dir;
    Vector3 deltaMove;
    [SerializeField] private int AIPercent;
    private void Awake()
    {
        tr = gameObject.GetComponent<Transform>();
    }
    private void Start()
    {
        SetTarget_RandomlyToPlayer(AIPercent);
    }
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        deltaMove = dir * speed * Time.deltaTime;
        tr.Translate(deltaMove, Space.World);
    }
    private void SetTarget_RandomlyToPlayer(int percent)
    {
        int tmpRandomValue = Random.Range(0, 100);
        if (percent > tmpRandomValue)
        {
            GameObject target = GameObject.Find("Player");
            if(target == null)
            {
                dir = Vector3.back;
            }
            else
            {
                dir = (target.transform.position - tr.position).normalized;
            }
        }
        else
        {
            dir = Vector3.back;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("PlayerWeapon"))
        {
            // todo -> ÆÄ±« ÀÌÆåÆ®

            GameObject effectGO = Instantiate(destroyEffect);
            effectGO.transform.position = tr.position;
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
