using System;
using System.Collections.Generic;
using _Game.Crime;
using _Game.Family.ExecutionerSim.Core;
using UnityEngine;

namespace _Game.Crime
{
    public enum DayEvent
    {
        NONE,
        BRIBE,
        
    }
    
    [CreateAssetMenu(fileName = "CrimeData", menuName = "Crime/DayData", order = 0)]
    public class DayData : ScriptableObject
    {
        public DayEvent additionalEvent;
        [TextArea] public string lawsText;
        public CaseFile crime;
        public ChoiceText consequences;
        public ChoiceText endText;

        public List<FamilyMemeber> DamageDatas;

        public List<Dialogue> Dialogues;
    }

    [Serializable]
    public class ChoiceText
    {
        [TextArea] public string executeText;
        [TextArea] public string mercyText;
    }

    public enum Speaker
    {
        Subject,
        Executor
    }

    [Serializable]
    public class Dialogue
    {
        [TextArea] public string text;
        public Speaker Speaker;
    }
}

