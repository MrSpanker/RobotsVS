using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private List<GameObject> _environmentList = new List<GameObject>();

    private GameManager _gameManager;
    private GameStateManager _gameStateManager;

    public void Init(GameManager gameManager, GameStateManager gameStateManager)
    {
        _gameManager = gameManager;
        _gameStateManager = gameStateManager;
        //_gameManager.OnUpLevel += UpLevel;
        //RegisterEnvironment();
    }
    

    public void RegisterEnvironment(GameObject objectToSpawn)
    {
        _environmentList.Add(objectToSpawn);
    }    
}
