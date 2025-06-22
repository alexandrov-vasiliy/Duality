using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Crime
{
    /// <summary>
    /// Simple push‑button behaviour.  Scales the designated <see cref="push"/> transform
    /// on the Y axis when pressed and restores it when released, with DOTween‑driven
    /// animation settings exposed in the inspector.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class MercyButton : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("Object that will visually depress when the button is pressed. If left empty, the component's own transform is used.")]
        [SerializeField] private Transform push;

        [Header("Animation Settings")]
        [Tooltip("How much of the original Y‑scale remains while the button is held down (0 = fully flat, 1 = no change)."), Range(0f, 1f)]
        [SerializeField] private float pushFactor = 0.8f;

        [Tooltip("Time in seconds for the press animation.")]
        [SerializeField] private float pressDuration = 0.15f;
        [Tooltip("Time in seconds for the release animation.")]
        [SerializeField] private float releaseDuration = 0.15f;

        [SerializeField] private Ease pressEase = Ease.OutQuad;
        [SerializeField] private Ease releaseEase = Ease.OutQuad;

        [Header("Events")]
        public UnityEvent onClick;

        private Vector3 _originalScale;
        private Tween _currentTween;
        private bool _isPressed;

        private void Awake()
        {
            if (push == null) push = transform;
            _originalScale = push.localScale;
        }

        private void OnMouseDown()
        {
            if(G.RunState.isBlocked) return;

            _isPressed = true;
            AnimateScale(_originalScale.y * pushFactor, pressDuration, pressEase);
            G.feel.PlayBtn();
        }

        private void OnMouseUp()
        {
            if(G.RunState.isBlocked) return;

            if (!_isPressed) return;
            _isPressed = false;
            AnimateScale(_originalScale.y, releaseDuration, releaseEase);
            onClick?.Invoke();
        }

        private void OnMouseExit()
        {
            // Restore if the cursor leaves the collider without releasing the button
            if (!_isPressed) return;
            _isPressed = false;
            AnimateScale(_originalScale.y, releaseDuration, releaseEase);
        }

        private void AnimateScale(float targetY, float duration, Ease ease)
        {
            _currentTween?.Kill();
            _currentTween = push.DOScaleY(targetY, duration).SetEase(ease);
        }

        private void OnDisable()
        {
            _currentTween?.Kill();
            if (push != null)
                push.localScale = _originalScale;
        }
    }
}

