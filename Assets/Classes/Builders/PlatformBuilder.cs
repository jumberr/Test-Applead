using System.Collections;
using Classes.GenObjects;
using UnityEngine;

namespace Classes.Builders
{
    public class PlatformBuilder : Builder<Platform>
    {
        private GameObject prefab;
        private Vector3 position = new Vector3(-50, 0, 0);
        private const float DURATION = 1f;

        public PlatformBuilder(GameObject prefab, Transform parent, int amount, float length, Camera cam) : base(parent, amount, length, cam)
        {
            this.prefab = prefab;
            InitializePlatforms();
        }

        private void InitializePlatforms()
        {
            for (var i = 0; i < Amount; i++)
            {
                position.x += Length;
                GeneratedObjects[i] = GameManager.InstantiateGeneratedObj(prefab, position, Quaternion.identity, Parent);
            }
        }
        
        public IEnumerator ChangePlatformsPosition()
        {
            var i = 0;
            var offset = 2f * Length;
            var camTransform = Camera.transform;
            var waitForSeconds = new WaitForSecondsRealtime(DURATION);

            while (true) // todo: while local player is alive
            {
                if (camTransform.position.x - offset > GeneratedObjects[i].transform.position.x)
                {
                    position.x += Length;
                    GeneratedObjects[i].transform.position = position;
                    i++;
                    if (i >= Amount)
                        i = 0;
                }
                yield return waitForSeconds;
            }
        }
    }
}
