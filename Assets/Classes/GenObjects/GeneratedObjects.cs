using UnityEngine;

namespace Classes.GenObjects
{
    public abstract class GeneratedObjects
    {
        private Transform transform;
        private GameObject gameObject;

        public Transform Transform => transform;
        public GameObject GameObject => gameObject;

        protected GeneratedObjects(Transform transform, GameObject gameObject)
        {
            this.transform = transform;
            this.gameObject = gameObject;
        }
    }
}
