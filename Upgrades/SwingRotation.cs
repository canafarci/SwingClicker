using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingRotation : MonoBehaviour
{
    [SerializeField] Transform _center;
    private void LateUpdate()
    {
        Vector3 relativePos = _center.position - transform.position;
        relativePos.y = 0;
        // the second argument, upwards, defaults to Vector3.up
        transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);
    }
}
