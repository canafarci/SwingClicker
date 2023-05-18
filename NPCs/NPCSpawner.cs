using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] GameObject _prefab;
    [SerializeField] float _minSpawn, _maxSpawn; float _currentSpawnMin, _currentSpawnMax;
    [SerializeField] Transform _target1, _target2;
    InputReader _reader;
    Coroutine _timeDecayRoutine;

    void Awake()
    {
        _reader = FindObjectOfType<InputReader>();
        _currentSpawnMin = _minSpawn;
        _currentSpawnMax = _maxSpawn;
    }
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
        _currentSpawnMin = _minSpawn / 2f;
        _currentSpawnMax = _maxSpawn / 2f;
        yield return new WaitForSeconds(.25f);
        _currentSpawnMin = _minSpawn;
        _currentSpawnMax = _maxSpawn;
    }
    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }
    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(_currentSpawnMin, _currentSpawnMax));
            MovingNPC npc = GameObject.Instantiate(_prefab, transform.position, transform.rotation).GetComponent<MovingNPC>();
            npc.Initialize(_target1, _target2);
        }
    }
}
