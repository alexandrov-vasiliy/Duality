using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Family
{
    public class MemberRowView : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameLabel;
        [SerializeField] private TMP_Text healthStatusLabel;
        [SerializeField] private Button buyMedicine;

        private string memberName;
        private void OnEnable()
        {
            buyMedicine.onClick.AddListener(HandleBuyMedicine);
        }

        private void HandleBuyMedicine()
        {
            G.FamilySystem.Heal(memberName);
        }

        public void Set(string name, int health)
        {
            buyMedicine.gameObject.SetActive(health is <= 80 and > 0);

            memberName = name;
            nameLabel.text = name;
            healthStatusLabel.text = HealthStateToString(health);
        }

        private static string HealthStateToString(int health)
        {
            if (health <= 0) return "Dead";               // 0
            if (health < 20) return "At death's door";    // 1–19
            if (health < 50) return "Severely ill";       // 20–49
            if (health < 80) return "Mild illness";       // 50–79
            return "Healthy";                              // 80–100
        }
    }

}