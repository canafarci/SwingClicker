using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableNewJoint : MonoBehaviour
{
    [SerializeField] GameObject _gameObject;


    public void EnableObject()
    {
        gameObject.SetActive(false);
        _gameObject.SetActive(true);
    }
}
