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
    [SerializeField] Button _Button;
    [SerializeField] Mover _Mover;

    Joke _currentJoke;
    Language _language;
    float _iterationWaitSeconds = .008f;
    bool _isShowingCard;
    bool _isMoving;

    public void SetJoke(Joke joke)
    {
        _currentJoke = joke;

        foreach (LanguagedText lText in CurrentJoke.JokeTexts)
        {
            if(_language == lText.Language)
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

        Vector2 targetPosition = targetTransform.position;

        _Mover.TryMove(targetPosition, _iterationWaitSeconds);
    }
    public void SetButtonEnablity(bool setTo)
    {
        _Button.enabled = setTo;
    }
    public void SetIterationSpeed(float setTo) => _iterationWaitSeconds = setTo;
    public void SetLanguage(Language language) => _language = language;
}