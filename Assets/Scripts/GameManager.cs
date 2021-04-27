using System.Collections.Generic;
using Classes.Builders;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Platforms")] [SerializeField] private Transform platformParent;
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private int platformsAmount = 10;
    [SerializeField] private float platformLength;
    private PlatformBuilder platformBuilder;

    [Space]
    [Header("Obstacles")] 
    [SerializeField] private Transform obstacleParent;
    [SerializeField] private GameObject[] obstaclesPrefabs;
    [SerializeField] private GameObject[] coinsPrefabs;
    [SerializeField] private int amountOfChunks = 5;
    private ObjectsBuilder objectsBuilder;

    [SerializeField] private Camera cam;

    private void Awake()
    {
        Time.timeScale = 1f;
        
        platformBuilder = new PlatformBuilder(platformPrefab, platformParent, platformsAmount, platformLength, cam);
        StartCoroutine(platformBuilder.ChangePlatformsPosition());

        FindBiggestLengthOfObstacle(out var length);
        objectsBuilder =
            new ObjectsBuilder(obstaclesPrefabs, coinsPrefabs, length, obstacleParent, amountOfChunks, platformLength, cam);
        StartCoroutine(objectsBuilder.ChangeObstaclesPosition());
    }

    private void FindBiggestLengthOfObstacle(out float length)
    {
        length = 0;
        for (var i = 0; i < obstaclesPrefabs.Length; i++)
        {
            var tempLength = obstaclesPrefabs[i].GetComponent<BoxCollider>().size.x;
            if (tempLength > length)
                length = tempLength;
        }
    }

    public static void ChangeParent(List<GameObject> groupsGO, List<List<GameObject>> list, int i, Transform parent,
        string name)
    {
        groupsGO.Add(new GameObject($"{name}s Group #{i}"));
        for (var j = 0; j < list[i].Count; j++)
        {
            list[i][j].transform.parent = groupsGO[i].transform;
        }

        groupsGO[i].transform.parent = parent;
    }

    public static GameObject InstantiateGeneratedObj(GameObject prefab, Vector3 position, Quaternion quaternion,
        Transform parent)
    {
        return Instantiate(prefab, position, quaternion, parent);
    }
}