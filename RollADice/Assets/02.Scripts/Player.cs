using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    static public Player instance;
    private Transform tr;
    private void Awake()
    {
        if (instance == null) instance = this;
        tr = transform;
    }
    public void Move(Vector3 target)
    {
        tr.position = target;
    }
}
