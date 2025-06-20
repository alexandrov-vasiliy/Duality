using System;
using System.Collections;
using UnityEngine;

namespace _Game.Crime
{
    public class RunState : MonoBehaviour
    {
        public AudioClip AgonyAudioClip;
        public AudioClip AgonyEnd;
        public AudioClip LaunchClip;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartExecute();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                Mercy();
            }
        }

        public void StartExecute()
        {
            StartCoroutine(Execute());
        }

        public void Mercy()
        {
            G.Door.Open();
        }

        public IEnumerator Execute()
        {
            G.Door.Close();
            yield return new WaitForSeconds(0.5f);
            
            G.Main.sfxAudio.PlayOneShot(LaunchClip);
            yield return new WaitForSeconds(LaunchClip.length);
            
            G.Main.sfxAudio.PlayOneShot(AgonyAudioClip);
            yield return new WaitForSeconds(AgonyAudioClip.length);
            
            G.Main.sfxAudio.PlayOneShot(AgonyEnd);


        }
    }
}