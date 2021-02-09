#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class SkullTower_Arrow : MonoBehaviour
{
    //Private Variables:
    [SerializeField] SpriteRenderer _spriteRenderer;

    //Public Methods:
    public void Move (Vector3 position)
    {
        Tween.Position (transform, position, .25f, 0, Tween.EaseOutBack);
        Tween.Stop (_spriteRenderer.GetInstanceID (), Tween.TweenType.SpriteRendererColor); //makes sure we can interrupt any previous color tweens on this spriteRenderer
        Tween.Color (_spriteRenderer, Color.white, Color.black, .25f, .1f); //slight delay here will keep the arrow bright white for a beat
    }
}