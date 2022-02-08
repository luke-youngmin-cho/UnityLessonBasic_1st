using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    private float _HP; 
    public float HP
    {
        set
        {
            _HP = value;
            int HPint = (int)_HP;
            HPSlider.value = _HP / HPMax;
            if(_HP <= 0)
            {
                DestroyByPlayerWeapon();
            }
        }
        get
        {
            return _HP;
        }
    }
    [SerializeField] float HPInit;
    [SerializeField] float HPMax;
    [SerializeField] Slider HPSlider;

    [SerializeField] private float score;
    [SerializeField] private float damage;
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
        HP = HPInit;
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // ÇÃ·¹ÀÌ¾î Ã¼·Â ±ðÀ½
            Player player = collision.gameObject.GetComponent<Player>();
            player.HP -= damage;
            // ÆÄ±«ÀÌÆåÆ®
            GameObject effectGO = Instantiate(destroyEffect);
            effectGO.transform.position = tr.position;
            Destroy(this.gameObject);
        }

    }
    public void DestroyByPlayerWeapon()
    {
        GameObject effectGO = Instantiate(destroyEffect);
        effectGO.transform.position = tr.position;

        GameObject playerGO = GameObject.Find("Player");
        playerGO.GetComponent<Player>().score += score;

        Destroy(this.gameObject);
    }
}
