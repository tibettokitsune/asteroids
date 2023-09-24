using System;
using Game.Scripts.Infrastructure;
using Game.Scripts.UI;
using UnityEngine;

namespace Game.Scripts.Units
{
    public class UnitsFactory
    {
        private UnitsConfiguration _unitsConfiguration;
        public UnitsFactory()
        {
            _unitsConfiguration = Resources.Load<UnitsConfiguration>("UnitsViewAsset");
        }
        public IUpdateItem CreatePlayer(IPlayerInput playerInput, GameplayHUDPanel hud)
        {
            var player = new PlayerPresenter(_unitsConfiguration.playerConfiguration, Vector2.zero, playerInput, hud);

            return player;
        }

        public IUpdateItem CreateEnemy()
        {
            return null;
        }
    }
}