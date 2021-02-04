using UnityEngine;
using UnityEngine;

public class CountableSurface : MonoBehaviour
{
    public int AmountOfPoints => amountOfPoints;

    [SerializeField] private int amountOfPoints = 1;
}