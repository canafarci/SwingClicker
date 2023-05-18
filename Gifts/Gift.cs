using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour
{
    public bool IsPickedUp = false;
    public bool IsGrounded { get { return _isGrounded; } }
    bool _isGrounded = false;
    private void Start()
    {
        StartCoroutine(Init());
    }
    private void OnDisable()
    {
        References.Instance.Gifts.Remove(this);
    }
    private void OnEnable()
    {
        References.Instance.Gifts.Add(this);
    }
    IEnumerator Init()
    {
        yield return new WaitForSeconds(2f);
        _isGrounded = true;
    }
}
