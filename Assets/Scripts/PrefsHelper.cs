using UnityEngine;
using UnityEngine.Events;

public class PrefsHelper : MonoBehaviour
{
    [SerializeField] UnityEvent OnTurkish;
    [SerializeField] UnityEvent OnEnglish;

    void Start()
    {
        if(Prefs.Instance != null)
        {
            Prefs.Instance.OnRefresh += Refresh;
            Refresh();
        }
    }

    public void TryChangeLanguage(int languageAsInt)
    {
        if (Prefs.Instance != null) Prefs.Instance.SetLanguage(languageAsInt);
    }
    void Refresh()
    {
        switch (Prefs.Instance.LanguagePref)
        {
            case Language.Turkish:
                OnTurkish?.Invoke();
                break;
            case Language.English:
                OnEnglish?.Invoke();
                break;
        }
    }
}
