using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{
    public EnvironmentManager EnvironmentManager;
    public Transform parentObject;
    public GameObject[] environments;

    [SerializeField][Range(0f, 1f)] public float density;
    private float spawnAreaSizeZ;
    private float spawnAreaSizeX;
    private int objectCount;
    private int objectRadius;

    public bool nextLvl;

    private void Start()
    {
        Renderer groundRenderer = GetComponent<Renderer>();
        spawnAreaSizeZ = groundRenderer.bounds.size.z;
        spawnAreaSizeX = groundRenderer.bounds.size.x;
    }
    private void Update()
    {
        if (nextLvl)
        {
            SpawnEnvironment();
            nextLvl = false;
        }
    }
    private void SpawnEnvironment()
    {
        objectCount = Mathf.RoundToInt((spawnAreaSizeZ * spawnAreaSizeX / 400) * density);
        int numberOfCopiesOfObject = objectCount / environments.Length;

        for (int i = 0; i < environments.Length; i++)
        {
            GameObject objectToSpawn = environments[i];
            float objectRadius = GetObjectRadius(i);

            for (int j = 0; j < numberOfCopiesOfObject; j++)
            {
                Vector3 spawnPosition;

                do
                {
                    spawnPosition = new Vector3(Random.Range(-spawnAreaSizeZ / 4, spawnAreaSizeZ / 4), 0, Random.Range(-spawnAreaSizeX / 4, spawnAreaSizeX / 4));
                } while (IsOverlaping(spawnPosition, objectRadius));

                Debug.LogError("ÇÀØ¨Ë Â EnvironmentManager.RegisterEnvironment(objectToSpawn);");

                EnvironmentManager.RegisterEnvironment(objectToSpawn);                
                GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.Euler(0, spawnPosition.x * 3, 0));
                spawnedObject.transform.parent = parentObject;
            }
        }       
    }
    private float GetObjectRadius(int objectNumber)
    {
        if (environments.Length > 0)
        {
            Collider objectCollider = environments[objectNumber].GetComponent<SphereCollider>();           
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
