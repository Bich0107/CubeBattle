using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoSingleton<CanvasManager>
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] GameObject gameplayCanvas;
    [SerializeField] GameObject inventoryCanvas;

    public void OpenInventory()
    {
        playerInput.SetControlStatus(false);
        inventoryCanvas.SetActive(true);
        gameplayCanvas.SetActive(false);
    }

    public void CloseInventory()
    {
        playerInput.SetControlStatus(true);
        inventoryCanvas.SetActive(false);
        gameplayCanvas.SetActive(true);
    }
}
