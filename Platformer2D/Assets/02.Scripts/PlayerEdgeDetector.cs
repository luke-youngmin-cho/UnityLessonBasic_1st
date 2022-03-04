using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEdgeDetector : MonoBehaviour
{
    bool top, bottom;
    public Vector2 topCenter, bottomCenter;
    public bool isDetected;
    public Vector2 targetPlayerPos;
    public LayerMask groundLayer;

    Rigidbody2D rb;
    PlayerController controller;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerController>();
    }
    private void Update()
    {
        top = Physics2D.OverlapCircle(new Vector2(rb.position.x + topCenter.x * controller.direction, 
                                                  rb.position.y + topCenter.y),
                                                  0.01f,
                                                  groundLayer);
        bottom = Physics2D.OverlapCircle(new Vector2(rb.position.x + bottomCenter.x * controller.direction,
                                                     rb.position.y + bottomCenter.y),
                                                     0.01f,
                                                     groundLayer);

        isDetected = !top && bottom;
        if (isDetected)
        {
            targetPlayerPos = new Vector2(rb.position.x + (topCenter.x * controller.direction / 2),
                                          rb.position.y);
        }
        else
        {
            targetPlayerPos = Vector2.zero;
        }
    }
    private void OnDrawGizmos()
    {
        if (rb == null || controller == null) return;
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(new Vector2(rb.position.x + topCenter.x * controller.direction,
                                      rb.position.y + topCenter.y),
                                      0.01f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector2(rb.position.x + bottomCenter.x * controller.direction,
                                      rb.position.y + bottomCenter.y),
                                      0.01f);
    }
}
