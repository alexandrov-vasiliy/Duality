using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game
{
    public class UI : MonoBehaviour
    {
        public TMP_Text debugTmp;
        
        [Header("Fader")]
        public Image fadePanel;
        public float fadeDur = 0.3f;

        [Header("Laws")]
        public Image lawsPanel;
        public List<TMP_Text> lawsText;

        public void FadeOpen()
        {
            fadePanel.DOFade(1, fadeDur);
        }
        
        public void FadeClose()
        {
            fadePanel.DOFade(0, fadeDur);
        }

        public void ShowLaws()
        {
            lawsPanel.DOFade(0.8f, 0.4f);
            foreach (var tmpText in lawsText)
            {
                tmpText.DOFade(1f, 0.4f);
            }

            lawsText[1].text = G.crimeInitializator.currentDay.lawsText;
        }

        public void HideLaws()
        {
            lawsPanel.DOFade(0, 0.4f);
            foreach (var tmpText in lawsText)
            {
                tmpText.DOFade(0, 0.4f);
            }
        }
    }
}
