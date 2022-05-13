using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedOutLine : MonoBehaviour
{
    public Transform target;

    private void FixedUpdate()
    {
        if (target != null)
        {
            transform.position = target.position;
        }
    }
}
