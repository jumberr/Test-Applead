using UnityEngine;

public class ObjectsCollision : MonoBehaviour
{
    public delegate void OnCollideObject();
    public static event OnCollideObject OnCoinCollect;
    public static event OnCollideObject OnDeath;
    
    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag("Player")) return;
        if (gameObject.CompareTag("Obstacle"))
        {
            OnDeath?.Invoke();
        }
        else if (gameObject.CompareTag("Coin"))
        {
            OnCoinCollect?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
