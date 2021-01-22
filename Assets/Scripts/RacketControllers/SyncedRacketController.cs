using UnityEngine;

namespace Assets.Scripts.RacketControllers
{
    public class SyncedRacketController : RacketController
    {
        private Transform syncedObjectTransform;

        public void SyncWithOtherTransform(Transform transform)
        {
            syncedObjectTransform = transform;
        }
        
        private void Update()
        {
            SetNormalizedPosition();
        }
        
        private void SetNormalizedPosition()
        {
            
            var xCoordinate = syncedObjectTransform == null ? 0f : syncedObjectTransform.position.x;
            
            if (xCoordinate < moveMin)
            {
                xCoordinate = moveMin;
              
            }
            else if (xCoordinate > moveMax)
            {
                xCoordinate = moveMax;
            
            }

            rect.position = new Vector3(xCoordinate, rect.position.y, rect.position.z);
        }
        
    }
}
