using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace _Game.Crime
{
    public enum Gender
    {
        Male,
        Female
    }

    [Serializable]
    public class PersonProfile
    {
        [TextArea] public string FullName;
        public int Age;
        public Gender Gender;
        public string BodyType;
        public GameObject ModelPrefab;
    }

    [Serializable]
    public class CaseFile
    {
        public PersonProfile Subject;

        [TextArea] public string CrimeDescription;
        [TextArea] public string InvestigationNotes;
        [TextArea] public string ProofSummaries;
        [TextArea] public string EvidenceSummary;

        public List<GameObject> EvidenceModels = new List<GameObject>();
    }
}