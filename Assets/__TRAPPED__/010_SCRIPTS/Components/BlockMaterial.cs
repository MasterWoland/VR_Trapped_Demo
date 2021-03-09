using System;
using System.Collections;
using System.Collections.Generic;
using nl.allon.utils;
using Pixelplacement;
using Pixelplacement.TweenSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace nl.allon.components
{
    public class BlockMaterial : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _pulseCurve = default;
        // public Vector2 Offset = default;
        // public Color MaterialColor = default;
        // public bool UseDither = false;
        private MaterialPropertyBlock _propertyBlock = default;
        private Renderer _renderer = default;
        private float _duration = 0.5f;
        private TweenBase _pulseTween;
        
        // --- id's ---
        private int _mainColorID;
        private int _textureOffsetID;
        private int _useDitherID;
        private int _ditherAmountID;
        private int _emissionIntensityID;
        // private float _timer = 0f;
        // private bool _doDitherEffect = false;
        
        private void Awake()
        {
            PrepareMaterialBlock();
            OnValidate();
        }

        private void Start()
        {
            // MRA: obtain values from config!
            _pulseTween = Tween.Value(0.5f, 2.5f, Pulse, 2.5f, 0f, _pulseCurve, Tween.LoopType.PingPong);
            // MRA: perhaps Cancel tween on hit? And resume after?
            
            // _pulseTween.Start();
        }

        private void Pulse(float value)
        {
            _renderer.GetPropertyBlock(_propertyBlock); // always get the property before changing it with MaterialPropertyBlocks
            _propertyBlock.SetFloat(_emissionIntensityID, value);
            _renderer.SetPropertyBlock(_propertyBlock);
        }

        // private void SetProperties()
        // {
        //     // always get the property before changing it with MaterialPropertyBlocks
        //     _renderer.GetPropertyBlock(_propertyBlock);
        //
        //     _propertyBlock.SetColor(_mainColorID, Random.ColorHSV());
        //     _renderer.SetPropertyBlock(_propertyBlock);
        // }

        private void SetBlockProperties()
        {
            _renderer.GetPropertyBlock(_propertyBlock); // always get the property before changing it with MaterialPropertyBlocks
            _propertyBlock.SetVector(_textureOffsetID, new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)));
            _propertyBlock.SetInt(_useDitherID, 0);
            
            //apply propertyBlock to renderer
            _renderer.SetPropertyBlock(_propertyBlock);
        }

        #region PUBLIC
        public void ApplyHitEffect(float impact)
        {
            // Debug.Log("[BlockMaterial] Hit: "+impact);
            
            _renderer.GetPropertyBlock(_propertyBlock); // always get the property before changing it with MaterialPropertyBlocks
            _propertyBlock.SetInt(_useDitherID, 1);
            _propertyBlock.SetFloat(_ditherAmountID, 0);
            _renderer.SetPropertyBlock(_propertyBlock);
            // _doDitherEffect = true;
            // _propertyBlock.SetFloat(di, 1);
            float duration = _duration.Remap(0f, 0f, 15f, 1f);
            Tween.Value(0f, 0.8f, UpdateDither, 1f, 0f, null, Tween.LoopType.None, null, OnHitEffectComplete);
        }
        #endregion
        
        #region HIT EFFECT
        private void UpdateDither(float value)
        {
            _renderer.GetPropertyBlock(_propertyBlock); // always get the property before changing it with MaterialPropertyBlocks
            // _propertyBlock.SetInt(_useDitherID, 1);
            _propertyBlock.SetFloat(_ditherAmountID, value);
            _renderer.SetPropertyBlock(_propertyBlock);
        }

        private void OnHitEffectComplete()
        {
            Debug.Log("___ dither complete ____");
            _renderer.GetPropertyBlock(_propertyBlock); // always get the property before changing it with MaterialPropertyBlocks
            _propertyBlock.SetInt(_useDitherID, 0);
            // _propertyBlock.SetFloat(_ditherAmountID, value);
            _renderer.SetPropertyBlock(_propertyBlock);
        }
        #endregion
        
        #region HELPER METHODS
        private void PrepareMaterialBlock()
        {
            _propertyBlock = new MaterialPropertyBlock();
            _renderer = GetComponent<Renderer>();
            _mainColorID = Shader.PropertyToID("_MainColor");
            _textureOffsetID = Shader.PropertyToID("_TextureOffset");
            _useDitherID = Shader.PropertyToID("_UseDither");
            _ditherAmountID = Shader.PropertyToID("_DitherAmount");
            _emissionIntensityID = Shader.PropertyToID("_EmissionIntensity");
        }
        #endregion

        #region EDITOR
        // OnValidate is called in the editor after the component is edited
        void OnValidate()
        {
#if UNITY_EDITOR
            PrepareMaterialBlock(); // to obtain the id's when we are in the editor 
            if (_propertyBlock == null) _propertyBlock = new MaterialPropertyBlock();
            if (_renderer == null) _renderer = GetComponent<Renderer>();
#endif

            SetBlockProperties();
        }
        #endregion
    }
}