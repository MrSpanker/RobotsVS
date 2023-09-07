using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{
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
            Debug.LogError(i);
            GameObject objectToSpawn = environments[i];
            float objectRadius = GetObjectRadius(i);

            for (int j = 0; j < numberOfCopiesOfObject; j++)
            {
                Vector3 spawnPosition;

                do
                {
                    spawnPosition = new Vector3(Random.Range(-spawnAreaSizeZ / 4, spawnAreaSizeZ / 4), 0, Random.Range(-spawnAreaSizeX / 4, spawnAreaSizeX / 4));
                } while (IsOverlaping(spawnPosition, objectRadius));

                Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            }
        }       
    }
    private float GetObjectRadius(int objectNumber)
    {
        if (environments.Length > 0)
        {
            Collider objectCollider = environments[objectNumber].GetComponent<SphereCollider>();
            Debug.LogError(objectCollider);
            if (objectCollider != null)
            {
                Debug.LogError("objectCollider.bounds.extents.magnitude =" + objectCollider.bounds.extents.magnitude);
                return objectCollider.bounds.extents.magnitude;
            }
        }
        Debug.LogError("бепмск детнкр 0.1f");
        return 0.1f;
    }
    private bool IsOverlaping(Vector3 spawnPosition, float objectRadius)
    {
        Collider[] colliders = Physics.OverlapBox(spawnPosition, new Vector3(objectRadius, objectRadius, objectRadius), Quaternion.identity, LayerMask.GetMask("Ground"));
        return colliders.Length > 0;
    }
}
