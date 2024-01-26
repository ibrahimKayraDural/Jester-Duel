using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] CardController[] cardControllers;

    private void Update()
    {
        //CheckForCards();


        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (CardController c in cardControllers)
            {
                c.SetShowCard(!c.IsShowingCard);
            }
        }
    }

    //private void CheckForCards()
    //{
    //    Vector2 mousePos = Input.mousePosition;
    //    mousePos = Camera.main.ScreenToWorldPoint(mousePos);

    //    RaycastHit2D[] tempHitArray = Physics2D.RaycastAll(mousePos, Vector2.zero, .1f, 1 << 6);
    //    if (tempHitArray.Length <= 0)
    //    {

    //    }

    //    RaycastHit2D hit = tempHitArray[0];

    //    hit.Hover();
    //}
}
