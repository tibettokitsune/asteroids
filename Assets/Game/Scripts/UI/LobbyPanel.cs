using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class LobbyPanel : UIPanel
    {
        public Action OnGameStartClick;
        [SerializeField] private Button startGameBtn;

        private void Start()
        {
            startGameBtn.onClick.AddListener(() =>
            {
                OnGameStartClick.Invoke();
            });
        }
    }
}