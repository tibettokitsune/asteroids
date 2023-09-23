using UnityEngine;

namespace Game.Scripts.Units
{
    public class UnitView : MonoBehaviour
    {
        [SerializeField] private Transform viewObject;

        public void Rotate(float angle)
        {
            viewObject.transform.localEulerAngles = new Vector3(0, 0, angle);
        }

        public void Move(Vector2 position)
        {
            transform.position = position;
        }
        
        public void ForwardMove(float forwardValue)
        {
            transform.position += viewObject.transform.up * forwardValue;
        }
    }
}