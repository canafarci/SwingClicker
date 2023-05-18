using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BoingKit;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshNPC : MonoBehaviour
{
    NavMeshAgent _agent;
    Animator _animator;
    Vector3 _startPos;
    [SerializeField] Transform _hand;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        _startPos = transform.position;
    }
    private void Start()
    {
        StartCoroutine(CheckLoop());
    }
    IEnumerator CheckLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            List<Gift> gifts = References.Instance.Gifts.ToList();
            foreach (Gift gift in gifts)
            {
                if (gift.IsPickedUp || !gift.IsGrounded) continue;

                gift.IsPickedUp = true;
                gift.GetComponent<Rigidbody>().isKinematic = true;
                yield return StartCoroutine(PickupAndLeave(gift));
            }
        }
    }

    IEnumerator PickupAndLeave(Gift gift)
    {
        _animator.Play("happy_walk");
        yield return StartCoroutine(GetToPos(gift.transform.position));
        _animator.Play("pickup_v1");
        yield return new WaitForSeconds(0.5f);
        gift.transform.parent = _hand.transform;
        gift.transform.DOLocalMove(new Vector3(0.967000008f, 0.075000003f, 0.80400002f), 0.3f);
        gift.transform.GetComponent<BoingBehavior>().enabled = false;
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(GetToPos(_startPos));
        Destroy(gift.gameObject);
    }

    IEnumerator GetToPos(Vector3 target)
    {
        Vector3 tar = new Vector3(target.x, transform.position.y, target.z);
        _agent.destination = tar;
        while (Vector3.Distance(tar, transform.position) > _agent.stoppingDistance + 0.02f)
        {
            yield return new WaitForSeconds(0.2f);
        }
    }
}
