using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class MovingNPC : MonoBehaviour
{
    NavMeshAgent _agent;
    [SerializeField] AnimationCurve _curve;
    [SerializeField] float _ascendTime, _targetHeight;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    public void Initialize(Transform tr1, Transform tr2)
    {
        StartCoroutine(Move(tr1, tr2));
    }

    IEnumerator Move(Transform tr1, Transform tr2)
    {
        yield return StartCoroutine(GetToPos(tr1.position));
        yield return StartCoroutine(GetToPos(tr2.position));

        GetComponentInChildren<Animator>().Play("fall_loop");
        _agent.enabled = false;
        Tween tween = transform.DOMoveY(_targetHeight, _ascendTime).SetEase(_curve);

        yield return tween.WaitForCompletion();
        Destroy(gameObject);
    }
    IEnumerator GetToPos(Vector3 target)
    {
        Vector3 tar = new Vector3(target.x, transform.position.y, target.z);
        _agent.destination = tar;
        while (Vector3.Distance(tar, transform.position) > _agent.stoppingDistance - 0.02f)
        {
            yield return new WaitForSeconds(0.2f);
        }
    }
}
