using System;
using System.Collections;
using _Game;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class Bribe : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    [SerializeField] private float moveDuration = 0.5f;
    [SerializeField] private Ease moveEase = Ease.OutQuad;

    private Vector3 _originalPosition;

    private Quaternion _originalRotation;
    private bool _isPickedUp;
    
    
    [Header("Настройки письма")]
    [SerializeField] private GameObject letter;

    public float moveDistance = 2f; // Насколько дверь поднимается
    public float animationDuration = 0.5f; // Длительность анимации
    public Ease animationEase = Ease.OutQuad; // Кривая анимации

    public Vector3 moveDirection;
    private Vector3 letterClosedPosition;
    private Vector3 letterOpenedPosition;
    private Tween currentTween;
    private bool isAnimated = false;
    public void OpenEnvilope()
    {
        isAnimated = true;
        _animator.SetTrigger("Open");
        StartCoroutine(LetterRoutine());
    }

    public void CloseEnvilope()
    {
        _animator.SetTrigger("Close");
    }
    
    private void Awake()
    {
        _originalPosition = transform.position;
        _originalRotation = transform.rotation;
        
        letterClosedPosition = letter.transform.localPosition;
    }
    
    private void OnMouseDown()
    {
        if(isAnimated) return;

        
        if (G.FolderPickup.isPickedUp)
        {
            G.FolderPickup.ReturnToOriginal();
        }
        
        if (!_isPickedUp)
            PickUp();
        else
           StartCoroutine(ReturnToOriginal());
    }
    
    public void PickUp()
    {
        _isPickedUp = true;

        transform.DOMove(G.BribeHand.position, moveDuration)
            .SetEase(moveEase);
        transform.DORotateQuaternion(G.BribeHand.rotation, moveDuration)
            .SetEase(moveEase).OnComplete(OpenEnvilope);
        
        
    }
    
    public IEnumerator ReturnToOriginal()
    {
        isAnimated = true;
        _isPickedUp = false;
        CloseLetter();
        yield return new WaitForSeconds(animationDuration);
        CloseEnvilope();
        yield return ReturnRoutine();
        isAnimated = false;
    }

    private IEnumerator ReturnRoutine()
    {
        yield return new WaitForSeconds(1f);
        transform.DOMove(_originalPosition, moveDuration)
            .SetEase(moveEase);
        transform.DORotateQuaternion(_originalRotation, moveDuration)
            .SetEase(moveEase);
    }

    private IEnumerator LetterRoutine()
    {
        yield return new WaitForSeconds(1f);
        OpenLetter();
    }
    
    [Button()]
    public void OpenLetter()
    {

        letterOpenedPosition = letterClosedPosition + moveDirection * moveDistance;

        currentTween?.Kill();
        currentTween = letter.transform.DOLocalMove(letterOpenedPosition, animationDuration)
            .SetEase(animationEase).OnComplete((() => isAnimated = false));
            
    }

    [Button()]
    public void CloseLetter()
    {
        isAnimated = true;
        currentTween?.Kill();
        currentTween = letter.transform.DOLocalMove(letterClosedPosition, animationDuration)
            .SetEase(animationEase);

    }
}
