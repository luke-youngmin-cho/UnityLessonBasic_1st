using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public KeyCode keyCode;
    Transform tr;
    public float noteSpeed;
    private void Awake()
    {
        tr = transform;
    }
    private void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        tr.Translate(Vector2.down * noteSpeed * Time.fixedDeltaTime);
    }
    public void Hit(HitType type)
    {
        switch (type)
        {
            case HitType.Bad:
                break;
            case HitType.Miss:
                break;
            case HitType.Good:
                break;
            case HitType.Great:
                break;
            case HitType.Cool:
                break;
            default:
                break;
        }
    }
}

public enum HitType
{
    Bad,
    Miss,
    Good,
    Great,
    Cool
}
