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
    public float popForce = 1f;
    public float rotateSpeed = 1f;
      
    [Header("Kinematics")]
    private Rigidbody rb;
    private BoxCollider col;

    public LayerMask groundLayer;
    private Transform rendererTransform;
    private Vector3 rendererOffset;
    private float elapsedFixedTime;
    private Vector3 eulerAngleOrigin;
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
        eulerAngleOrigin = rendererTransform.eulerAngles;
    }

    private void OnEnable()
    {
        elapsedFixedTime = 0;
        StartCoroutine(E_ShowEffect());
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

    IEnumerator E_ShowEffect()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * popForce, ForceMode.Impulse);

        Debug.Log("Imma rotating");
        while (doFloatingEffect == false)
        {
            Collider[] grounds = Physics.OverlapSphere(rb.position - new Vector3(0f, col.size.y / 2, 0f), 0.1f, groundLayer);

            if (grounds.Length > 0)
                doFloatingEffect = true;

            Debug.Log("I'm rotating");
            rendererTransform.Rotate(new Vector3(rotateSpeed * Time.deltaTime, 0f, rotateSpeed * Time.deltaTime));
            yield return null;
        }
        rendererTransform.eulerAngles = eulerAngleOrigin;
    }

}
