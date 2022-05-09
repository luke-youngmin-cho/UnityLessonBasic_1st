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
    protected Coroutine coroutine = null;
    //====================================================================
    //************************** Public Methods **************************
    //====================================================================

    public virtual void PickUp(Player player)
    {
        if (coroutine == null)
        {
            int remain = InventoryView.instance.GetItemsView(item.type).AddItem(item, num, null);
            Debug.Log($"�÷��̾ ������ {item.name} {num - remain} �� ȹ�� �߽��ϴ�");

            if (remain <= 0)
                coroutine = StartCoroutine(E_PickUpEffect(player));
        }
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

            rendererTransform.Rotate(new Vector3(rotateSpeed * Time.deltaTime, 0f, rotateSpeed * Time.deltaTime));
            yield return null;
        }
        rendererTransform.eulerAngles = eulerAngleOrigin;
    }

    protected IEnumerator E_PickUpEffect(Player player)
    {
        doFloatingEffect = false;
        rb.velocity = Vector3.zero;
        //rb.AddForce(Vector3.up * popForce, ForceMode.VelocityChange);

        rb.useGravity = false;
        bool isReachedToPlayer = false;
        CharacterController playerCol = player.GetComponent<CharacterController>();
        Vector3 playerOffset = new Vector3(0,
                                           playerCol.height / 2 + playerCol.radius,
                                           0);

        MeshRenderer meshRenderer = rendererTransform.GetComponent<MeshRenderer>();
        //float fadeAlpha = 1f;
        //Color fadeColor = meshRenderer.material.color;

        float pickUpTimer = 10f;

        rb.velocity = Vector3.zero;
        while (pickUpTimer > 0 && isReachedToPlayer == false)
        {
            // �����۰� �÷��̾� ���� �Ÿ�
            float distance = (Vector3.Distance(player.transform.position + playerOffset, rb.position));
            Debug.Log(distance);
            // �������� �÷��̾�� ������
            if (distance < 0.1f)
            {
                isReachedToPlayer = true;
                rb.velocity = Vector3.zero;
                break;
            }   

            // ������ -> �÷��̾� �� ���ư� �ӵ� ����
            Vector3 moveVec = (player.transform.position + playerOffset - rb.position) * 5;

            // ���ư���
            //transform.position += moveVec * Time.deltaTime; // �ܼ� transform ������ ����, rb �����ǰ� �ӵ� ���� �߰� �ʿ�
            //rb.position += moveVec * Time.deltaTime; // �ܼ� rb ������ ����
            rb.MovePosition(rb.position + moveVec * Time.deltaTime); // ���������ϸ鼭 rb ������ ����

            // ������
            //fadeAlpha -= Time.deltaTime;
            //fadeColor = new Color(fadeColor.r, fadeColor.g, fadeColor.b, fadeAlpha);
            //
            //if (meshRenderer.material.GetColor("_Color") != null)
            //{
            //    meshRenderer.material.SetColor("_Color", fadeColor);
            //}
            pickUpTimer -= Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
