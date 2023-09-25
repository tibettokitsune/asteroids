using Game.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Infrastructure
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private LobbyPanel lobbyPanel;
        [SerializeField] private GameOverPanel gameOverPanel;

        private const string GameplaySceneName = "GamePlay";

        private LevelManager _levelManager;
        private void Start()
        {
            lobbyPanel.OnGameStartClick += StartGame;
            gameOverPanel.OnGameRestartClick += RestartGame;
        }

        private void StartGame()
        {
            lobbyPanel.Hide();
            var loadingScene = SceneLoader.LoadSceneByName(GameplaySceneName);
            loadingScene.completed += o =>
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByName( GameplaySceneName));
                var goInstance = new GameObject("GameManager");
                _levelManager = goInstance.AddComponent<LevelManager>();

                _levelManager.OnGameOver += GameOver;
            };
        }

        private void RestartGame()
        {
            gameOverPanel.Hide();
            StartGame();
        }

        private void GameOver(int score)
        {
            _levelManager.OnGameOver -= GameOver;
            var unloadingScene = SceneLoader.UnloadSceneByName(GameplaySceneName);
            unloadingScene.completed += o =>
            {
                _levelManager = null;
                gameOverPanel.Show();
                gameOverPanel.UpdateScoreLbl(score);
            };
        }
    }
}