using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.ItemSignatures;
using GlobalGameJam2018Networking;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour, IInteractable {

    /// <summary>
    /// describes the amount of shots of the color, can be upgraded
    /// </summary>
    private int QuantityPerItem = 5;

    public Text MyTextDemand;
    public Text MyTimer;

    private ProcessedItemSignature towerRequest;

    float TimeLeft = 300.0f;

    /// <summary>
    /// Storage of the EnergyAmunition, only one ammunition.
    /// </summary>
    private List<ProcessedItem> EnergyAmunitionList;

    private List<ProcessedItem> EnergyAmunitionFirstList = new List<ProcessedItem>();
    private List<ProcessedItem> EnergyAmunitionSecondList = new List<ProcessedItem>();
    private List<ProcessedItem> EnergyAmunitionThirdList = new List<ProcessedItem>();

    /// <summary>
    /// Only ProcessedItems of the Type Energy will be accepted
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool CanInteract(IItem item)
    {
        ProcessedItem proItem = item as ProcessedItem;
        return proItem?.ProcessedType == ProcessedItem.ProcessedItemType.Energy;
    }

    /// <summary>
    /// There is nothing you need to get out of the tower.
    /// </summary>
    public IItem GetItem()
    {
        return null;
    }

    public bool PutItem(IItem item)
    {
        if (CanInteract(item)) {
            EnergyAmunitionList.Add((ProcessedItem)item);
            
            //remove the item holding
            return true;
        }
        return false;
    }

    void CreateFirstDemand() {

        TimeLeft = 300.0f;
        MyTextDemand.text = "Energy Demand: Black";
        EnergyAmunitionFirstList.Add(new ProcessedItem(ProcessedItem.ProcessedItemType.Energy, ProcessedItem.ProcessedItemColor.Black));
    }

    void CreateSecondDemand() {
        if (!CheckDemand(EnergyAmunitionList, EnergyAmunitionFirstList)) {
            GetComponent<Door>().Health =- 50;
        }

        TimeLeft = 300.0f;
        MyTextDemand.text = "Energy Demand : Orange and Blue";
        EnergyAmunitionSecondList.Add(new ProcessedItem(ProcessedItem.ProcessedItemType.Energy, ProcessedItem.ProcessedItemColor.Blue));
        EnergyAmunitionSecondList.Add(new ProcessedItem(ProcessedItem.ProcessedItemType.Energy, ProcessedItem.ProcessedItemColor.Orange));
    }

    void CreateThirdDemand() {
        if (!CheckDemand(EnergyAmunitionList, EnergyAmunitionSecondList)) {
            GetComponent<Door>().Health =- 50;
        }

        TimeLeft = 300.0f;
        MyTextDemand.text = "Energy Demand : Green , Orange and Violet";
        EnergyAmunitionThirdList.Add(new ProcessedItem(ProcessedItem.ProcessedItemType.Energy, ProcessedItem.ProcessedItemColor.Green));
        EnergyAmunitionThirdList.Add(new ProcessedItem(ProcessedItem.ProcessedItemType.Energy, ProcessedItem.ProcessedItemColor.Orange));
        EnergyAmunitionThirdList.Add(new ProcessedItem(ProcessedItem.ProcessedItemType.Energy, ProcessedItem.ProcessedItemColor.Violet));
    }

    public void CheckWinCondition() {
        if (!CheckDemand(EnergyAmunitionList, EnergyAmunitionThirdList)) {
            GetComponent<Door>().Health =- 50;
        }
        MyTextDemand.text = "You win the game!";
    }

    public bool CheckDemand(List<ProcessedItem> EnergyAvailable, List<ProcessedItem> EnergyDemand)
    {
        return EnergyDemand.All(demand => EnergyAvailable.Contains(demand));
    }


    // Use this for initialization
    void Start () {
        MyTextDemand.text = "WE DEMAND THINGS!";
        Invoke("CreateFirstDemand", 3);
        Invoke("CreateSecondDemand", 303);
        Invoke("CreateThirdDemand", 604);
        Invoke("CheckWinCondition", 905);
	}
	
	// Update is called once per frame
	void Update () {
        TimeLeft -= Time.deltaTime;
        MyTimer.text = "Time Left:" + Mathf.Round(TimeLeft);
    }
}
