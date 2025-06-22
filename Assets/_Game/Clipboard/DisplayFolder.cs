using _Game.Crime;
using TMPro;
using UnityEngine;

namespace _Game.Clipboard
{
    public class DisplayFolder : MonoBehaviour
    {
        [Header("UI References (TextMeshPro)")]
        public TMP_Text headerText;

        public TMP_Text dividerText;
        public TMP_Text bodyText;

        private CaseFile _currentCase;

        private void Awake()
        {
            if (headerText == null || dividerText == null || bodyText == null)
                Debug.LogError("Один из TMP_Text не привязан в инспекторе!", this);
        }

        /// <summary>
        /// Вызывается извне, чтобы отобразить новое досье.
        /// </summary>
        public void DisplayCaseFile(CaseFile caseFile)
        {
            headerText.text = "";
            dividerText.text = "";
            bodyText.text = "";
            if (caseFile == null || caseFile.Subject == null)
            {
                Debug.LogWarning("CaseFile или его Subject равен null");
                return;
            }

            _currentCase = caseFile;

            headerText.text = BuildHeader(caseFile.Subject);
            dividerText.text = new string('-', 27);
            bodyText.text = BuildBody(caseFile);
        }

        /// <summary>
        /// Формируем шапку в стиле «печ. машинки»
        /// </summary>
        private string BuildHeader(PersonProfile p)
        {
            var genderAbbrev = p.Gender == Gender.Male ? "M" : "F";
            var q = '"';
            return string.Format(
                "NAME: <pos={4}70%{4}>{0}</pos>\n" +
                "AGE: <pos={4}70%{4}>{1}</pos>\n" +
                "SEX: <pos={4}70%{4}>{2}</pos>\n" +
                "BODY TYPE: <pos={4}70%{4}></pos>{3}",
                p.FullName,
                p.Age,
                genderAbbrev,
                p.BodyType,
                q
            );
        }

        /// <summary>
        /// Формируем тело досье с блоками.
        /// </summary>
        private string BuildBody(CaseFile c)
        {
            return
                $"CRIME DESCRIPTION:\n{c.CrimeDescription}\n" +
                $"INVESTIGATION:\n{c.InvestigationNotes}\n" +
                $"PROOFS:\n{c.ProofSummaries}\n" +
                $"EVIDENCE:\n{c.EvidenceSummary}";
        }
    }
}