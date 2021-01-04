using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TradeManager : MonoBehaviour
{
    [SerializeField] GameObject tradePanel;
    GameObject createdCanvas;
    GameObject createdPanel;

    int goldBalance;
    int capacityBalance;

    bool tradeStarted = false;

    GameObject player;
    GameObject dealer;

    [SerializeField] List<string> listOfTypes = new List<string>
            { "normal", "unique", "legendary"};
    [SerializeField] List<int> numOfCertainTypes = new List<int>
            { 3, 2, 1};
    [Range(0.0f, 1.0f)][SerializeField] List<float> chancesOfCertainTypes = new List<float>
            { 1.0f, 0.5f, 0.1f};

    public List<GameObject> goodsForSale = new List<GameObject>();

    List<GameObject> itemsForSell;
    List<GameObject> itemsToBuy;

    float maxIncPriceMultiplier = 1.5f;

    private void Start() {
        bool numIsEqul = true;
        if(listOfTypes.Count != numOfCertainTypes.Count)
            numIsEqul = false;
        if(listOfTypes.Count != chancesOfCertainTypes.Count)
            numIsEqul = false;
        
        if(!numIsEqul)
            Debug.LogError("Number of types/number of items/chances is not equal!!!");
    }

    public void StartTrade(GameObject p){
        player = p;
        dealer = gameObject;

        if(player.GetComponent<Inventory>() != null && dealer.GetComponent<Inventory>() != null){
            goldBalance = 0;

            itemsForSell = new List<GameObject>();
            itemsToBuy = new List<GameObject>();

            SetDealerGoods();
            
            player.GetComponent<Inventory>().ShowInventory( new Vector3(-350, 0, 0) );
            dealer.GetComponent<Inventory>().ShowInventory( new Vector3(350, 0, 0)); 

            player.GetComponent<Inventory>().isDuringTrade = true;
            dealer.GetComponent<Inventory>().isDuringTrade = true;

            createdCanvas = Instantiate(tradePanel, Vector3.zero, Quaternion.identity);
            createdCanvas.transform.SetParent(gameObject.transform);
            createdCanvas.GetComponent<Canvas>().sortingOrder = -1;

            createdPanel = createdCanvas.transform.Find("TradePanel").gameObject;
            createdPanel.transform.localPosition = Vector3.zero;

            createdPanel.transform.Find("AcceptButton").GetComponent<Button>().onClick.AddListener(AcceptTrade);
            
            Debug.Log("Trade started");
            tradeStarted = true;
        }
    }

    public void EndTrade(){
        player.GetComponent<Inventory>().CloseInventory();
        dealer.GetComponent<Inventory>().CloseInventory();

        player.GetComponent<Inventory>().isDuringTrade = false;
        dealer.GetComponent<Inventory>().isDuringTrade = false;

        Destroy(createdCanvas);

        player = null;
        dealer = null;

        List<GameObject> items = GetComponent<Inventory>().Items;
        for(int i = GetComponent<Inventory>().Items.Count - 1; i >= 0; i--){
            Destroy(items[i]);
            items.Remove(items[i]);
        }

        capacityBalance = 0;
        goldBalance = 0;
        
        Debug.Log("Trade ended");
        tradeStarted = false;
    }

    private void Update() {
        if(tradeStarted){
            if(Input.GetKeyDown(KeyCode.Escape)){
                EndTrade();
            }

            if (Input.GetMouseButtonDown(0)){
                TradeItemPicked();
            }

            FailureTextDisappearing();
        }
    }

    public void TradeItemPicked(){
        List<RaycastResult> raycastResultList = player.GetComponent<Inventory>().inventoryCanvas.GetComponent<ItemStatsUI>().raycastResultList;

        for(int i = 0; i < raycastResultList.Count; i++){
            if(raycastResultList[i].gameObject.tag == "InventoryImg" ){
                GameObject hoveredItemImage = raycastResultList[i].gameObject;
                GameObject hoveredItemPanel = hoveredItemImage.GetComponent<RawImageObject>().rawImagePanel;
                GameObject hoveredItem = hoveredItemImage.GetComponent<RawImageObject>().rawImageObject;


                if(GameObject.ReferenceEquals(hoveredItemPanel, player.GetComponent<Inventory>().inventoryPanel)){//item picked at player panel
                    if(!hoveredItemImage.GetComponent<Outline>().enabled){ // item picked
                        itemsForSell.Add(hoveredItem);
                        goldBalance += hoveredItem.GetComponent<Item>()._price;
                        capacityBalance -= hoveredItem.GetComponent<Item>()._weight;
                    }
                    else{                                                  // item unpicked
                        itemsForSell.Remove(hoveredItem);
                        goldBalance -= hoveredItem.GetComponent<Item>()._price;
                        capacityBalance += hoveredItem.GetComponent<Item>()._weight;
                    }
                }
                else if(GameObject.ReferenceEquals(hoveredItemPanel, dealer.GetComponent<Inventory>().inventoryPanel)){//item picked at dealer panel
                    if(!hoveredItemImage.GetComponent<Outline>().enabled){ // item picked
                        itemsToBuy.Add(hoveredItem);
                        goldBalance -= hoveredItem.GetComponent<Item>()._price;
                        capacityBalance += hoveredItem.GetComponent<Item>()._weight;
                    }
                    else{                                                  // item unpicked
                        itemsToBuy.Remove(hoveredItem);
                        goldBalance += hoveredItem.GetComponent<Item>()._price;
                        capacityBalance -= hoveredItem.GetComponent<Item>()._weight;
                    }
                }
                SetBalanceText();

                hoveredItemImage.GetComponent<Outline>().enabled = !hoveredItemImage.GetComponent<Outline>().enabled;
            }
        }
    }

    public void AcceptTrade(){
        if(CanDeal()){
            var dealerItems = dealer.GetComponent<Inventory>().Items;
            var playerItems = player.GetComponent<Inventory>().Items;

            for(int i = itemsForSell.Count - 1; i >= 0; i--){
                dealerItems.Add(itemsForSell[i]);
                playerItems.Remove(itemsForSell[i]);

                float priceMultiplier = Random.Range(1.0f, maxIncPriceMultiplier);
                itemsForSell[i].GetComponent<Item>()._price = Mathf.RoundToInt(itemsForSell[i].GetComponent<Item>()._price * priceMultiplier);
                
                itemsForSell.Remove(itemsForSell[i]);
            }

            for(int i = itemsToBuy.Count - 1; i >= 0; i--){
                dealerItems.Remove(itemsToBuy[i]);
                playerItems.Add(itemsToBuy[i]);

                itemsToBuy[i].GetComponent<Item>().SetProperties();
                itemsToBuy.Remove(itemsToBuy[i]);
            }

            dealer.GetComponent<Inventory>().ResetSlots();
            player.GetComponent<Inventory>().ResetSlots();

            player.GetComponent<Inventory>().gold += goldBalance;
            dealer.GetComponent<Inventory>().gold -= goldBalance;

            goldBalance = 0;
            SetBalanceText();

            player.GetComponent<Inventory>().currentCapacity += capacityBalance;
            capacityBalance = 0;
            

            player.GetComponent<Inventory>().SetTexts();
            dealer.GetComponent<Inventory>().SetTexts();

            SetFailureText("");
        }
    }

    public bool CanDeal(){
        bool canDeal = true;

        if(player.GetComponent<Inventory>().gold + goldBalance < 0)
        {   
            SetFailureText("You don't have enough money");
            canDeal = false;
        }
            
        if(dealer.GetComponent<Inventory>().gold - goldBalance < 0){
            SetFailureText("Dealer don't have enough money");
            canDeal = false;
        }
        if(player.GetComponent<Inventory>().currentCapacity + capacityBalance > player.GetComponent<Inventory>().maxCapacity){
            SetFailureText("It's too heavy");
            canDeal = false;
        }
        return canDeal;
    }

    public void SetFailureText(string text){
        Transform textFailure = createdPanel.transform.Find("FailureText");
        textFailure.gameObject.GetComponent<Text>().text = text;

        textFailure.gameObject.GetComponent<Text>().color = new Color(textFailure.gameObject.GetComponent<Text>().color.r,
                                                                    textFailure.gameObject.GetComponent<Text>().color.g,
                                                                    textFailure.gameObject.GetComponent<Text>().color.b,
                                                                    1.0f);
    }

    public void FailureTextDisappearing(){
        Transform textFailure = createdPanel.transform.Find("FailureText");

        if(textFailure.gameObject.GetComponent<Text>().color.a > 0.3f){
            if(textFailure.gameObject.GetComponent<Text>().color.a > 0.0f){
                //Debug.Log("ajigijg  " + textFailure.gameObject.GetComponent<Text>().color.a);
                textFailure.gameObject.GetComponent<Text>().color = new Color(textFailure.gameObject.GetComponent<Text>().color.r,
                                                                            textFailure.gameObject.GetComponent<Text>().color.g,
                                                                            textFailure.gameObject.GetComponent<Text>().color.b,
                                                                            textFailure.gameObject.GetComponent<Text>().color.a - 0.005f);
            }
        }else{
            textFailure.gameObject.GetComponent<Text>().color = new Color(textFailure.gameObject.GetComponent<Text>().color.r,
                                                                            textFailure.gameObject.GetComponent<Text>().color.g,
                                                                            textFailure.gameObject.GetComponent<Text>().color.b,
                                                                            0.0f);
        }
    }
            

    public void SetBalanceText(){
        Transform textBalance = createdPanel.transform.Find("BalanceText");
        textBalance.gameObject.GetComponent<Text>().text = "Balance: \n" + goldBalance.ToString();
    }

    public void SetDealerGoods(){
        GetComponent<Inventory>().gold = Random.Range(100, 150);

        for(int typeNum = 0; typeNum < listOfTypes.Count; typeNum++){
            List<GameObject> itemsOfCertainType = goodsForSale.FindAll(item => item.GetComponent<Item>()._type == listOfTypes[typeNum]);
            if(itemsOfCertainType.Count > 0){
                for(int itemCounter = 0; itemCounter < numOfCertainTypes[typeNum]; itemCounter++){
                    if(Random.Range(0.0f, 1.0f) < chancesOfCertainTypes[typeNum]){
                        int index = Random.Range(0, itemsOfCertainType.Count);
                        GameObject createdItem = Instantiate(itemsOfCertainType[index], Vector3.zero, Quaternion.identity);
                        createdItem.GetComponent<Item>().SetProperties();
                        createdItem.SetActive(false);

                        float priceMultiplier = Random.Range(1.0f, maxIncPriceMultiplier);
                        createdItem.GetComponent<Item>()._price = Mathf.RoundToInt(createdItem.GetComponent<Item>()._price * priceMultiplier);
                        

                        GetComponent<Inventory>().Items.Add(createdItem);
                    }
                }
            }
        }
    }
}
