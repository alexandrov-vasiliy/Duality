using UnityEngine;

namespace _Game
{
    public class Main : MonoBehaviour
    {
        public AudioSource sfxAudio; 
        private void Start()
        {
            G.ui.HideLaws();
            G.DayEndView.Close();
            G.FamilyView.Close();
            G.crimeInitializator.InitDay();
        }
    }
}
