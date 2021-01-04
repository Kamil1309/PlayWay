using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemStatsUI : MonoBehaviour
{
    [SerializeField] GameObject statsPanelPrefab;
    GameObject createdPanel;

    GameObject inventoryPanel;

    bool areStatsShown = false;

    public List<RaycastResult> raycastResultList;

    private void Start() {
        inventoryPanel = GetComponent<InventoryUIComponents>().inventoryPanel;
    }

    public void IsMouseOverUIWithIgnores(GameObject inventoryPanel){
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
    
        raycastResultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultList);

        int counter = 0;
        for(int i = 0; i < raycastResultList.Count; i++){
            if(raycastResultList[i].gameObject.tag == "InventoryImg" ){
                if(inventoryPanel == raycastResultList[i].gameObject.GetComponent<RawImageObject>().rawImagePanel){
                    GameObject imgObj = raycastResultList[i].gameObject.GetComponent<RawImageObject>().rawImageObject;

                    if(!areStatsShown){
                        areStatsShown = true;
                        
                        createdPanel = Instantiate(statsPanelPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                        createdPanel.transform.SetParent(inventoryPanel.transform);
                        createdPanel.GetComponent<RectTransform>().localPosition = new Vector3(raycastResultList[i].gameObject.transform.localPosition.x + 140, 
                                                                                            raycastResultList[i].gameObject.transform.localPosition.y, 0.0f);

                        Transform textName = createdPanel.transform.Find("ItemName");
                        Transform textType = createdPanel.transform.Find("ItemType");
                        Transform textDescription = createdPanel.transform.Find("ItemDescription");
                        Transform textWeight = createdPanel.transform.Find("ItemWeight");
                        Transform textPrice = createdPanel.transform.Find("ItemPrice");

                        textName.gameObject.GetComponent<Text>().text = imgObj.GetComponent<Item>()._name;
                        SetTypeText(textType.gameObject, imgObj.GetComponent<Item>()._type);
                        textDescription.gameObject.GetComponent<Text>().text = "\nDescription: \n" + imgObj.GetComponent<Item>()._description;
                        textWeight.gameObject.GetComponent<Text>().text = "\nWeight: " + imgObj.GetComponent<Item>()._weight;
                        textPrice.gameObject.GetComponent<Text>().text = "\nPrice: " + imgObj.GetComponent<Item>()._price.ToString();
                    }
                    break;
                }
                
            }
            counter++;
        }
        
        if(counter == raycastResultList.Count){
            if(createdPanel != null)
                Destroy(createdPanel);
            areStatsShown = false;
        }
        
    }

    public void SetTypeText(GameObject textType, string type){
        Text textToColor = textType.GetComponent<Text>();
        
        if(type == "normal"){
            textToColor.color = new Color(255.0f/255.0f, 255.0f/255.0f, 255.0f/255.0f, 255.0f/255.0f);
        }
        if(type == "unique"){
            textToColor.color = new Color(240.0f/255.0f, 255.0f/255.0f, 0.0f/255.0f, 255.0f/255.0f);
        }
        if(type == "legendary"){
            textToColor.color = new Color(255.0f/255.0f, 121.0f/255.0f, 0.0f/255.0f, 255.0f/255.0f);
        }

        textType.GetComponent<Text>().text = type;
    }
}