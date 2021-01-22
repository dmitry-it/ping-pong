using UnityEngine;

namespace Assets.Scripts
{
    public class CountableSurface : MonoBehaviour
    {
        public int AmountOfPoints => _amountOfPoints;

        [SerializeField] private int _amountOfPoints = 1;
    }
}