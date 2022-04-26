using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hpMax;
    private float _hp;
    public float hp
    {
        set
        {
            if (value < 0)
            {
                value = 0;
                // do die
                DropRandomItem();
                Destroy(gameObject);
            }

            _hp = value;

            if (enemyUI != null)
            {
                enemyUI.SetHPBar(_hp / hpMax);
            }
        }

        get
        {
            return _hp;
        }

    }

    [SerializeField] private Item[] dropItems;
    [SerializeField] private EnemyUI enemyUI;
    public void Hurt(float damage)
    {
        hp -= damage;
    }

    private void Awake()
    {
        hp = hpMax;
    }

    /// <summary>
    /// 랜덤하게 아이템을 드롭하는 함수
    /// </summary>
    private void DropRandomItem()
    {
        // 드롭아이템 목록이 있는지 체크
        if (dropItems == null || 
            dropItems.Length <= 0) 
            return;

        // 드롭할 아이템 
        Item tmpItem = dropItems[Random.Range(0, dropItems.Length)];

        // 드롭할 아이템 드롭
        if (tmpItem != null)
            Instantiate(ItemAssets.GetItemPrefab(tmpItem.name), transform.position, Quaternion.identity);
    }

}
