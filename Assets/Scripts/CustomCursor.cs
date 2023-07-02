using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class CustomCursor : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Sprite cursorDef, cursorRot;
    private bool isLocked = false;
    private int cursorLockedX, cursorLockedY;
    private void Awake()
    {
        Cursor.visible = false;
    }
    private void Update()
    {
        if (isLocked)
            return;

        image.transform.position = Input.mousePosition;
    }

    internal void ResetMode() => SetMode(CustomCursorMode.Default);
    internal void SetMode(CustomCursorMode mode)
    {
        Sprite spr = cursorDef;
        switch (mode)
        {
            case CustomCursorMode.Rotate:
                spr = cursorRot;
                break;
        }
        image.sprite = spr;
    }
    internal void LockPosition()
    {
        isLocked = true;
        GetCursorPos(out cursorLockedX, out cursorLockedY);
    }

    internal void UnlockPosition()
    {
        isLocked = false;
        SetCursorPos(cursorLockedX, cursorLockedY);
    }

    [DllImport("user32.dll")]
    static extern bool GetCursorPos(out int X, out int Y);
    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int X, int Y);
}

public enum CustomCursorMode
{
    Default,
    Rotate,
}
