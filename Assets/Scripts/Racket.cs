using RacketControllers;
using UnityEngine;

public class Racket : MonoBehaviour
{
    private RacketController _moveDragController;

    /// <summary>
    ///     Use for Init racket with manual control
    /// </summary>
    public void InitAsPlayerRacket()
    {
        _moveDragController = gameObject.AddComponent<RacketDragController>();
        _moveDragController.InitInContainer(transform.parent.gameObject.GetComponent<RectTransform>());
    }

    /// <summary>
    ///     Use to repeat movement of another racket
    /// </summary>
    /// <param name="transformToSync">Racket for mirroring</param>
    public void InitAsSyncedRacket(Transform transformToSync)
    {
        var controller = gameObject.AddComponent<SyncedRacketController>();
        controller.InitInContainer(transform.parent.gameObject.GetComponent<RectTransform>());
        controller.SyncWithOtherTransform(transformToSync);
        _moveDragController = controller;
    }
}