using UnityEngine;

namespace _Game.Family
{
using System;
using UnityEngine;

namespace ExecutionerSim.Core
{
    /// <summary>
    /// Centralised model of the player‑character’s family state.
    /// Everything here reflects the *Everything Has Cost* pillar: every change in
    /// money, health, or morale stems from the player’s courtroom actions.
    /// Attach this script to a persistent GameObject (e.g. "GameSystems").
    /// </summary>
    public class FamilySystem : MonoBehaviour
    {
        #region DATA‑MODEL

        [Serializable]
        public class FamilyStatus
        {
            [Header("Economy")]
            [Tooltip("Current family budget in local currency units.")]
            public int Money = 0;

            [Header("Wife Health (0‑100)")]
            [Range(0, 100)] public float WifeHealth = 100f;

            [Header("Children Mood (0‑100)")]
            [Range(0, 100)] public float ChildrenMood = 50f;

            [Header("Risk of Exposure (0‑1)")]
            [Range(0f, 1f)] public float Risk = 0f;
        }

        [SerializeField] private FamilyStatus _status = new FamilyStatus();

        // Read‑only public snapshot (deep‑copy to avoid external mutation).
        public FamilyStatus Snapshot => new FamilyStatus
        {
            Money = _status.Money,
            WifeHealth = _status.WifeHealth,
            ChildrenMood = _status.ChildrenMood,
            Risk = _status.Risk
        };

        #endregion

        #region EVENTS
        /// <summary>
        /// Fired whenever the status changes. Subscribe UI widgets or other systems.
        /// </summary>
        public event Action<FamilyStatus> OnStatusChanged;
        #endregion

        #region EXTERNAL API

        /// <summary>
        /// Adds (positive) or removes (negative) money from the family budget.
        /// </summary>
        public void AdjustMoney(int amount)
        {
            _status.Money = Mathf.Max(0, _status.Money + amount);
            Dispatch();
        }

        /// <summary>
        /// Apply medical treatment cost and improve wife’s health.
        /// </summary>
        /// <param name="treatmentCost">Money required.</param>
        /// <param name="healthGain">Health gained (0‑100 scale).</param>
        public void PayForTreatment(int treatmentCost, float healthGain)
        {
            if (_status.Money < treatmentCost) return; // Not enough funds.
            AdjustMoney(-treatmentCost);
            _status.WifeHealth = Mathf.Clamp(_status.WifeHealth + healthGain, 0f, 100f);
            Dispatch();
        }

        /// <summary>
        /// Adjusts children mood. Positive values cheer up, negative depress.
        /// </summary>
        public void AdjustChildrenMood(float delta)
        {
            _status.ChildrenMood = Mathf.Clamp(_status.ChildrenMood + delta, 0f, 100f);
            Dispatch();
        }

        /// <summary>
        /// Adjust risk of exposure / blackmail (0‑1 scale).
        /// </summary>
        public void AdjustRisk(float delta)
        {
            _status.Risk = Mathf.Clamp01(_status.Risk + delta);
            Dispatch();
        }

        /// <summary>
        /// Convenience wrapper applied at the end of a trial day.
        /// </summary>
        public void ApplyDailyOutcome(TrialOutcome outcome)
        {
            // Morale: harsh executions decrease mood, pardons increase.
            AdjustChildrenMood(outcome.Decision == Verdict.Execute ? -10f : +5f);

            _status.WifeHealth = Mathf.Clamp(_status.WifeHealth - outcome.DailyHealthDecay, 0f, 100f);

            Dispatch();
        }

        #endregion

        #region PRIVATE
        private void Dispatch() => OnStatusChanged?.Invoke(Snapshot);

        private void Awake()
        {
            // Optional: persist across scenes.
            DontDestroyOnLoad(gameObject);
        }
        #endregion
    }

    /// <summary>
    /// Struct representing the outcome of a single trial day.
    /// Populate from the courtroom system and pass to <see cref="FamilySystem.ApplyDailyOutcome"/>.
    /// </summary>
    [Serializable]
    public struct TrialOutcome
    {
        public Verdict Decision;
        public float DailyHealthDecay; // e.g., 2f per day.
    }

    public enum Verdict
    {
        Execute,
        Pardon
    }
}

}