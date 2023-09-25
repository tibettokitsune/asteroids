using TMPro;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class GameplayHUDPanel : UIPanel
    {
        [SerializeField] private TextMeshProUGUI coordinatesLbl;
        [SerializeField] private TextMeshProUGUI rotationAngleLbl;
        [SerializeField] private TextMeshProUGUI velocityLbl;
        [SerializeField] private TextMeshProUGUI laserShootsLbl;
        [SerializeField] private TextMeshProUGUI laserCooldownLbl;
        [SerializeField] private TextMeshProUGUI scoreLbl;

        public void UpdateCoordinates(Vector2 coordinates) => coordinatesLbl.text = coordinates.ToString();
        public void UpdateRotationAngle(float angle)
        {
            var res = angle % 360;
            if (res < 0) res += 360f;
            rotationAngleLbl.text = res.ToString("F0");
        }

        public void UpdateVelocity(float velocity) => velocityLbl.text = velocity.ToString("F2");

        public void UpdateScore(int score) => scoreLbl.text = score.ToString();


        public void UpdateLaserTimer(float rechargeTime) => laserCooldownLbl.text = rechargeTime.ToString("F1");

        public void UpdateNumberOfLaserShoots(int availableNumberOfShoots) =>
            laserShootsLbl.text = availableNumberOfShoots.ToString();
    }
}