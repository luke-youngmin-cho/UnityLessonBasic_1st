using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Laser : Tower
{
    public LineRenderer lineRenderer;
    public Transform firePoint;
    public ParticleSystem hitEffect;
    public Buff buffSlow;
    public int damage;
    public int damageIncrementPercent;
    private float elapsedLaserTime;
    private int damageStep = 0;

    private Coroutine buffCoroutine = null;
    private Enemy targetEnemy;
    private void FixedUpdate()
    {
        Laser();
    }
    private void Laser()
    {
        if (target == null)
        {
            lineRenderer.enabled = false;

            if(hitEffect.isPlaying)
                hitEffect.Stop();

            buffSlow.doBuff = false;
            elapsedLaserTime = 0;
            damageStep = 0;
        }
        else
        {
            if(targetEnemy != target.GetComponent<Enemy>())
            {
                buffSlow.OnDeactive(targetEnemy);
                buffSlow.doBuff = true;
                buffCoroutine = StartCoroutine(BuffManager.ActiveBuff(target.GetComponent<Enemy>(), buffSlow));
                targetEnemy = target.GetComponent<Enemy>();
                elapsedLaserTime = 0;
                damageStep = 0;
            }   

            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, target.position);
            lineRenderer.enabled = true;

            if(hitEffect.isStopped)
                hitEffect.Play();

            Vector3 dir = (firePoint.position - target.position).normalized;
            hitEffect.transform.position = target.position + dir * 0.5f;
            hitEffect.transform.rotation = Quaternion.LookRotation(dir);

            targetEnemy.hp -= (int)(damage * (1f + damageIncrementPercent / 100) * (damageStep + 1));

            switch (damageStep)
            {
                case 0:
                    if (elapsedLaserTime > 1) damageStep++;
                    break;
                case 1:
                    if (elapsedLaserTime > 2) damageStep++;
                    break;
                case 2:
                    if (elapsedLaserTime > 3) damageStep++;
                    break;
                default:
                    break;
            }

            elapsedLaserTime += Time.deltaTime;
        }
    }


}
