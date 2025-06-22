using System;
using System.Collections;
using _Game.Family.ExecutionerSim.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Crime
{
    public class RunState : MonoBehaviour
    {
        public AudioClip AgonyAudioClip;
        public AudioClip AgonyEnd;
        public AudioClip LaunchClip;

        public int rewardExecute = 20;
        public int rewardMercy = 10;

        private bool choiceAccept = false;
        public bool isBlocked = false;

        private void OnEnable()
        {
            G.crimeInitializator.newDay += OnNewDay;
        }

        private void OnDisable()
        {
            G.crimeInitializator.newDay -= OnNewDay;

        }

        private void OnNewDay()
        {
            choiceAccept = false;
        }

        public void StartMercy()
        {
            if(isBlocked) return;

            if(choiceAccept) return;
            
            StartCoroutine(Mercy());
        }

        public void StartExecute()
        {
            if(isBlocked) return;
            
            if(choiceAccept) return;

            StartCoroutine(Execute());
        }

        public IEnumerator Mercy()
        {
            G.Micro.HandleNewDay();

            choiceAccept = true;
            G.Door.Close();

            G.ProgressTracker.RecordDecision(G.crimeInitializator.currentDay, PlayerDecision.Mercy);
            yield return new WaitForSeconds(1);
            G.feel.PlayFree();
            yield return new WaitForSeconds(0.3f);
            
            int rewardSum = rewardMercy + G.crimeInitializator.currentDay.mercyRev;
            yield return WaitDayResultScreen(G.crimeInitializator.currentDay.consequences.mercyText, Verdict.Pardon, rewardSum);
            yield return WaitFamilyScreen(rewardSum);
            
        }

        public IEnumerator Execute()
        {
            choiceAccept = true;
            G.Micro.HandleNewDay();
            
            G.Main.sfxAudio.PlayOneShot(LaunchClip);

            G.Door.Close();
            G.ProgressTracker.RecordDecision(G.crimeInitializator.currentDay, PlayerDecision.Execute);


            yield return new WaitForSeconds(LaunchClip.length);

            G.Main.sfxAudio.PlayOneShot(AgonyAudioClip);
            yield return new WaitForSeconds(AgonyAudioClip.length);

            G.LeverSwitch.SetState(false);
            G.Main.sfxAudio.PlayOneShot(AgonyEnd);
            int rewardSum = rewardExecute + G.crimeInitializator.currentDay.executeRev; 
            yield return WaitDayResultScreen(G.crimeInitializator.currentDay.consequences.executeText, Verdict.Execute, rewardSum);
            yield return WaitFamilyScreen(rewardSum);
        }

        public IEnumerator WaitDayResultScreen(string choiceText, Verdict verdict, int reward)
        {
            G.FolderPickup.gameObject.SetActive(false);
            G.ui.FadeOpen();
            yield return new WaitForSeconds(G.ui.fadeDur);
            G.DayEndView.Open(verdict, reward, choiceText);

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