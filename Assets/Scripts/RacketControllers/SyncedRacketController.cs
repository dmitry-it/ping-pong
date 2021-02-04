using UnityEngine;

namespace RacketControllers
{
    public class SyncedRacketController : RacketController
    {
        private Transform _syncedObjectTransform;

        private void Update()
        {
            SetNormalizedPosition();
        }

        public void SyncWithOtherTransform(Transform transform)
        {
            _syncedObjectTransform = transform;
        }

        private void SetNormalizedPosition()
        {
            var xCoordinate = _syncedObjectTransform == null ? 0f : _syncedObjectTransform.position.x;

            if (xCoordinate < MoveMin)
                xCoordinate = MoveMin;
            else if (xCoordinate > MoveMax) xCoordinate = MoveMax;

            var position = Rect.position;
            position = new Vector3(xCoordinate, position.y, position.z);
            Rect.position = position;
        }
    }
}