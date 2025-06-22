using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

namespace _Game
{
    public class CameraSwitcher : MonoBehaviour
    {
        public CinemachineCamera vcMain;
        public CinemachineCamera vcLaw;

        private bool showLows = false;
        void Start()
        {
            SetCamera(vcMain); // при старте активируем главную
        }

        void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Space)) return;
            
            if (showLows)
            {
                SetCamera(vcMain);
                G.ui.HideLaws();
                showLows = false;
            }
            else
            {
                SetCamera(vcLaw);
                G.ui.ShowLaws();
                showLows = true;
            }

        }

        public void SetCamera(CinemachineCamera activeCam)
        {
            vcMain.Priority = 0;
            vcLaw.Priority = 0;
            activeCam.Priority = 11;

        }
    }
}