using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Laser : Tower
{
    public LineRenderer lineRenderer;
    public Transform firePoint;
    public ParticleSystem hitEffect;
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
        }
        else
        {
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
