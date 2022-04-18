using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void SetTrigger(string name) =>
        animator.SetTrigger(name);
    
    public void SetFloat(string name, float value) =>
        animator.SetFloat(name, value);

    public void SetBool(string name, bool value) =>
        animator.SetBool(name, value);
}
