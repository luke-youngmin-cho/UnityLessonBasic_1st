using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Weapon1 : Equipment
{
    public LayerMask targetLayer;
    [SerializeField] private TrailRenderer trailRenderer;

    private bool _doCasting;
    public bool doCasting
    {
        set
        {
            if (value == false)
            {
                targets.Clear();
                if (trailRenderer != null)
                    trailRenderer.enabled = false;
            }
            else
            {
                if (trailRenderer != null)
                    trailRenderer.enabled = true;
            }

            _doCasting = value;
        }
    }
    private Dictionary<int, GameObject> targets = new Dictionary<int, GameObject> ();

    public List<GameObject> GetTargets()
    {
        return targets.Values.ToList();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (_doCasting)
        {
            if (1 << collision.gameObject.layer == targetLayer)
            {
                if (collision.gameObject.TryGetComponent(out Enemy enemy))
                {
                    int hash = collision.gameObject.GetHashCode();// Object �� ���� �ؽø� ���ϴ� �Լ�
                    if (targets.ContainsKey(hash) == false)
                        targets.Add(hash, collision.gameObject);
                }
            }
        }
    }
}
