#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class EnemyDamage_Effect : MonoBehaviour
{
    //Private Variables:
    [SerializeField] Color _injuryColor = Color.red;
    [SerializeField] float _injuryDuration  = .25f;
    [SerializeField] Transform _shakeTarget;
    [SerializeField] float _shakeIntensity = .025f;
    Color[] _initialColors;
    Renderer[] _renderers;
    Vector3 _shakeTargetInitialPosition;

    //Init:
    void Awake ()
    {
        //cache:
        _shakeTargetInitialPosition = _shakeTarget.localPosition;

        //initialize color cache array:
        _renderers = GetComponentsInChildren<Renderer> ();
        _initialColors = new Color[_renderers.Length];

        //cache initial colors:
        for (int i = 0; i < _renderers.Length; i++)
        {
            _initialColors [i] = _renderers [i].material.color;	
        }
    }

    //Event Handlers:
    void OnCollisionEnter (Collision collision)
    {
        Tween.Shake (_shakeTarget, _shakeTargetInitialPosition, Vector3.one * _shakeIntensity, _injuryDuration, 0);

        for (int i = 0; i < _renderers.Length; i++)
        {
            Tween.Color (_renderers[i], _injuryColor, _initialColors [i], _injuryDuration, 0);	
        }
    }
}