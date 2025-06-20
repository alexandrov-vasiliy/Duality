using DG.Tweening;
using UnityEngine;

namespace _Game.Crime
{
    public class Door : MonoBehaviour
    {
        [Header("Настройки анимации")]
        public float moveDistance = 2f; // Насколько дверь поднимается
        public float animationDuration = 0.5f; // Длительность анимации
        public Ease animationEase = Ease.OutQuad; // Кривая анимации

        private Vector3 closedPosition;
        private Vector3 openedPosition;
        private Tween currentTween;

        private void Awake()
        {
            closedPosition = transform.position;
            openedPosition = closedPosition + Vector3.up * moveDistance;
        }

        public void Open()
        {
            // Остановить текущую анимацию
            currentTween?.Kill();
            currentTween = transform.DOMove(openedPosition, animationDuration)
                .SetEase(animationEase);
            
            G.feel.PlayOpenDoor();
        }

        public void Close()
        {
            currentTween?.Kill();
            currentTween = transform.DOMove(closedPosition, animationDuration)
                .SetEase(animationEase);
            
            G.feel.PlayCloseDoor();

        }
    

    }
}