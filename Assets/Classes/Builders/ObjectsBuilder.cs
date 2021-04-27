using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Classes.GenObjects;
using UnityEngine;
using Object = Classes.GenObjects.Object;

namespace Classes.Builders
{
    public class ObjectsBuilder : Builder<Object>
    {
        private readonly GameObject[] obstaclesPrefab;
        private readonly GameObject[] coinsPrefab;   
        private readonly float lengthOfBiggestObj;

        private readonly List<GameObject> objectsGo = new List<GameObject>();
        private List<List<GameObject>> objects;
        
        private Vector3 position = Vector3.zero;
        private readonly Vector3 startOffset = new Vector3(25, -6f, 1.5f);
        private readonly Quaternion quaternion = Quaternion.Euler(45, 0, 0);
        private const float DURATION = 1f;
        private const int AMOUNT_OF_LINES = 5;
        private const int MAX_AMOUNT_OF_COINS = 2;

        public ObjectsBuilder(GameObject[] obstaclesPrefab, GameObject[] coinsPrefab, float lengthOfBiggestObj, Transform parent, int amount, float length, Camera cam) : base(
            parent, amount, length, cam)
        {
            this.obstaclesPrefab = obstaclesPrefab;
            this.coinsPrefab = coinsPrefab;
            this.lengthOfBiggestObj = lengthOfBiggestObj;
            InitializeObjects();
        }

        private void InitializeObjects()
        {
            position += startOffset;
            var halfLenght = Length / 2;

            objects = new List<List<GameObject>>();
            for (var i = 0; i < Amount; i++)
            {
                objects.Add(new List<GameObject>());
            }

            for (var i = 0; i < Amount; i++) // chunks
            {
                var amountOfObstacles = Random.Range(1, AMOUNT_OF_LINES);
                var amountOfCoins = Random.Range(0, MAX_AMOUNT_OF_COINS);
                var allObj = amountOfCoins + amountOfObstacles;
                
                var offsetSpawnObj = allObj * lengthOfBiggestObj / 2;
                
                var pos = Vector3.zero;

                CreateObj(obstaclesPrefab, amountOfObstacles, halfLenght, offsetSpawnObj, pos, i);
                CreateObj(coinsPrefab, amountOfCoins, halfLenght, offsetSpawnObj, pos, i);
                
                MoveOverlappingObjects(objects[i], lengthOfBiggestObj);
                GameManager.ChangeParent(objectsGo, objects, i, Parent, "Objects");
                objectsGo[i].transform.position = position;
                position.x += Length; // change position of chunks
            }
        }

        private void CreateObj(GameObject[] prefab, int amount, float halfLenght, float offsetSpawnObj, Vector3 pos, int i)
        {
            for (var j = 0; j < amount; j++)
            {
                var offsetX = Random.Range(-halfLenght + offsetSpawnObj, halfLenght - offsetSpawnObj);
                pos.x += offsetX;
                    
                var prefabObj = prefab[Random.Range(0, prefab.Length)]; // select prefab

                var obj = GameManager.InstantiateGeneratedObj(prefabObj, pos, quaternion, Parent);
                objects[i].Add(obj);

                var line = Random.Range(0, AMOUNT_OF_LINES);
                ChangeLine(objects[i][j], line); // change line of obstacle (Z coord)
            }
        }

        private void ChangeLine(GameObject gameObject, int line)
        {
            var pos = gameObject.transform.position;

            pos.z = line switch
            {
                0 => 0,
                1 => 5,
                2 => 10,
                3 => 15,
                4 => 20,
                _ => pos.z
            };
            
            pos.z += startOffset.z;
            gameObject.transform.position = pos;
        }
        
        public IEnumerator ChangeObstaclesPosition()
        {
            var i = 0;
            var offset = 1.5f * Length;
            var camTransform = Camera.transform;
            var waitForSeconds = new WaitForSecondsRealtime(DURATION);

            while (true) // todo: while local player is alive
            {
                if (camTransform.position.x - offset > objectsGo[i].transform.position.x)
                {
                    position.x += Length;
                    objectsGo[i].transform.position = position;
                    for (var j = 0; j < objects[i].Count; j++)
                    {
                        var random = Random.Range(0, AMOUNT_OF_LINES);
                        ChangeLine(objects[i][j], random);
                        if (!objects[i][j].activeSelf)
                        {
                            objects[i][j].SetActive(true);
                        }
                    }
                    MoveOverlappingObjects(objects[i], lengthOfBiggestObj);
                    i++;
                    if (i >= Amount)
                        i = 0;
                }
                yield return waitForSeconds;
            }
        }
        
        private void MoveOverlappingObjects(List<GameObject> list, float lengthOfBiggestObstacle)
        {
            list = list.OrderBy(go => go.transform.transform.position.x).ToList();

            var halfLenght = lengthOfBiggestObstacle / 2;
            for (var i = 0; i < list.Count; i++)
            {
                var pos = list[i].transform.position;
                pos.x +=  i * halfLenght;
                list[i].transform.position = pos;
            }
        }
    }
}