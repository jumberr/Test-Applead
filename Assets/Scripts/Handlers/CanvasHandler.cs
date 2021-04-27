using Classes;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Handlers
{
     public class CanvasHandler : MonoBehaviour
     {
          [SerializeField] private Button leftButton;
          [SerializeField] private Button rightButton;
          [SerializeField] private GameObject gameOver;
          [SerializeField] private GameObject pauseMenu;
          [SerializeField] private Text coinsText;

          private void Start()
          {
               var scene = SceneManager.GetActiveScene();
               if (scene.buildIndex == 0) return;
               var playerMovement = Player.LocalPlayer.PlayerMovement;
               leftButton.onClick.AddListener(() => { playerMovement.ChangeLine(1); });
               rightButton.onClick.AddListener(() => { playerMovement.ChangeLine(-1); });

               Player.OnPlayerDie += GameOver;
               Player.OnChangeCoinsAmount += ShowCoins;
               
               ShowCoins(Player.LocalPlayer.Coins);
          }

          private void ShowCoins(int value)
          {
               coinsText.text = value.ToString();
          }

          private void GameOver()
          {
               gameOver.SetActive(true);

               Player.OnPlayerDie -= GameOver;
               Player.OnChangeCoinsAmount -= ShowCoins;
               
               Time.timeScale = 0f;
          }

          public void RestartLevel()
          {
               var scene = SceneManager.GetActiveScene();
               DOTween.Clear(true);
               SceneManager.LoadScene(scene.name);
          }

          public void StartLevel(int i)
          {
               DOTween.Clear(true);
               SceneManager.LoadScene(i);
          }

          public void OpenPause()
          {
               Time.timeScale = 0f;
               pauseMenu.SetActive(true);
          }
          
          public void ClosePause()
          {
               Time.timeScale = 1f;
               pauseMenu.SetActive(false);
          }
     }
}
