using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NoteHitter : MonoBehaviour
{
    public KeyCode keyCode;
    Transform tr;
    public LayerMask noteLayer;
    public SpriteRenderer icon;
    public Color pressedColor;
    Color originColor;
    private void Awake()
    {
        tr = transform;
        originColor = icon.color;
    }
    void Update()
    {
        if (Input.GetKeyDown(keyCode))
            TryHitNote();

        if (Input.GetKey(keyCode))
            ChangeColor();
        else
            ClearColor();

    }
    private void ChangeColor()
    {
        icon.color = pressedColor;
    }
    private void ClearColor()
    {
        icon.color = originColor;
    }


    private void TryHitNote()
    {
        HitType hitType = HitType.Bad;
        List<Collider2D> overlaps = Physics2D.OverlapBoxAll(tr.position,
                                                            new Vector2(tr.lossyScale.x / 2, tr.lossyScale.y * NoteManager.judgeHit_Miss),
                                                            0, noteLayer).ToList();
        if(overlaps.Count > 0)
        {
            overlaps.OrderByDescending(x => x.transform.position.y);

            float distance = Mathf.Abs(overlaps[0].transform.position.y - tr.position.y);

            if(distance < NoteManager.judgeHit_Cool)
                hitType = HitType.Cool;
            else if(distance < NoteManager.judgeHit_Great)
                hitType = HitType.Great;
            else if(distance < NoteManager.judgeHit_Good)
                hitType = HitType.Good;
            else if(distance < NoteManager.judgeHit_Miss)
                hitType = HitType.Miss;

            overlaps[0].gameObject.GetComponent<Note>().Hit(hitType);
            Destroy(overlaps[0].gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.lossyScale.x /2 , transform.lossyScale.y * NoteManager.judgeHit_Bad, 0));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.lossyScale.x / 2, transform.lossyScale.y * NoteManager.judgeHit_Miss, 0));
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.lossyScale.x / 2, transform.lossyScale.y * NoteManager.judgeHit_Good, 0));
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.lossyScale.x / 2, transform.lossyScale.y * NoteManager.judgeHit_Great, 0));
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.lossyScale.x / 2, transform.lossyScale.y * NoteManager.judgeHit_Cool, 0));

    }
}
