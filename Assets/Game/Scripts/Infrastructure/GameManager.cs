using System;
using Game.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Infrastructure
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private LobbyPanel lobbyPanel;

        private const string GameplaySceneName = "GamePlay";

        private LevelManager _levelManager;
        private void Start()
        {
            lobbyPanel.OnGameStartClick += StartGame;
        }

        private void StartGame()
        {
            lobbyPanel.Hide();
            var loadingScene = SceneLoader.LoadSceneByName(GameplaySceneName);
            loadingScene.completed += o =>
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByName( GameplaySceneName));
                var levelManager = new GameObject("GameManager", typeof(LevelManager));
            };
        }
        
        private void RestartGame(){}
    }
}