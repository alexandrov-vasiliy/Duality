using System;
using DG.Tweening;
using Febucci.UI;
using UnityEngine;
using UnityEngine.UI;

public class DayEndView : MonoBehaviour
{
    public Image panel;

    public TypewriterByCharacter choiceText;

    public Image DayEndImage;
    public Button continueButton;

    public bool continuePressed;
    private void Awake()
    {
        continueButton.onClick.AddListener(HandleContinueButton);
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
    
    
    
}
