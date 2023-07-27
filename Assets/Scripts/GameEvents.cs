using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action onIdle;
    public event Action onLeftClick_Input;
    public event Action onLeftClick_Input_Idle;
    public event Action onTakeDamage;
    public event Action onGainXP;
    public event Action onLevelUp;
    public event Action onDie;
    public event Action onChugging;
    public event Action onChugging_Idle;
    public event Action onBooking;
    public event Action onBooking_Idle;

    public void Idle()
    {
        if (onIdle != null)
        {
            onIdle();
        }
    }        
    public void LeftClick_Input()
    {
        if (onLeftClick_Input != null)
        {
            onLeftClick_Input();
        }
    }    
    public void LeftClick_Input_Idle()
    {
        if (onLeftClick_Input_Idle != null)
        {
            onLeftClick_Input_Idle();
        }
    }
    public void TakeDamage()
    {
        if (onTakeDamage != null)
        {
            onTakeDamage();
        }
    }        
    public void GainXP()
    {
        if (onGainXP != null)
        {
            onGainXP();
        }
    }       
    public void LevelUp()
    {
        if (onTakeDamage != null)
        {
            onTakeDamage();
        }
    }    
    public void Die()
    {
        if (onDie != null)
        {
            onDie();
        }
    }
    public void Chugging()
    {
        if (onChugging != null)
        {
            onChugging();
        }
    }        
    public void Chugging_Idle()
    {
        if (onChugging != null)
        {
            onChugging();
        }
    }    
    public void Booking()
    {
        if (onBooking != null)
        {
            onBooking();
        }
    }    
    public void Booking_Idle()
    {
        if (onBooking != null)
        {
            onBooking();
        }
    }

}