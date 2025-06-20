using System;
using UnityEngine;

namespace _Game
{
    public class Main : MonoBehaviour
    {
        public AudioSource sfxAudio; 
        private void Start()
        {
            G.crimeInitializator.InitDay();
        }
    }
}
