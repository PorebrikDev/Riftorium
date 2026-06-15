using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.UI.VirtualMouseInput;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    private Vector2 hotspot = new Vector2(0, 0);

    private void Start()
    {
        UnityEngine.Cursor.SetCursor(cursorTexture, hotspot, UnityEngine.CursorMode.Auto);
        UnityEngine.Cursor.visible = true;
    }
}

