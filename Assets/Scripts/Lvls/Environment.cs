using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    private bool _isFrozen;
    private Transform _playerTransform;
    int randomNumber;
    float positionX;

    public float cof = 1.9f;

    private void Start()
    {
        Debug.LogError("ß ÐÎÄÈËÑß !!!!!!");
    }

    //public void Init(Transform playerTransform, EnvironmentManager environmentManager)
    //{
    //    _playerTransform = playerTransform;
    //    _enemyManager = enemyManager;
    //    _enemyHit = Instantiate(_enemyHitPrefab, transform.position, Quaternion.identity);
    //    _enemyHit.Init();
    //}

    //private void FixedUpdate()
    //{
    //    if (Mathf.Abs(transform.position.x - spawnArea.position.x) > 2.5f || Mathf.Abs(transform.position.z - spawnArea.position.z) > 2.5f)
    //    {
    //        Vector3 oppositePosition = new Vector3(
    //            spawnArea.position.x - Mathf.Sign(currentPosition.x - spawnArea.position.x) * 2.5f,
    //            currentPosition.y,
    //            spawnArea.position.z - Mathf.Sign(currentPosition.z - spawnArea.position.z) * 2.5f
    //        );

    //        transform.position = oppositePosition;
    //    }
    //}
}
