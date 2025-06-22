using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

namespace _Game.Handle
{
    public class LeverSwitch : MonoBehaviour
    {
        [Header("Lever")] public Transform leverHandle;

        [Tooltip("Локальный угол (°) в положении ВКЛ.")]
        public Vector3 onRotation = new Vector3(-60f, 0f, 0f);

        [Tooltip("Локальный угол (°) в положении ВЫКЛ.")]
        public Vector3 offRotation = Vector3.zero;

        [Header("Tween Settings")] [Tooltip("Сколько секунд длится анимация.")] [Min(0f)]
        public float tweenDuration = 0.25f;

        [Tooltip("Ease из DOTween, если не используется кастомная кривая.")]
        public Ease tweenEase = Ease.OutBack;

        [Header("Custom Curve (optional)")] [Tooltip("Использовать ли собственную кривую вместо пресета Ease.")]
        public bool useCustomCurve = false;

        [Tooltip("Кастомная кривая скорости. X — время (0-1), Y — скорость.")]
        public AnimationCurve customCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        [Header("Events")] public UnityEvent onPowerOn;
        public UnityEvent onPowerOff;

        [SerializeField] private bool isOn;
        private Tween activeTween;

        /// <summary>Переключить состояние.</summary>
        public void Toggle() => SetState(!isOn);

        /// <summary>Установить конкретное состояние.</summary>
        public void SetState(bool turnOn)
        {
            if (isOn == turnOn) return;
            isOn = turnOn;

            activeTween?.Kill();
            Vector3 targetRot = turnOn ? onRotation : offRotation;

            var t = leverHandle.DOLocalRotate(targetRot, tweenDuration);
            if (useCustomCurve)
                t.SetEase(customCurve);
            else
                t.SetEase(tweenEase);

            activeTween = t.OnComplete(() =>
            {
                if (isOn) onPowerOn?.Invoke();
                else      onPowerOff?.Invoke();
            });
            
        }

        // Пример простого входа
        private void OnMouseDown() => Toggle();
    }
}