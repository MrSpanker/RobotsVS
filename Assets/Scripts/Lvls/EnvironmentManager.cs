using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private List<GameObject> _environmentList = new List<GameObject>();

    private GameManager _gameManager;
    private GameStateManager _gameStateManager;

    public string folderPath; 
    public GameObject[] loadedObjects;
    public int LvlNumber = 1;

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
    public void LoadObjects()
    {
        
        string folderPath = "Assets/Lvls/Lvl " + LvlNumber;
        
        if (Directory.Exists(folderPath))
        {
            string[] files = Directory.GetFiles(folderPath);
            loadedObjects = new GameObject[files.Length];

            for (int i = 0; i < files.Length; i++)
            {
                GameObject obj = Resources.Load<GameObject>(folderPath + "/" + Path.GetFileNameWithoutExtension(files[i]));
                if (obj != null)
                {
                    loadedObjects[i] = obj;
                }
                else
                {
                    Debug.LogWarning("Не удалось загрузить объект из файла: " + files[i]);
                }
            }
        }
        else
        {
            Debug.LogError("Папка не найдена: " + folderPath);
        }
    }
}

