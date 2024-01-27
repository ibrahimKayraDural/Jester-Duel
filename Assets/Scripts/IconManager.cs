using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconManager : MonoBehaviour
{
    public static IconManager Instance;

    [SerializeField] GameObject[] _Icons;//worst to best

    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    public void SpawnReactionIcon(FunDegree fun)
    {
        int index = (int)fun + 2;

        Instantiate(_Icons[index], transform.position, Quaternion.identity);
    }
}
