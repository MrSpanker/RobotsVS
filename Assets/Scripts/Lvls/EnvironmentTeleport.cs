using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentTeleport : MonoBehaviour
{
    private Transform _playerTransform;
    private bool _isFrozen;
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] float _speed = 3f;
    [SerializeField] private float _rotationLerp = 3f;

    void FixedUpdate()
    {
        //return;
        if (_playerTransform == null) return;
        if (_isFrozen) return;
        Vector3 toPlayer = _playerTransform.position - transform.position;
        // TODO: Убрать магическое число
        if (toPlayer.magnitude > 32f)
        {
            transform.position += toPlayer * 1.95f;
        }
        Quaternion targetRotation = Quaternion.LookRotation(toPlayer);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _rotationLerp);
        _rigidbody.velocity = transform.forward * _speed;
    }
}
