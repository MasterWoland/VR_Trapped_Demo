using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

namespace Pixelplacement
{
    public class Spinner : MonoBehaviour
    {
        //Private Variables:
        [SerializeField] Vector3 amount = new Vector3 (0, 10, 0);
        [SerializeField] float speed = 1;

        //Init:
        void Awake ()
        {
            Tween.Rotate (transform, amount, Space.Self, speed, 0, Tween.EaseLinear, Tween.LoopType.Loop);
        }
    }
}