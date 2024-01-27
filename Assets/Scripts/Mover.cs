using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public bool IsMoving => _isMoving;

    bool _isMoving;


    public void TryMove(Vector2 targetPosition, float iterationWaitSeconds, bool finishCurrentMovement = true)
    {
        if (IsMoving && finishCurrentMovement) return;

        if (refKey_GoToPosition != null) StopCoroutine(refKey_GoToPosition);
        refKey_GoToPosition = GoToPosition(targetPosition, iterationWaitSeconds);
        StartCoroutine(refKey_GoToPosition);
    }

    IEnumerator refKey_GoToPosition;
    IEnumerator GoToPosition(Vector2 position, float iterationWaitSeconds)
    {
        _isMoving = true;

        Vector2 targetVector = position - (Vector2)transform.position;
        Vector2 moveFraction = targetVector.normalized * .1f;
        int iteration = Mathf.CeilToInt(targetVector.magnitude / moveFraction.magnitude);

        for (int i = 0; i < iteration; i++)
        {
            transform.position = transform.position + (Vector3)moveFraction;
            yield return new WaitForSeconds(iterationWaitSeconds);
        }

        transform.position = position;
        _isMoving = false;
    }
}
