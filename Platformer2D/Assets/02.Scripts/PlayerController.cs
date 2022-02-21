using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Transform tr;
    Rigidbody2D rb;
    public float moveSpeed;
    public float jumpForce;

    public bool isJumping;
    public Transform groundDetectPoint;
    public float groundMinDistance;
    private void Awake()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        // 키보드 좌우 입력 받아서 게임오브젝트를 좌우로 움직이는 기능
        float h = Input.GetAxis("Horizontal");
        rb.position += new Vector2(h * moveSpeed * Time.deltaTime, 0);

        if(isJumping == false && Input.GetKeyDown(KeyCode.LeftAlt))
        {
            isJumping = true;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        Vector2 origin = groundDetectPoint.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, groundMinDistance);
        
        Collider2D hitCol = hit.collider;
        if(hitCol != null)
        {
            if(hitCol.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                isJumping = false;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(groundDetectPoint.position,
                        new Vector3(groundDetectPoint.position.x,
                                    groundDetectPoint.position.y - groundMinDistance,
                                    groundDetectPoint.position.z));
    }
}
