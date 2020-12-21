using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemStats : MonoBehaviour
{
    public GameObject statsPanelPrefab;

    bool areStatsShown = false;

    GameObject createdPanel;

    public void IsMouseOverUIWithIgnores(GameObject inventoryPanel){
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
    
        List<RaycastResult> raycastResultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultList);

        int counter = 0;
        for(int i = 0; i < raycastResultList.Count; i++){
            if(raycastResultList[i].gameObject.tag == "InventoryImg" ){
                GameObject imgObj = raycastResultList[i].gameObject.GetComponent<RawImageObject>().rawImageObject;

                if(!areStatsShown){
                    areStatsShown = true;
                    
                    createdPanel = Instantiate(statsPanelPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    createdPanel.transform.SetParent(inventoryPanel.transform);
                    createdPanel.GetComponent<RectTransform>().localPosition = new Vector3(raycastResultList[i].gameObject.transform.localPosition.x + 140, 
                                                                                        raycastResultList[i].gameObject.transform.localPosition.y, 0.0f);

                    Transform text = createdPanel.transform.Find("Text");
                    text.gameObject.GetComponent<Text>().text = "Name\n" + imgObj.GetComponent<Item>()._name +
                                                                "\n\nDescription:" + imgObj.GetComponent<Item>()._description + 
                                                                "\n\nWeight: " + imgObj.GetComponent<Item>()._weight;
                }
                break;
            }
            counter++;
        }
        
        if(counter == raycastResultList.Count){
            Destroy(createdPanel);
            areStatsShown = false;
        }
        
    }
}
