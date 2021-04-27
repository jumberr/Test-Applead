using DG.Tweening;
using UnityEngine;

namespace Classes
{
    public class Player
    {
        public static Player LocalPlayer;
        
        private readonly Transform transform;
        private readonly GameObject gameObject;
        private PlayerMovement playerMovement;
        private readonly Animator animator;
        private int coins;
        private static readonly int GetCoin = Animator.StringToHash("GetCoin");

        public delegate void PlayerDie();
        public static event PlayerDie OnPlayerDie;
        public delegate void PlayerChange(int value);
        public static event PlayerChange OnChangeCoinsAmount;

        public Transform Transform => transform;
        public GameObject GameObject => gameObject;
        public PlayerMovement PlayerMovement => playerMovement;
        public int Coins => coins;
        
        public Player(Transform transform, GameObject gameObject, Animator animator, PlayerMovement playerMovement)
        {
            LocalPlayer = this;
            
            this.transform = transform;
            this.gameObject = gameObject;
            this.animator = animator;
            this.playerMovement = playerMovement;
            coins = LoadCoins();
            
            ObjectsCollision.OnCoinCollect += CollectCoin;
            ObjectsCollision.OnDeath += Die;
            
        }

        private void CollectCoin()
        {
            animator.SetTrigger(GetCoin);
            coins++;
            
            OnChangeCoinsAmount?.Invoke(coins);
            
            SaveCoins();
        }

        private void SaveCoins()
        {
            PlayerPrefs.SetInt("Coins", coins);
        }
        
        private int LoadCoins()
        {
            return PlayerPrefs.HasKey("Coins") ? PlayerPrefs.GetInt("Coins") : 0;
        }

        private void Die()
        {
            OnPlayerDie?.Invoke();
            
            ObjectsCollision.OnCoinCollect -= CollectCoin;
            ObjectsCollision.OnDeath -= Die;
            
            DOTween.Clear(true);
        }
    }
}
 