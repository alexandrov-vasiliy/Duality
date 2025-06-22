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
        public Material ModelMaterial;
    }

    [Serializable]
    public class CaseFile
    {
        public PersonProfile Subject;

        [TextArea(5, 35)]
        public string CrimeDescription;

        public List<GameObject> EvidenceModels = new List<GameObject>();
    }
}