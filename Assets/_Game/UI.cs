using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Febucci.UI;
using Febucci.UI.Core;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Game
{
    public class UI : MonoBehaviour
    {
        public Sprite ExecuteSprite;
        public Sprite MercyaSprite;
        
        [Header("Fader")]
        public Image fadePanel;
        public float fadeDur = 0.3f;

        [Header("Laws")]
        public Image lawsPanel;
        public List<TMP_Text> lawsText;
        public bool lawPanelAccess = false;

        [FormerlySerializedAs("debugTmp")]
        [Header("Tutorial")]
        public TypewriterCore tutorialTypewriter;
        private TypewriterCore _activeTypewriter;
        public Image TutorialPanel;

        public void FadeOpen()  => fadePanel.DOFade(1, fadeDur);
        public void FadeClose() => fadePanel.DOFade(0, fadeDur);

        public void ShowLaws()
        {
            if(!lawPanelAccess) return;
            
            lawsPanel.DOFade(0.8f, 0.4f);
            foreach (var tmpText in lawsText)
                tmpText.DOFade(1f, 0.4f);
            lawsText[1].text = G.crimeInitializator.currentDay.lawsText;
        }

        public void HideLaws()
        {
            lawsPanel.DOFade(0, 0.4f);
            foreach (var tmpText in lawsText)
                tmpText.DOFade(0, 0.4f);
        }

        public void StartTutorial() => StartCoroutine(TutorialRoutine());

        private IEnumerator TutorialRoutine()
        {
            G.RunState.isBlocked = true;
            yield return ShowAndWait(
              "You showed up for your job as the executioner. Not the best kind of work — but someone has to feed the family.");
            yield return ShowAndWait(
              "There’s a case file on the table — it belongs to <pend>the accused</pend>.");
            yield return ShowAndWait(
              "The lever — <shake>activate current</shake>. The red button — release the accused");
            yield return ShowAndWait(
                "You may speak with the condemned. Use the microphone.");            
            yield return ShowAndWait(
                "<incr>SPACE, view the news</incr>");
            lawPanelAccess = true;
            TutorialPanel.transform.DOMoveY( TutorialPanel.transform.position.y + 1500, 0.3f);
            G.RunState.isBlocked = false;

        }

        private IEnumerator ShowAndWait(string message)
        {
            tutorialTypewriter.ShowText(message);
            _activeTypewriter = tutorialTypewriter;

            bool wasSkipped = false;

            // Этап 1: Ждём, пока текст полностью не появится (или игрок его не скипнет)
            while (_activeTypewriter.isShowingText)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _activeTypewriter.SkipTypewriter(); // Показываем всё сразу
                    wasSkipped = true;
                }

                yield return null;
            }

            // Этап 2: если текст был скипнут — нужно дождаться ещё одного клика
            if (wasSkipped)
            {
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            }
            else
            {
                // Текст сам закончился → ждём один клик для перехода
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            }

            yield return null;
        }
    }
}
