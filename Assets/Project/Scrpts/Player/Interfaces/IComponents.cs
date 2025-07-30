using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IComponents
{
    Rigidbody2D rb { get; set; }
    PlayerInputHandler inputHandler { get; set; }

}