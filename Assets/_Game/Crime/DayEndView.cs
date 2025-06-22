using System;
using _Game;
using _Game.Family.ExecutionerSim.Core;
using DG.Tweening;
using Febucci.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayEndView : MonoBehaviour
{
    public Image panel;

    public TypewriterByCharacter choiceText;
    public TMP_Text moneyRecived;
    public Image DayEndImage;
    public Button continueButton;

    public bool continuePressed;

    private bool wasSkipped;

    private void Awake()
    {
        continueButton.onClick.AddListener(HandleContinueButton);
    }

    private void HandleContinueButton()
    {
        if (choiceText.isShowingText)
        {
            // Текст ещё печатается — просто показать его мгновенно
            choiceText.SkipTypewriter();
            wasSkipped = true;
        }
        else
        {
            // Если текст был пропущен вручную — ждём второго нажатия
            if (wasSkipped)
            {
                wasSkipped = false;
                return;
            }

            // Текст уже показан — можно закрывать экран
            continuePressed = true;
        }
    }

    public void Open(Verdict verdict, int reward, string message)
    {
        wasSkipped = false;
        continuePressed = false;

        var q = '"';
        moneyRecived.text = $"Money received: <color={q}yellow{q}>{reward}</color>";

        // Устанавливаем нужный спрайт
        DayEndImage.sprite = verdict == Verdict.Execute
            ? G.ui.ExecuteSprite
            : G.ui.MercyaSprite;

        // Включаем панель ПЕРЕД вызовом ShowText (важно!)
        panel.gameObject.SetActive(true);

        // Теперь безопасно вызывать ShowText
        choiceText.ShowText(message);
    }
    
    public void Close()
    {
        panel.gameObject.SetActive(false);
    }
}