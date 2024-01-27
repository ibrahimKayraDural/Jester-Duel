using UnityEngine;
using UnityEngine.Events;

public class CurtainController : MonoBehaviour
{
    [SerializeField] Animator _Animator;
    [SerializeField] bool _OpenOnStart = true;
    [SerializeField] UnityEvent OnOpened;
    [SerializeField] UnityEvent OnClosed;

    private void Start()
    {
        if (_OpenOnStart) OpenCurtain();
    }

    public void OpenCurtain()
    {
        _Animator.Play("open");
    }
    public void Opened() => OnOpened?.Invoke();

    public void CloseCurtain()
    {
        _Animator.Play("close");
    }
    public void Closed() => OnClosed?.Invoke();
}