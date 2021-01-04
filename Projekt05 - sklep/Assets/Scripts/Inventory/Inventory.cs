using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<GameObject> Items = new List<GameObject>();

    public float maxCapacity = 100;
    public float currentCapacity = 0;

    public int gold;

    
    [HideInInspector] public bool isInventoryOpen;
    [HideInInspector] public bool isDuringTrade;

    public GameObject InventoryUI;

    [HideInInspector]
    public GameObject inventoryCanvas;
    [HideInInspector]
    public GameObject inventoryPanel;
    List<GameObject> createdSlots = new List<GameObject>();

    float imgLeftDist = 180.0f;
    float imgInterval = 120.0f;
    float imgNumInRow = 4.0f;
  
    void Start()
    {
        inventoryCanvas = Instantiate(InventoryUI, Vector3.zero, Quaternion.identity);
        inventoryCanvas.transform.SetParent(gameObject.transform);
        inventoryCanvas.SetActive(false);

        inventoryPanel = inventoryCanvas.GetComponent<InventoryUIComponents>().inventoryPanel;
    }

    public void ShowInventory(Vector2 inventoryPos){
        inventoryPanel.transform.localPosition = inventoryPos;
    
        inventoryCanvas.SetActive(true);

        SetTexts();
        AddSlots();
        
        isInventoryOpen = true;
    }

    public void CloseInventory(){
        DestroySlots();
        inventoryCanvas.SetActive(false);

        isInventoryOpen = false;
    }

    public void AddSlots(){
        for(int i = 0; i < Items.Count; i++){
            GameObject createdImage = Instantiate(inventoryCanvas.GetComponent<InventoryUIComponents>().rawImagePrefab, Vector3.zero, Quaternion.identity);
            createdImage.transform.SetParent(inventoryPanel.transform);
            createdImage.transform.localPosition = new Vector3(-imgLeftDist + imgInterval * (i%imgNumInRow), 
                                                                imgLeftDist - imgInterval * Mathf.Floor(i/imgNumInRow));
            createdImage.GetComponent<RawImage>().texture = Items[i].GetComponent<Item>().inventorySprite;
            createdImage.GetComponent<RawImageObject>().rawImageObject = Items[i];
            createdImage.GetComponent<RawImageObject>().rawImagePanel = inventoryPanel;
            createdImage.GetComponent<Outline>().enabled = false;

            createdSlots.Add(createdImage);
        }
    }

    public void SetTexts(){
        if(maxCapacity < float.MaxValue)
            inventoryPanel.transform.Find("CapacityText").GetComponent<Text>().text = "Capacity: " + currentCapacity +"/" + maxCapacity;
        else
            inventoryPanel.transform.Find("CapacityText").GetComponent<Text>().text = "Capacity: " + " -/- ";
        inventoryPanel.transform.Find("GoldText").GetComponent<Text>().text = "Gold: " + gold;
    }

    public void DestroySlots(){
        for(int i = createdSlots.Count - 1; i >= 0; i--){
            Destroy(createdSlots[i]);
        }
    }

    public void ResetSlots(){
        DestroySlots();
        AddSlots();
    }

    private void Update() {
        if(isInventoryOpen){
            inventoryCanvas.GetComponent<ItemStatsUI>().IsMouseOverUIWithIgnores(inventoryPanel);
        }
        
    }
}