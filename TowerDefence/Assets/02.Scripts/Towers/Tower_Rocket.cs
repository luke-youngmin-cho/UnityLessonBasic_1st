using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Rocket : Tower
{
    public GameObject rocketPrefab;
    public Transform[] firePoints;
    public int damage;
    public float reloadTime;
    public float reloadTimer;
    public override void Update()
    {
        base.Update();

        if (reloadTimer < 0)
        {
            if (target != null)
            {
                Attack();
                reloadTimer = reloadTime;
            }
        }
        else
            reloadTimer -= Time.deltaTime;
    }

    private void Attack()
    {
        foreach (var firePoint in firePoints)
        {
            GameObject missile = Instantiate(rocketPrefab, firePoint.position, Quaternion.identity);
            Vector3 dir = (target.transform.position - missile.transform.position).normalized;
            missile.GetComponent<Rocket>().Setup(dir, target, damage);
        }
    }
}
