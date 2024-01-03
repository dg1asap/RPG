using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasButtonController : MonoBehaviour
{
    public Image imageToToggle; // Przypisz obraz, który chcesz ukrywać i pokazywać.
    private bool isImageVisible = true;

    public void ToggleImageVisibility()
    {
        isImageVisible = !isImageVisible;
        imageToToggle.gameObject.SetActive(!isImageVisible);
    }
}
