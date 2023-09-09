using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private List<Environment> _environmentList = new List<Environment>();

    private GameManager _gameManager;
    private GameStateManager _gameStateManager;

    public void Init(GameManager gameManager, GameStateManager gameStateManager)
    {
        _gameManager = gameManager;
        _gameStateManager = gameStateManager;
        //_gameManager.OnUpLevel += UpLevel;
        SetupEnvironment();
    }

    private void SetupEnvironment()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject enemyGameObject = transform.GetChild(i).gameObject;
            if (enemyGameObject.activeSelf)
            {
                Environment environment = enemyGameObject.GetComponent<Environment>();
                _environmentList.Add(environment);
                environment.Init(_playerTransform, this);
            }
        }

        _environmentList.Clear();

        for (int i = 0; i < _chapterSettings.EnemyWavesArray.Length; i++)
        {
            _environmentList.Add(_chapterSettings.EnemyWavesArray[i].Enemy);
        }
    }
    private void NextLvl(int lvl)
    {

    }
}
