using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;

    //[SerializeField] private ChapterSettings _chapterSettings;

    [SerializeField] private Renderer _groundRenderer;
    private GameManager _gameManager;
    private GameStateManager _gameStateManager;

    private string resourcePath;
    [SerializeField] public int LvlNumber = 1;
    [SerializeField] private Environment[] loadedObjects;

    private int objectCount;
    private float spawnAreaSizeZ;
    private float spawnAreaSizeX;
    private int copiesOfOneObject;
    [SerializeField][Range(0f, 1f)] public float density;
    Vector3 spawnPosition;
    float objectRadius;

    //[SerializeField] Environment _environment;

    private Transform parentObject;

    public void Init(GameManager gameManager, GameStateManager gameStateManager)
    {
        _gameManager = gameManager;
        _gameStateManager = gameStateManager;

        spawnAreaSizeZ = _groundRenderer.bounds.size.z;
        spawnAreaSizeX = _groundRenderer.bounds.size.x;
        parentObject = transform.parent;
        LoadObjects();
        //SpawnEnvironment();
    }

    public void LoadObjects()
    {
        resourcePath = "Assets/Resources/Lvl " + LvlNumber;


        string[] files = Directory.GetFiles(resourcePath)
            .Where(file => !file.EndsWith(".meta"))
            .ToArray();

        loadedObjects = Resources.LoadAll<Environment>(resourcePath);

        if (loadedObjects.Length == 0)
        {
            Debug.LogWarning("Не удалось найти объекты в ресурсах для уровня " + LvlNumber);
        }
        else
        {
            Debug.Log("Загружено " + loadedObjects.Length + " объектов из ресурсов для уровня " + LvlNumber);
        }
    }
    private void SpawnEnvironment()
    {
        objectCount = Mathf.RoundToInt((spawnAreaSizeZ * spawnAreaSizeX / 400) * density);
        copiesOfOneObject = objectCount / loadedObjects.Length;

        for (int i = 0; i < loadedObjects.Length; i++)
        {
            Environment objectToSpawn = loadedObjects[i];
            //objectRadius = GetObjectRadius(i);

            for (int j = 0; j < copiesOfOneObject; j++)
            {
                do
                {
                    spawnPosition = new Vector3(Random.Range(-spawnAreaSizeZ / 4, spawnAreaSizeZ / 4), 0, Random.Range(-spawnAreaSizeX / 4, spawnAreaSizeX / 4));
                } while (IsOverlaping(spawnPosition, objectRadius));

                Environment spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.Euler(0, spawnPosition.x * 3, 0));
                spawnedObject.transform.parent = parentObject;
            }
        }
    }

    private float GetObjectRadius(int objectNumber)
    {
        if (loadedObjects.Length > 0)
        {
            Collider objectCollider = loadedObjects[objectNumber].GetComponent<SphereCollider>();
            if (objectCollider != null)
            {
                return objectCollider.bounds.extents.magnitude;
            }
        }
        return 0.1f;
    }
    private bool IsOverlaping(Vector3 spawnPosition, float objectRadius)
    {
        Collider[] colliders = Physics.OverlapBox(spawnPosition, new Vector3(objectRadius, objectRadius, objectRadius), Quaternion.identity, LayerMask.GetMask("Ground"));
        return colliders.Length > 0;
    }
}

