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
        public int mercyRev = 0;
        public int executeRev = 0;
        public DayEvent additionalEvent;
        [TextArea(5, 20)] public string lawsText;
        public CaseFile crime;
        public ChoiceText consequences;
        public ChoiceText endText;

        public List<FamilyMemeber> DamageDatas;

        public List<Dialogue> Dialogues;
    }

    [Serializable]
    public class ChoiceText
    {
        [TextArea(5, 20)] public string executeText;
        [TextArea(5, 20)] public string mercyText;
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

