﻿using DG.Tweening;
using UnityEngine;

namespace _Game.Clipboard
{

    public class FolderPickup : MonoBehaviour
    {
        [Header("Настройки взятия в руку")]
        [SerializeField] private Transform handTarget;    // Трансформ “руки” (прикрепи к камере)
        [SerializeField] private float moveDuration = 0.5f;
        [SerializeField] private Ease moveEase = Ease.OutQuad;

        private Vector3 _originalPosition;
        private Quaternion _originalRotation;
        public bool isPickedUp { private set; get; }

        private void Awake()
        {
            // Сохраняем стартовые позицию и вращение
            _originalPosition = transform.position;
            _originalRotation = transform.rotation;
        }

        private void OnMouseDown()
        {
            // По клику то поднимаем, то возвращаем
            if (!isPickedUp)
                PickUp();
            else
                ReturnToOriginal();
        }

        /// <summary>
        /// Переносит папку к handTarget (как будто берут в руку).
        /// </summary>
        public void PickUp()
        {
            isPickedUp = true;
            // Перемещаем и вращаем по DOTween
            transform.DOMove(handTarget.position, moveDuration)
                .SetEase(moveEase);
            transform.DORotateQuaternion(handTarget.rotation, moveDuration)
                .SetEase(moveEase);
        }

        /// <summary>
        /// Возвращает папку на исходную позицию и вращение.
        /// </summary>
        public void ReturnToOriginal()
        {
            isPickedUp = false;
            transform.DOMove(_originalPosition, moveDuration)
                .SetEase(moveEase);
            transform.DORotateQuaternion(_originalRotation, moveDuration)
                .SetEase(moveEase);
        }
    }

}