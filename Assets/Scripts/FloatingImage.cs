using UnityEngine;

public class FloatingImage : MonoBehaviour
{
    [SerializeField] float _MoveSpeed = 1;
    [SerializeField] float _DissolveTime = 1;
    [SerializeField] SpriteRenderer _Renderer;

    void Update()
    {
        Color targetColor = _Renderer.color;
        targetColor.a = Mathf.Max(targetColor.a - Time.deltaTime / _DissolveTime, 0);
        _Renderer.color = targetColor;

        Vector2 targetPos = transform.position;
        targetPos.y += Time.deltaTime * _MoveSpeed;
        transform.position = targetPos;

        if (targetColor.a == 0) Destroy(gameObject);
    }
}
