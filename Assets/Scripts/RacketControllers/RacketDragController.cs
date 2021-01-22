using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.RacketControllers
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
            
                if (xCoordinate < moveMin)
                {
                    xCoordinate = moveMin;
              
                }
                else if (xCoordinate > moveMax)
                {
                    xCoordinate = moveMax;
            
                }

                rect.localPosition = new Vector3(xCoordinate, rect.position.y, rect.position.z);
            }
        }
    }
}