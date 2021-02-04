using UnityEngine;

namespace RacketControllers
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class RacketController : MonoBehaviour
    {
        [SerializeField] protected RectTransform moveContainer;
        protected RectTransform Rect;


        protected float MoveMin;

        protected float MoveMax;

        private void Start()
        {
            Rect = gameObject.GetComponent<RectTransform>();
        }

        public void InitInContainer(RectTransform container)
        {
            moveContainer = container;
            Rect = GetComponent<RectTransform>();
            var rect = moveContainer.rect;
            MoveMin = rect.xMin + (Rect.rect.width / 2);
            MoveMax = rect.xMax - (Rect.rect.width / 2);
        }
    }
}