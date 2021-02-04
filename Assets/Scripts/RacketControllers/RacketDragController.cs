using UnityEngine;
using UnityEngine.EventSystems;

namespace RacketControllers
{
    public class RacketDragController : RacketController, IDragHandler
    {
   

        public void OnDrag(PointerEventData eventData)
        {
            SetNormalizedPosition(eventData);
        }

        private void SetNormalizedPosition(PointerEventData data)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(moveContainer, data.position,
                data.pressEventCamera, out var globalMousePos))
            {
                var xCoordinate = globalMousePos.x;
            
                if (xCoordinate < MoveMin)
                {
                    xCoordinate = MoveMin;
              
                }
                else if (xCoordinate > MoveMax)
                {
                    xCoordinate = MoveMax;
            
                }

                Rect.localPosition = new Vector3(xCoordinate, Rect.position.y, Rect.position.z);
            }
        }
    }
}