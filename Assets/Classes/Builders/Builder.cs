using Classes.GenObjects;
using UnityEngine;

namespace Classes.Builders
{
    public class Builder<T> where T : GeneratedObjects
    {
        private Transform parent;
        private int amount;
        private float length;
        private Camera cam;
        private GameObject[] generatedObjects;
        
        public Transform Parent => parent;
        public int Amount => amount;
        public float Length => length;
        public Camera Camera => cam;
        public GameObject[] GeneratedObjects
        {
            get => generatedObjects;
            set => generatedObjects = value;
        }
        
        protected Builder(Transform parent, int amount, float length, Camera cam)
        {
            this.parent = parent;
            this.amount = amount;
            this.length = length;
            this.cam = cam;
            generatedObjects = new GameObject[amount];
        }
        
    }
}
