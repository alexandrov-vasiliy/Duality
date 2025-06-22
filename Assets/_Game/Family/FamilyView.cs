using System;
using _Game;
using _Game.Family;
using _Game.Family.ExecutionerSim.Core;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FamilyView : MonoBehaviour
{
    [Header("Base UI")]
    [SerializeField] private Image panel;
    [SerializeField] private Button continueButton;

    [SerializeField] private TMP_Text money;
    
    [Header("Members UI")]
    [Tooltip("Parent transform where member rows will be instantiated")] 
    [SerializeField] private Transform membersRoot;

    [Tooltip("Prefab that represents one member row (name + health status)")] 
    [SerializeField] private MemberRowView memberRowPrefab;
    public bool continuePressed;
    private void Awake()
    {
        continueButton.onClick.AddListener(HandleContinueButton);
        G.FamilySystem.OnStatusChanged += DrawFamilyStatus;

    }
    
    private void DrawFamilyStatus(FamilySystem.FamilyStatus obj)
    {
        var q = '"';
        money.text = $"<color={q}yellow{q}>Money:  {obj.Money}";
        DisplayMembers(obj.members);
    }
    private void HandleContinueButton()
    {
        continuePressed = true;
    }
    public void Open()
    {
        panel.gameObject.SetActive(true);
    
   
    }
    
    public void Close()
    {
        panel.gameObject.SetActive(false);

    
    }
    
    public void DisplayMembers(System.Collections.Generic.IEnumerable<FamilyMemeber> members)
    {
        // 1. Remove previous rows.
        foreach (Transform child in membersRoot)
        {
            Destroy(child.gameObject);
        }

        // 2. Instantiate a fresh row for every member.
        foreach (var member in members)
        {
            var row = Instantiate(memberRowPrefab, membersRoot);
            row.Set(member.name, member.health);
        }
    }

}
