using System.Collections.Generic;
using UnityEngine;

namespace _Game.Crime
{
    public class CrimeInitializator: MonoBehaviour
    {
        public List<DayData> Days;

        private int currentDayIndex = 0;
        
        public DayData currentDay => Days[currentDayIndex];

        [SerializeField] private Transform criminalPersonPoint;
        [SerializeField] private Transform evidencePoint;
        
        private GameObject criminalPersonPrefab;
        private List<GameObject> spawnedEvidence = new List<GameObject>();

        public void NextDay()
        {
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
            if (!criminalPersonPrefab)
            {
                Destroy(criminalPersonPrefab);
            }

            if (currentDay.crime.Subject.ModelPrefab == null)
            {
                return;
            }
            
            criminalPersonPrefab = Instantiate(currentDay.crime.Subject.ModelPrefab, criminalPersonPoint);
            
            G.Door.Open();
            G.Clipboard.DisplayCaseFile(currentDay.crime);
            
            SpawnEvidence();
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