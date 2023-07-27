using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private bool leftClickInput;

    private void Update()
    {
            leftClickInput = Input.GetMouseButton(0); 

        if (leftClickInput) GameEvents.current.LeftClick_Input();
        if (!leftClickInput) GameEvents.current.LeftClick_Input_Idle();
    }
}