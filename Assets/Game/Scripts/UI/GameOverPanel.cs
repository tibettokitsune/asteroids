using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class GameOverPanel : UIPanel
    {
        public Action OnGameRestartClick;
        [SerializeField] private Button restartGameBtn;
        [SerializeField] private TextMeshProUGUI scoreLbl;
        private void Start()
        {
            restartGameBtn.onClick.AddListener(() =>
            {
                OnGameRestartClick.Invoke();
            });
        }

        public void UpdateScoreLbl(int score)
        {
            scoreLbl.text = $"Your score {score}";
        }
    }
}