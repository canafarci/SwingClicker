using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftSpawner : MonoBehaviour
{
    [SerializeField] float _minTimeClicked, _maxTimeClicked, _minTimeIdle, _maxTimeIdle;
    [SerializeField] GameObject[] _gifts;
    InputReader _reader;
    Coroutine _timeDecayRoutine, _longSpawnRoutine, _shortSpawnRoutine;
    bool _isClicked = false;
    void Start() => StartCoroutine(SpawnLoop());
    void Awake() => _reader = FindObjectOfType<InputReader>();
    private void OnEnable() => _reader.ClickHandler += OnClick;
    private void OnDisable() => _reader.ClickHandler -= OnClick;
    private void OnClick()
    {
        if (_timeDecayRoutine != null)
            StopCoroutine(_timeDecayRoutine);

        _timeDecayRoutine = StartCoroutine(TimeDecay());
    }
    IEnumerator TimeDecay()
    {
        _isClicked = true;
        yield return new WaitForSeconds(3f);
        _isClicked = false;
        _timeDecayRoutine = null;
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);

            if (_isClicked)
            {
                if (_longSpawnRoutine != null)
                {
                    StopCoroutine(_longSpawnRoutine);
                    _longSpawnRoutine = null;
                }

                if (_shortSpawnRoutine == null)
                    _shortSpawnRoutine = StartCoroutine(ShortSpawn());
            }
            else
            {
                if (_shortSpawnRoutine != null)
                {
                    StopCoroutine(_shortSpawnRoutine);
                    _shortSpawnRoutine = null;
                }

                if (_longSpawnRoutine == null)
                    _longSpawnRoutine = StartCoroutine(LongSpawn());
            }
        }
    }

    IEnumerator LongSpawn()
    {
        float randTime = Random.Range(_minTimeIdle, _maxTimeIdle);
        yield return new WaitForSeconds(randTime);
        Spawn();
        _longSpawnRoutine = null;
    }
    IEnumerator ShortSpawn()
    {
        float randTime = Random.Range(_minTimeClicked, _maxTimeClicked);
        yield return new WaitForSeconds(randTime);
        Spawn();
        _shortSpawnRoutine = null;
    }

    private void Spawn()
    {
        GameObject spawnobj = _gifts[Random.Range(0, _gifts.Length)];
        GameObject.Instantiate(spawnobj, transform.position, transform.rotation);
    }
}
