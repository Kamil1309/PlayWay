using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public List<GameObject> Items = new List<GameObject>();

    public int maxCapacity = 100;
    public float currentCapacity = 0;

    public GameObject inventoryCanvas;
    public GameObject panelPrefab;
    public GameObject imagePrefab;

    GameObject createdPanel;
    GameObject createdImage;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Tab)){
            if(!inventoryCanvas.activeSelf){
                
                inventoryCanvas.SetActive(true);

                createdPanel = Instantiate(panelPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                createdPanel.transform.SetParent(inventoryCanvas.transform);
                createdPanel.GetComponent<RectTransform>().localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                createdPanel.transform.Find("Text").GetComponent<Text>().text = "Capacity: " + currentCapacity +"/" + maxCapacity;

                for(int i = 0; i < Items.Count; i++){
                    createdImage = Instantiate(imagePrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    createdImage.transform.SetParent(createdPanel.transform);
                    createdImage.transform.localPosition = new Vector3(-180 + 120 * (i%4), 180 - 120 * Mathf.Floor(i/4), 0.0f);
                    createdImage.GetComponent<RawImageObject>().rawImageObject = Items[i];

                    createdImage.GetComponent<RawImage>().texture = Items[i].GetComponent<Item>().inventorySprite;
                }
            }
        }
        if(Input.GetKey(KeyCode.Tab)){
            GetComponent<ItemStats>().IsMouseOverUIWithIgnores(createdPanel);
        }

        if(Input.GetKeyUp(KeyCode.Tab)){
            inventoryCanvas.SetActive(false);

            Destroy(createdPanel);
        }
    }   

}
