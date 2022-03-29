using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Laser : Tower
{
    public LineRenderer lineRenderer;
    public Transform firePoint;
    public ParticleSystem hitEffect;
    public Buff buffSlow;

    private Coroutine buffCoroutine = null;
    private Transform targetMem;
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
            if(buffCoroutine != null)
                StopCoroutine(buffCoroutine);
        }
        else
        {
            if(target != targetMem)
            {
                if(buffCoroutine !=null)
                    StopCoroutine(buffCoroutine);
                else
                {
                    buffSlow.doBuff = true;
                    buffCoroutine = StartCoroutine(BuffManager.ActiveBuff(target.GetComponent<Enemy>(), buffSlow));
                    targetMem = target;
                }
            }   

            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, target.position);
            lineRenderer.enabled = true;

            if(hitEffect.isStopped)
                hitEffect.Play();

            Vector3 dir = (firePoint.position - target.position).normalized;
            hitEffect.transform.position = target.position + dir * 0.5f;
            hitEffect.transform.rotation = Quaternion.LookRotation(dir);
        }
    }

}
