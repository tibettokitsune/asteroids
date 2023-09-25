using Game.Scripts.UI;

namespace Game.Scripts.Infrastructure
{
    public class ScoreController
    {
        public int Score { get; private set; }
        private readonly GameplayHUDPanel _hud;
        
        public ScoreController(GameplayHUDPanel hud)
        {
            _hud = hud;
            _hud.UpdateScore(Score);
        }
        
        public void IncrementScore()
        {
            Score++;
            _hud.UpdateScore(Score);
        }
    }
}