using System;
using UnityEngine;

public class Prefs : MonoBehaviour
{
    public static Prefs Instance;

    public Language LanguagePref;
    public event Action OnRefresh;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetLanguage(Language languageToSet)
    {
        LanguagePref = languageToSet;
        OnRefresh?.Invoke();
    }
    public void SetLanguage(int languageAsInt)
    {
        LanguagePref = (Language)languageAsInt;
        OnRefresh?.Invoke();
    }
}
