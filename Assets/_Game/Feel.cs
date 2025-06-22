using MoreMountains.Feedbacks;
using UnityEngine;

namespace _Game
{
    public class Feel : MonoBehaviour
    {
        [SerializeField] private MMF_Player openDoor;
        [SerializeField] private MMF_Player closeDoor;
        [SerializeField] private MMF_Player mercyBtn;
        [SerializeField] private MMF_Player free;
        [SerializeField] private MMF_Player micro;

        public void PlayOpenDoor()
        {
            openDoor.PlayFeedbacks();
        }

        public void PlayCloseDoor()
        {
            closeDoor.PlayFeedbacks();
        }

        public void PlayBtn()
        {
            mercyBtn.PlayFeedbacks();
        }

        public void PlayFree()
        {
            free.PlayFeedbacks();
        }

        public void PlayMicro()
        {
            micro.PlayFeedbacks();
        }
    
    }
}