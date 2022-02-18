using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceAnimationFinishEffect : MonoBehaviour
{
    public float lastingTime;
    public float rotateSpeed;
    private float elapsedTime;
    private void OnEnable()
    {
        elapsedTime = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (elapsedTime > lastingTime)
            this.gameObject.SetActive(false);

        elapsedTime += Time.deltaTime;
        this.transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }
}
