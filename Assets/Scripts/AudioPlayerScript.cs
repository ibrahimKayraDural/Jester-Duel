using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlayerScript : MonoBehaviour
{
    public static AudioPlayerScript Instance;

    [SerializeField] int _MenuIndex = 0;
    [SerializeField] GameObject _MenuSong;
    [SerializeField] GameObject _OtherSong;

    AudioSource _menuSongAS;
    AudioSource _otherSongAS;

    bool _isInitialized;

    void Start()
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
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }

        GameObject ms = Instantiate(_MenuSong);
        GameObject os = Instantiate(_OtherSong);

        DontDestroyOnLoad(ms);
        DontDestroyOnLoad(os);

        if (ms.TryGetComponent(out AudioSource aSource)) _menuSongAS = aSource;
        if (os.TryGetComponent(out aSource)) _otherSongAS = aSource;

        SelectSong(true);
        _isInitialized = true;
    }

    void SceneManager_sceneLoaded(Scene level, LoadSceneMode arg1)
    {
        SelectSong(level.buildIndex == _MenuIndex);
    }


    void SelectSong(bool selectMenu)
    {
        if (SelectSongRef != null) StopCoroutine(SelectSongRef);
        SelectSongRef = SelectSongIEnum(selectMenu);
        StartCoroutine(SelectSongRef);
    }
    IEnumerator SelectSongRef;
    IEnumerator SelectSongIEnum(bool selectMenu)
    {
        float speed = .01f;
        float amount = .01f;
        amount = selectMenu ? amount : -amount;

        for (int i = 0; i < 100; i++)
        {
            _menuSongAS.volume = Mathf.Clamp(_menuSongAS.volume + amount, 0, 1);
            _otherSongAS.volume = Mathf.Clamp(_otherSongAS.volume - amount, 0, 1);
            yield return new WaitForSeconds(speed);
        }

        _menuSongAS.volume = selectMenu ? 1 : 0;
        _otherSongAS.volume = selectMenu == false ? 1 : 0;
    }
}
