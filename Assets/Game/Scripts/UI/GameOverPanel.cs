using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class GameOverPanel : UIPanel
    {
        public Action OnGameRestartClick;
        [SerializeField] private Button restartGameBtn;

        private void Start()
        {
            restartGameBtn.onClick.AddListener(() =>
            {
                OnGameRestartClick.Invoke();
            });
        }
    }
}