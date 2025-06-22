using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace _Game.Crime
{
    public class CrimeInitializator: MonoBehaviour
    {
        [Expandable]
        public List<DayData> Days;

        private int currentDayIndex = 0;
        
        public DayData currentDay => currentDayIndex < Days.Count ? Days[currentDayIndex] : Days[0];

        [SerializeField] private Transform criminalPersonPoint;
        [SerializeField] private Transform evidencePoint;

        [SerializeField] private GameObject maleObj;
        [SerializeField] private GameObject femaleObj;
        
        [SerializeField] private SkinnedMeshRenderer maleMesh;
        [SerializeField] private SkinnedMeshRenderer femaleMesh;
        
        private List<GameObject> spawnedEvidence = new List<GameObject>();

        public void NextDay()
        {
            G.FamilyView.continuePressed = false;
            G.DayEndView.continuePressed = false;
            
            if (currentDay.additionalEvent == DayEvent.BRIBE)
            {
                G.BribeEvent.RemoveBribe();
            }
            
            if (currentDayIndex >= Days.Count)
            {
                currentDayIndex = 0;
                InitDay();
                return;
            }
            currentDayIndex++;
            InitDay();
        }

        public void InitDay()
        {
            Debug.Log(currentDay.crime.Subject.FullName);
            if (currentDay.crime.Subject.Gender == Gender.Male)
            {
                maleObj.SetActive(true);
                femaleObj.SetActive(false);
                maleMesh.material = currentDay.crime.Subject.ModelMaterial;
            }
            if (currentDay.crime.Subject.Gender == Gender.Female)
            {
                maleObj.SetActive(false);
                femaleObj.SetActive(true);
                femaleMesh.material = currentDay.crime.Subject.ModelMaterial;
            }
            
            G.Door.Open();
            G.Clipboard.DisplayCaseFile(currentDay.crime);
            SpawnEvent();
            SpawnEvidence();
        }

        private void SpawnEvent()
        {
            if (currentDay.additionalEvent == DayEvent.BRIBE)
            {
                G.BribeEvent.ApplyBribe();
            }
        }

        private void SpawnEvidence()
        {
            foreach (var o in spawnedEvidence)
            {
                Destroy(o);
            }
            
            spawnedEvidence.Clear();

            foreach (var crimeEvidencePrefab in currentDay.crime.EvidenceModels)
            {
                
                var evidence = Instantiate(crimeEvidencePrefab, evidencePoint);
                
                spawnedEvidence.Add(evidence);
            }
        }
    }
}