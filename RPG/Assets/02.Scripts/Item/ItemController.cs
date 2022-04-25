using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public Item item;
    public int num = 1;

    [Header("Floating Effect")]
    public bool doFloatingEffect;
    public float floatingSpeed;
    public float floatingHeight;

    [Header("Dropping Effect")]
    public float popForce;
    public float rotateSpeed;
      

    [Header("Kinematics")]
    private Rigidbody rb;
    private BoxCollider col;

    public LayerMask groundLayer;
    private Transform rendererTransform;
    private Vector3 rendererOffset;
    private float elapsedFixedTime;
    //====================================================================
    //************************** Public Methods **************************
    //====================================================================

    public void PickUp()
    {
        // to do -> 인벤토리에 아이템 추가
        // to do -> 픽업 효과
    }

    //====================================================================
    //************************** Private Methods *************************
    //====================================================================

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
        rendererTransform = transform.Find("Renderer");
        rendererOffset = rendererTransform.localPosition;
    }

    private void OnEnable()
    {
        elapsedFixedTime = 0;
    }
    private void FixedUpdate()
    {
        if (doFloatingEffect)
            Floating();
    }

    private void Floating()
    {
        rendererTransform.localPosition = rendererOffset +
                                          new Vector3(0f,
                                                      floatingHeight * Mathf.Sin(floatingSpeed * elapsedFixedTime),
                                                      0f);
        elapsedFixedTime += Time.fixedDeltaTime;
    }


}
