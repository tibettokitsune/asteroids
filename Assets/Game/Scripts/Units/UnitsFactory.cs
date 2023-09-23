using System;
using Game.Scripts.Infrastructure;
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
        public IUpdateItem CreatePlayer(IPlayerInput playerInput)
        {
            var player = new PlayerPresenter(_unitsConfiguration.playerConfiguration, Vector2.zero, playerInput);

            return player;
        }

        public IUpdateItem CreateEnemy()
        {
            return null;
        }
    }
}