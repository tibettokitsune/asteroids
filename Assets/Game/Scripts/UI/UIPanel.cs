using Unity.Collections;
using UnityEngine;

namespace Game.Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIPanel : MonoBehaviour
    {
        [SerializeField, ReadOnly] private CanvasGroup canvasGroup;

        private void OnValidate()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Show() => ChangeVisibility(true);

        public void Hide() => ChangeVisibility(false);

        private void ChangeVisibility(bool state)
        {
            canvasGroup.alpha = state? 1f : 0f;
            canvasGroup.interactable = state;
            canvasGroup.blocksRaycasts = state;
        }
    }
}