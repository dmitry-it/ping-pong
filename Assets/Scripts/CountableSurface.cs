using UnityEngine;

public class CountableSurface : MonoBehaviour
{
    [SerializeField] private int amountOfPoints = 1;
    public int AmountOfPoints => amountOfPoints;
}