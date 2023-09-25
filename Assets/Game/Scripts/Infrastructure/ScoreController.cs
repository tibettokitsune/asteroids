using Game.Scripts.UI;

namespace Game.Scripts.Infrastructure
{
    public class ScoreController
    {
        private readonly GameplayHUDPanel _hud;
        private int _score;
        
        public ScoreController(GameplayHUDPanel hud)
        {
            _hud = hud;
            _hud.UpdateScore(_score);
        }

        public void IncrementScore()
        {
            _score++;
            _hud.UpdateScore(_score);
        }
    }
}