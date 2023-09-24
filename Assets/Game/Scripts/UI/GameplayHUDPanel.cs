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

        public void UpdateCoordinates(Vector2 coordinates) => coordinatesLbl.text = coordinates.ToString();
        public void UpdateRotationAngle(float angle)
        {
            var res = angle % 360;
            if (res < 0) res += 360f;
            rotationAngleLbl.text = res.ToString("F0");
        }

        public void UpdateVelocity(float velocity) => velocityLbl.text = velocity.ToString("F1");
        
        
    }
}