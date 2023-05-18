using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float _maxVelocity, _minVelocity;
    InputReader _reader;
    Coroutine _timeDecayRoutine;
    [SerializeField] float _speedUpRate = 20f;
    HingeJoint _joint;

    void Awake()
    {
        _reader = FindObjectOfType<InputReader>();
        _joint = GetComponent<HingeJoint>();
    }
    private void OnEnable() => _reader.ClickHandler += OnClick;
    private void OnDisable() => _reader.ClickHandler -= OnClick;

    private void Start()
    {
        _joint.useMotor = true;

        JointMotor motor = new JointMotor();
        motor.targetVelocity = _minVelocity;
        motor.freeSpin = true;
        motor.force = 50f;

        _joint.motor = motor;
    }
    private void OnClick()
    {
        if (_timeDecayRoutine != null)
            StopCoroutine(_timeDecayRoutine);

        _timeDecayRoutine = StartCoroutine(TimeDecay());
    }


    IEnumerator TimeDecay()
    {
        if (_joint.motor.targetVelocity < _maxVelocity)
        {
            JointMotor motor = new JointMotor();
            motor.targetVelocity = _joint.velocity + _speedUpRate;
            motor.freeSpin = true;
            motor.force = 50f;

            _joint.motor = motor;
        }

        yield return new WaitForSeconds(.25f);

        while (_joint.motor.targetVelocity > _minVelocity)
        {
            yield return new WaitForSeconds(.25f);

            JointMotor newMotor = new JointMotor();
            newMotor.force = 50f;
            newMotor.freeSpin = true;
            newMotor.targetVelocity = _joint.velocity - _speedUpRate;

            _joint.motor = newMotor;
        }

        _timeDecayRoutine = null;
    }
}
