using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
//using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnvironmentTeleport : MonoBehaviour
{
    public Transform _playerTransform;
    private bool _isFrozen;

    int randomNumber;
    float positionX;

    public float cof = 1.9f;

    private void Start()
    {
        GameObject foundObject = GameObject.Find("Player");
        _playerTransform = foundObject.GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        //return;
        if (_playerTransform == null) return;
        if (_isFrozen) return;
        
        Vector3 toPlayer = _playerTransform.transform.position - transform.position;
        if (toPlayer.magnitude > 100f)
        {
            Vector3 newPosition;
            if (Mathf.Abs(_playerTransform.transform.position.x - transform.position.x) >= Mathf.Abs(_playerTransform.transform.position.y - transform.position.y))
            {
                newPosition = transform.position + toPlayer * cof;
                float newPositionX = (_playerTransform.transform.position.x - transform.position.x) * cof;
                newPosition = new Vector3(newPositionX, newPosition.y, newPosition.z);                
            }
            else
            {
                newPosition = transform.position + toPlayer * cof;
                float newPositionY = (_playerTransform.transform.position.y - transform.position.y) * cof;
                newPosition = new Vector3(newPositionY, newPosition.y, newPosition.z);
            }
            transform.position = newPosition;
            //transform.position += toPlayer * 1.5f;
            randomNumber = Random.Range(0, 360);
            transform.rotation = Quaternion.Euler(0, randomNumber, 0);
        }        
    }
}
