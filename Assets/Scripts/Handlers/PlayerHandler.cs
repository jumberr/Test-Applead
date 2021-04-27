using Classes;
using UnityEngine;

namespace Handlers
{
    public class PlayerHandler : MonoBehaviour
    {
        
        [SerializeField] private GameObject playerPrefab;
        private Player player;

        private void Awake()
        {
            CreatePlayer(out var obj);
            var playerMovement = obj.GetComponent<PlayerMovement>();
            var animator = obj.GetComponent<Animator>();
            player = new Player(obj.transform, obj, animator, playerMovement);
        }

        private void CreatePlayer(out GameObject obj)
        {
            obj = Instantiate(playerPrefab, transform);
        }
    }
}
