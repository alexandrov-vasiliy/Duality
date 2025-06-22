using System;
using System.Collections;
using UnityEngine;

namespace _Game.Crime
{
    public class RunState : MonoBehaviour
    {
        public AudioClip AgonyAudioClip;
        public AudioClip AgonyEnd;
        public AudioClip LaunchClip;

        public int reward = 20;
        public int rewardMercy = 10;
        
        public void StartMercy()
        {
            StartCoroutine(Mercy());
        }

        public void StartExecute()
        {
            StartCoroutine(Execute());
        }

        public IEnumerator Mercy()
        {
            G.Door.Close();

            G.ProgressTracker.RecordDecision(G.crimeInitializator.currentDay, PlayerDecision.Mercy);
            yield return new WaitForSeconds(1);
            G.feel.PlayFree();
            yield return new WaitForSeconds(0.3f);

            yield return WaitDayResultScreen(G.crimeInitializator.currentDay.consequences.mercyText);
            yield return WaitFamilyScreen(rewardMercy);
            
        }

        public IEnumerator Execute()
        {
            G.Main.sfxAudio.PlayOneShot(LaunchClip);

            G.Door.Close();
            G.ProgressTracker.RecordDecision(G.crimeInitializator.currentDay, PlayerDecision.Execute);


            yield return new WaitForSeconds(LaunchClip.length);

            G.Main.sfxAudio.PlayOneShot(AgonyAudioClip);
            yield return new WaitForSeconds(AgonyAudioClip.length);

            G.LeverSwitch.SetState(false);
            G.Main.sfxAudio.PlayOneShot(AgonyEnd);

            yield return WaitDayResultScreen(G.crimeInitializator.currentDay.consequences.executeText);
            yield return WaitFamilyScreen(reward);
        }

        public IEnumerator WaitDayResultScreen(string choiceText)
        {
            G.FolderPickup.gameObject.SetActive(false);
            G.ui.FadeOpen();
            yield return new WaitForSeconds(G.ui.fadeDur);
            G.DayEndView.Open();
            G.DayEndView.choiceText.ShowText(choiceText);

            yield return new WaitUntil(() => G.DayEndView.continuePressed);
            G.DayEndView.Close();

        }

        public IEnumerator WaitFamilyScreen(int addedMoney)
        {
            G.FamilySystem.AdjustMoney(addedMoney);
            G.FamilySystem.Damage(G.crimeInitializator.currentDay.DamageDatas);
            G.FamilyView.Open();


            yield return new WaitUntil(() => G.FamilyView.continuePressed);
            G.FolderPickup.gameObject.SetActive(true);
            G.ui.FadeClose();
            yield return new WaitForSeconds(G.ui.fadeDur);
            
            G.crimeInitializator.NextDay();

            G.FamilyView.Close();
        }
        
     
    }
}