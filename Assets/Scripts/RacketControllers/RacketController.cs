using UnityEngine;

namespace Assets.Scripts.RacketControllers
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class RacketController : MonoBehaviour
    {
        [SerializeField] protected RectTransform moveContainer;
        protected RectTransform rect;


        protected float moveMin;

        protected float moveMax;

        private void Start()
        {
            rect = gameObject.GetComponent<RectTransform>();
        }

        public void InitInContainer(RectTransform container)
        {
            moveContainer = container;
            rect = GetComponent<RectTransform>();
            moveMin = moveContainer.rect.xMin + (rect.rect.width / 2);
            moveMax = moveContainer.rect.xMax - (rect.rect.width / 2);
        }
    }
}