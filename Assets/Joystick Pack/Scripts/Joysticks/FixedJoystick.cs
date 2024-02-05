using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedJoystick : Joystick
{
    public Vector2 JoystickDirection { get; private set; }

    private void Update()
    {
        // Pobierz wartości ruchu z joysticka
        float horizontal = base.Horizontal;
        float vertical = base.Vertical;

        // Zapisz wartości jako kierunek joysticka
        JoystickDirection = new Vector2(horizontal, vertical).normalized;
    }
}