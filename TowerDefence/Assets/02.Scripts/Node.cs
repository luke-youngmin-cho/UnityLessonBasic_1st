using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    bool isTowerHere
    {
        get 
        { 
            return towerInfo != null; 
        }
    }
    public TowerInfo towerInfo;

    private Color originColor;
    public Color buildAvailableColor;
    public Color buildNotAvailableColor;

    Renderer rend;
    BoxCollider col;
    private void Awake()
    {
        rend = GetComponent<Renderer>();
        col = GetComponent<BoxCollider>();
        originColor = rend.material.color;
    }
    private void OnMouseEnter()
    {   
        if (TowerViewPresenter.instance.isSelected)
        {
            Transform previewTransform = TowerViewPresenter.instance.GetTowerPreviewTransform();
            previewTransform.gameObject.SetActive(true);
            previewTransform.position = transform.position + new Vector3(0, col.size.y / 2, 0);

            if (isTowerHere)
                rend.material.color = buildNotAvailableColor;
            else
                rend.material.color = buildAvailableColor;
        }
    }
    private void OnMouseExit()
    {
        rend.material.color = originColor;
    }

    private void OnMouseDown()
    {
        if(Input.GetMouseButton(0))
        {
            if (isTowerHere &&
                TowerViewPresenter.instance.isSelected == false)
            {
                TowerUI.instance.upgradePriceText.text = towerInfo.price.ToString();
                TowerUI.instance.sellPriceText.text = (towerInfo.price * 0.8).ToString();
                TowerUI.instance.transform.position = transform.position + Vector3.up * 2;
                TowerUI.instance.node = this;
                TowerUI.instance.gameObject.SetActive(true);
            }
            else if (isTowerHere == false &&
                     TowerViewPresenter.instance.isSelected)
            {
                Transform previewTransform = TowerViewPresenter.instance.GetTowerPreviewTransform();
                string towerName = previewTransform.GetComponent<TowerPreview>().towerName;

                ObjectPool.SpawnFromPool(towerName,
                                         previewTransform.position);
                if (TowerAssets.instance.TryGetTowerInfoByName(towerName, out TowerInfo tmpTowerInfo))
                {
                    // todo -> spend money : towerInfo.price
                    towerInfo = tmpTowerInfo;
                }
                previewTransform.gameObject.SetActive(false);
                TowerViewPresenter.instance.SetTowerHandler(null);
            }
        }
        else
        {
            TowerViewPresenter.instance.SetTowerHandler(null);
        }
        
    }

}
