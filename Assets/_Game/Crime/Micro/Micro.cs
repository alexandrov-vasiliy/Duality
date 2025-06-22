using System;
using System.Collections;
using System.Collections.Generic;
using _Game;
using _Game.Crime;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.UI;

public class Micro : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform dialogueContainer;
    [SerializeField] private GameObject leftBubblePrefab;
    [SerializeField] private GameObject rightBubblePrefab;

    [Header("Timing")]
    [SerializeField] private float delayBetweenBubbles = 0.15f; // gap while printing two messages

    private List<Dialogue> _dialogues;
    private int _index;

    // References to the currently animating typewriter (if any)
    private TypewriterCore _activeTypewriter;
    private bool _isSpawningPair;

    #region Unity

    private void OnEnable()
    {
        G.crimeInitializator.newDay += HandleNewDay;
    }

    private void OnDisable()
    {
        G.crimeInitializator.newDay -= HandleNewDay;
    }

    private void OnMouseDown()
    {
        OnMicClick();
    }

    private void Start()
    {
        DayData day = G.crimeInitializator.currentDay;
        if (day == null)
        {
            Debug.LogError("[MicDialogueController] currentDay is null – ensure CrimeInitializator is set.");
            enabled = false;
            return;
        }

        _dialogues = day.Dialogues;
        _index = 0;
    }
    
    #endregion

    private void OnMicClick()
    {
        // 1) If there is an active typewriter, first try to fast‑forward / provide input
        if (_activeTypewriter != null)
        {
            if (_activeTypewriter.isShowingText)
            {
                // If <waitinput> is running, giving any input resumes it.
                // There isn’t a public API yet, so we simulate input by skipping.
                _activeTypewriter.SkipTypewriter();
                return; // Consume this click – either revealed or resumed.
            }
        }

        // 2) If we are spawning a pair, ignore further presses until done
        if (_isSpawningPair) return;

        // 3) Spawn next two messages (if any)
        if (_index < _dialogues.Count)
            StartCoroutine(SpawnPairCoroutine());
    }

    /// <summary>
    /// Instantiates up to two dialogue bubbles (Executor then Subject).
    /// </summary>
    private IEnumerator SpawnPairCoroutine()
    {
        _isSpawningPair = true;
        G.feel.PlayMicro();

        int toSpawn = Mathf.Min(2, _dialogues.Count - _index);
        for (int i = 0; i < toSpawn; i++)
        {
            Dialogue d = _dialogues[_index++];
            GameObject prefab = d.Speaker == Speaker.Executor ? rightBubblePrefab : leftBubblePrefab;
            if (prefab == null)
            {
                Debug.LogError($"[MicDialogueController] Missing bubble prefab for {d.Speaker}");
                continue;
            }

            GameObject go = Instantiate(prefab, dialogueContainer);
            TypewriterCore writer = go.GetComponent<TypewriterCore>();
            if (writer == null)
            {
                Debug.LogError("[MicDialogueController] Bubble prefab lacks Typewriter component.");
                continue;
            }

            // Keep reference so clicks can fast‑forward this line
            _activeTypewriter = writer;

            writer.ShowText(d.text); // Starts typewriter automatically when mode == OnShowText

            // Wait until the line is fully revealed (takes into account <waitfor> & <waitinput>)
            while (writer.isShowingText)
                yield return null;

            // Short pause before the next message in the same click cycle
            yield return new WaitForSeconds(delayBetweenBubbles);
        }

        // Scroll to bottom (if inside ScrollRect)
        if (dialogueContainer.GetComponentInParent<ScrollRect>() != null)
            Canvas.ForceUpdateCanvases();

        _isSpawningPair = false;
    }

    public void HandleNewDay()
    {
        _index = 0;

        _dialogues = G.crimeInitializator.currentDay.Dialogues;

        foreach (Transform child in dialogueContainer)
        {
            Destroy(child.gameObject);
        }
    }
}