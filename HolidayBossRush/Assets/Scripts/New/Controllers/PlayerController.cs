using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
public class PlayerController : InputController
{
    public override float Move()
    {
        return Input.GetAxisRaw("Move");
    }
        
    public override bool Jump()
    {
        return Input.GetButtonDown("Jump");
    }
}
