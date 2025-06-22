using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace _Game.Crime
{

    public enum PlayerDecision
    {
        Execute,
        Mercy
    }

    public class GameProgressTracker : MonoBehaviour
    {
        // Хранит выбор игрока на каждый день
        private Dictionary<DayData, PlayerDecision> decisions = new();

        /// <summary>
        /// Запомнить выбор игрока для конкретного дня
        /// </summary>
        public void RecordDecision(DayData dayData, PlayerDecision decision)
        {
            if (dayData == null) return;
            decisions[dayData] = decision;
        }

        /// <summary>
        /// Получить сохранённый выбор игрока на конкретный день
        /// </summary>
        public PlayerDecision? GetDecision(DayData dayData)
        {
            if (decisions.TryGetValue(dayData, out var decision))
                return decision;

            return null;
        }

        /// <summary>
        /// Собрать и вернуть текст концовки на основе всех сделанных выборов
        /// </summary>
        public string GetFinalEndText()
        {
            StringBuilder result = new();

            foreach (var pair in decisions)
            {
                var day = pair.Key;
                var decision = pair.Value;

                if (day.endText == null) continue;

                string part = decision switch
                {
                    PlayerDecision.Execute => day.endText.executeText,
                    PlayerDecision.Mercy => day.endText.mercyText,
                    _ => ""
                };

                if (!string.IsNullOrWhiteSpace(part))
                {
                    result.AppendLine(part.Trim());
                    result.AppendLine(); // пустая строка между блоками
                }
            }

            return result.ToString().Trim();
        }

        /// <summary>
        /// Очистить всю прогрессию (например, при начале новой игры)
        /// </summary>
        public void ResetProgress()
        {
            decisions.Clear();
        }
    }

}