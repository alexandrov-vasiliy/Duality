using System.Collections.Generic;
using UnityEngine;

namespace _Game.Family
{
    using System;
    using UnityEngine;

    public enum FamilyMemberId
    {
        Son,
        Wife,
        Daughter,
    }

    namespace ExecutionerSim.Core
    {
        [Serializable]
        public class FamilyMemeber
        {
            public FamilyMemberId id;
            public string name;
            public int health;
        }

        public class FamilySystem : MonoBehaviour
        {
            [Serializable]
            public class FamilyStatus
            {
                [Header("Economy")] [Tooltip("Current family budget in local currency units.")]
                public int Money = 0;

                public List<FamilyMemeber> members = new List<FamilyMemeber>();
            }

            [SerializeField] private FamilyStatus _status = new FamilyStatus();

            [SerializeField] private int medicineHealValue = 20;
            [SerializeField] private int medicinePriceValue = 20;
            // Read‑only public snapshot (deep‑copy to avoid external mutation).
            public FamilyStatus Snapshot => new FamilyStatus
            {
                Money = _status.Money,
                members = _status.members
            };


            public event Action<FamilyStatus> OnStatusChanged;

            public void AdjustMoney(int amount)
            {
                _status.Money = Mathf.Max(0, _status.Money + amount);
                Dispatch();
            }


            private void Dispatch() => OnStatusChanged?.Invoke(Snapshot);

            public void Damage(List<FamilyMemeber> damage)
            {
                foreach (var familyMemeber in damage)
                {
                    var member = _status.members.Find((member) => member.id == familyMemeber.id);
                    member.health -= familyMemeber.health;
                }
                Dispatch();

            }

            public void Heal(string memberName)
            {
                if (_status.Money < medicinePriceValue)
                {
                    return;
                }
                
                var member = _status.members.Find((member) => member.name == memberName);
                member.health += medicineHealValue;
                _status.Money -= medicinePriceValue;
                Dispatch();
            }
        }


        [Serializable]
        public struct TrialOutcome
        {
            public Verdict Decision;
            public float DailyHealthDecay;
        }

        public enum Verdict
        {
            Execute,
            Pardon
        }
    }
}