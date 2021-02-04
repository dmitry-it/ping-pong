using System;
using Popups;
using UnityEngine;
using UnityEngine.Assertions;

public class PopupFabric : MonoBehaviour
{
    [SerializeField] private Canvas uiCanvas;

    [SerializeField] private CanvasGroup gameCanvasGroup;

    private void Awake()
    {
        Assert.IsNotNull(uiCanvas);
        Assert.IsNotNull(gameCanvasGroup);
    }


    public void OpenPopup<T>(string popupName, Action<T> onOpened) where T : Popup
    {
        var go = Resources.Load<GameObject>("Popups/" + popupName);
        var popup = Instantiate(go, uiCanvas.transform, false);
        Assert.IsNotNull(popup);
        var component = popup.GetComponent<T>();
        gameCanvasGroup.interactable = false;
        gameCanvasGroup.blocksRaycasts = false;
        component.OnCloseEvent.AddListener(() =>
        {
            gameCanvasGroup.interactable = true;
            gameCanvasGroup.blocksRaycasts = true;
        });
        onOpened(component);
    }
}