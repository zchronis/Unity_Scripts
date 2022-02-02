using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Mover mover;
    private CameraMk1 cam;
    private AtkManager atk;
    private LevelChanger fade;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var movers = FindObjectsOfType<Mover>();
        var cams = FindObjectsOfType<CameraMk1>();
        var atks = FindObjectsOfType<AtkManager>();
        var fades = FindObjectsOfType<LevelChanger>();
        var index = playerInput.playerIndex;
        mover = movers.FirstOrDefault(m => m.GetPlayerIndex() == index);
        cam = cams.FirstOrDefault(m => m.GetPlayerIndex() == index);
        atk = atks.FirstOrDefault(m => m.GetPlayerIndex() == index);
        fade = fades.FirstOrDefault(m => m.GetPlayerIndex() == index);
    }

    public void OnMove(CallbackContext context)
    {
        if (mover != null)
            mover.SetInputVector(context.ReadValue<Vector2>());
    }

    public void OnLook(CallbackContext context)
    {
        if (cam != null) 
        {
            cam.SetInputVector(context.ReadValue<Vector2>());
            
            if(context.performed)
                cam.SetZoomLevel(context.ReadValue<Vector2>());
        }
    }

    public void OnAttack(CallbackContext context)
    {
        if(context.performed)
            atk.BtnAttack();
    }
    
    public void OnInventory(CallbackContext context)
    {
        if(context.performed)
        {
            fade.FadeToLevel(0);
        }
    }

}

