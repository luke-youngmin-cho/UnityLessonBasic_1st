using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerViewPresenter : MonoBehaviour
{
    public static TowerViewPresenter instance;
    private void Awake()
    {
        instance = this;
    }

    TowerHandler selectedTowerHandler;
    public bool isSelected
    {
        get
        {
            return selectedTowerHandler != null;
        }
    }

    public LayerMask nodeLayer;
    private void Update()
    {
        if (selectedTowerHandler != null)
        {
            /*Vector3 pos = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, 
                                                               Input.mousePosition.y, 1.0f));
            RaycastHit[] hits = Physics.RaycastAll(ray,float.PositiveInfinity, nodeLayer).
                                    OrderBy(h => h.transform.position.y).ToArray();
            
            if (hits.Length > 0)
            {
                selectedTowerHandler.SetTowerPreviewObjectPosition(hits[0].transform.position);
            }*/
        }
    }

    public void SetTowerHandler(TowerHandler towerHandler)
    {
        selectedTowerHandler = towerHandler;
    }

    public Transform GetTowerPreviewTransform()
    {
        return selectedTowerHandler.towerPreviewObject.transform;
    }
}
