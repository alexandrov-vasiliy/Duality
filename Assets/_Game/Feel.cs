using MoreMountains.Feedbacks;
using UnityEngine;

namespace _Game
{
    public class Feel : MonoBehaviour
    {
        [SerializeField] private MMF_Player openDoor;
        [SerializeField] private MMF_Player closeDoor;

        public void PlayOpenDoor()
        {
            openDoor.PlayFeedbacks();
        }

        public void PlayCloseDoor()
        {
            closeDoor.PlayFeedbacks();
        }

    
    }
}