using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class Rocket : MonoBehaviour
{
    private Enemy _targetEnemy;
    private float _speed;
    private float _damage;
    private float _timer;

    public void Init(Enemy targetEnemy, float damage, float speed)
    {
        _damage = damage;
        _targetEnemy = targetEnemy;
        _speed = speed;
        Destroy(gameObject, 4f);
    }
    private void Update()
    {
        _timer += Time.deltaTime;
        if(_timer < 0.5) {
            transform.position += Vector3.up * 0.1f;
        }
        else
        {
            if (_targetEnemy)
            {
                Vector3 toEnemy =   _targetEnemy.transform.position - transform.position;
                transform.position = Vector3.MoveTowards(transform.position, _targetEnemy.transform.position, _speed * Time.deltaTime);
                transform.Rotate(toEnemy);
                if (transform.position == _targetEnemy.transform.position)
                {
                    AffectEnemy();
                    Destroy(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        
    }

    void AffectEnemy()
    {
        _targetEnemy.SetDamage(_damage, true);
    }
}
