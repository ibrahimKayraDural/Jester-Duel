using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    public bool IsShowingCard => _isShowingCard;
    public Joke CurrentJoke => _currentJoke;

    [SerializeField] TextMeshProUGUI _JokeText;
    [SerializeField] Transform _ShowPosition;
    [SerializeField] Transform _HidePosition;
    [SerializeField, Min(0)] float _IterationWaitSeconds = .1f;
    [SerializeField] Language _Language;
    [SerializeField] Button _Button;

    Joke _currentJoke;
    bool _isShowingCard;
    bool _isMoving;

    public void SetJoke(Joke joke)
    {
        _currentJoke = joke;

        foreach (LanguagedText lText in CurrentJoke.JokeTexts)
        {
            if(_Language == lText.Language)
            {
                _JokeText.text = lText.Text;
            }
        }
    }
    public void SetShowCard(bool setTo)
    {
        if (_isMoving) return;

        Transform targetTransform = setTo ? _ShowPosition : _HidePosition;
        _isShowingCard = setTo;

        Vector2 targetPosition = transform.position;
        targetPosition.y = targetTransform.position.y;    
        
        if(refKey_GoToPosition != null) StopCoroutine(refKey_GoToPosition);
        refKey_GoToPosition = GoToPosition(targetPosition);
        StartCoroutine(refKey_GoToPosition);
    }
    public void SetButtonEnablity(bool setTo)
    {
        _Button.enabled = setTo;
    }

    IEnumerator refKey_GoToPosition;
    IEnumerator GoToPosition(Vector2 position) 
    {
        _isMoving = true;

        Vector2 targetVector = position - (Vector2)transform.position;
        Vector2 moveFraction = targetVector.normalized * .1f;
        int iteration = Mathf.CeilToInt(targetVector.magnitude / moveFraction.magnitude);
        
        for(int i = 0; i < iteration; i++)
        {
            transform.position = transform.position + (Vector3)moveFraction;
            yield return new WaitForSeconds(_IterationWaitSeconds);
        }

        transform.position = position;
        _isMoving = false;
    }
}