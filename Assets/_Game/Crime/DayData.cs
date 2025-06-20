using System;
using _Game.Crime;
using UnityEngine;

namespace _Game.Crime
{
    [Serializable]
    public class ChoiceText
    {
        [TextArea] public string executeText;
        [TextArea] public string mercyText;
    }
    
    [CreateAssetMenu(fileName = "CrimeData", menuName = "Crime/DayData", order = 0)]
    public class DayData : ScriptableObject
    {
        public CaseFile crime;
        public ChoiceText consequences;
        public ChoiceText endText;
    }
}

