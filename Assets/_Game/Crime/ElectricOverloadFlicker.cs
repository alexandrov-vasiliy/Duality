using System.Collections;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace _Game.Crime
{
    /// <summary>
    /// Simulates an electrical overload by flickering a set of Light components using DOTween.
    /// Interval between bursts, number of flashes per burst, and the spacing between flashes
    /// are all randomised within ranges exposed in the Inspector via NaughtyAttributes sliders.
    /// </summary>
    [DisallowMultipleComponent]
    public class ElectricOverloadFlicker : MonoBehaviour
    {
        [Header("Lights")]
        [SerializeField] private Light[] lights;

        [Header("Intensity")]
        [SerializeField, Min(0f)] private float baseIntensity = 0.8f;
        [SerializeField, Min(0f)] private float overloadIntensity = 3f;

        [Header("Timing")]
        [InfoBox("All timings randomised each burst")] 
        [MinMaxSlider(0f, 5f)]
        public Vector2 intervalRange = new Vector2(1f, 2f);

        [MinMaxSlider(1, 20)]
        public Vector2Int burstCountRange = new Vector2Int(6, 10);

        [MinMaxSlider(0.01f, 0.5f), Tooltip("Interval between individual flashes inside a burst (seconds)")]
        public Vector2 burstIntervalRange = new Vector2(0.04f, 0.08f);

        [Header("Tween")]
        [SerializeField] private Ease ease = Ease.OutExpo;
        [Tooltip("Automatically start when this component becomes enabled")]
        [SerializeField] private bool playOnEnable = true;

        private Sequence flickerSeq;
        private bool isRunning;

        private void Awake()
        {
            SetIntensity(baseIntensity);
        }

        private void OnEnable()
        {
            if (playOnEnable) StartFlicker();
        }

        private void OnDisable()
        {
            StopFlicker();
        }

        /// <summary>
        /// Starts the flicker loop if it is not already running.
        /// </summary>
        public void StartFlicker()
        {
            if (isRunning) return;
            isRunning = true;
            StartCoroutine(FlickerLoop());
        }

        /// <summary>
        /// Stops flicker loop and resets lights back to base intensity.
        /// </summary>
        public void StopFlicker()
        {
            isRunning = false;
            KillSequence();
            SetIntensity(baseIntensity);
        }

        private IEnumerator FlickerLoop()
        {
            while (isRunning)
            {
                // Random delay before the next burst starts
                float wait = Random.Range(intervalRange.x, intervalRange.y);
                yield return new WaitForSeconds(wait);

                KillSequence();

                int burstCount = Random.Range(burstCountRange.x, burstCountRange.y + 1);
                float burstInterval = Random.Range(burstIntervalRange.x, burstIntervalRange.y);
                float half = burstInterval * 0.5f;

                flickerSeq = DOTween.Sequence();

                for (int i = 0; i < burstCount; i++)
                {
                    float t = i * burstInterval;
                    foreach (var light in lights)
                    {
                        flickerSeq.Insert(t,       light.DOIntensity(overloadIntensity, half).SetEase(ease));
                        flickerSeq.Insert(t + half, light.DOIntensity(baseIntensity,     half).SetEase(ease));
                    }
                }

                yield return flickerSeq.WaitForCompletion();
            }
        }

        private void KillSequence()
        {
            if (flickerSeq != null && flickerSeq.IsActive())
            {
                flickerSeq.Kill();
                flickerSeq = null;
            }
        }

        private void SetIntensity(float value)
        {
            foreach (var light in lights)
            {
                if (light) light.intensity = value;
            }
        }
    }
}
