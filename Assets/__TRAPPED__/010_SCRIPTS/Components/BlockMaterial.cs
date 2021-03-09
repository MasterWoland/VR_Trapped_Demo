using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace nl.allon.components
{
    public class BlockMaterial : MonoBehaviour
    {
        public Vector2 Offset = default;
        public Color MaterialColor = default;
        public bool UseDither = false;
        private MaterialPropertyBlock _propertyBlock = default;
        private Renderer _renderer = default;
        private int _mainColorID;
        private int _textureOffsetID;
        private int _useDitherID;
        private int _ditherAmountID;
        private float _duration = 0.5f;
        private float _timer = 0f;
        private bool _doDitherEffect = false;
        
        private void Awake()
        {
            PrepareMaterialBlock();
            OnValidate();
        }

        private void SetProperties()
        {
            // always get the property before changing it with MaterialPropertyBlocks
            _renderer.GetPropertyBlock(_propertyBlock);

            _propertyBlock.SetColor(_mainColorID, Random.ColorHSV());
            _renderer.SetPropertyBlock(_propertyBlock);
        }

        private void SetBlockProperties()
        {
            _renderer.GetPropertyBlock(_propertyBlock); // always get the property before changing it with MaterialPropertyBlocks
            // _propertyBlock.SetColor("_mainColor", MaterialColor);
            _propertyBlock.SetVector(_textureOffsetID, Offset);
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
            _doDitherEffect = true;
            // _propertyBlock.SetFloat(di, 1);
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
        
        #region TEMP
        private void Update()
        {
            if (_doDitherEffect)
            {
                Debug.Log("___ dither effect ___ "+this.name);
                
                _timer += Time.deltaTime;
                float value = Mathf.Lerp(0, 0.8f, _timer / _duration);
                
                _renderer.GetPropertyBlock(_propertyBlock); // always get the property before changing it with MaterialPropertyBlocks
                // _propertyBlock.SetInt(_useDitherID, 1);
                _propertyBlock.SetFloat(_ditherAmountID, value);
                _renderer.SetPropertyBlock(_propertyBlock);

                if (_timer >= _duration)
                {
                    _renderer.GetPropertyBlock(_propertyBlock); // always get the property before changing it with MaterialPropertyBlocks
                    _propertyBlock.SetInt(_useDitherID, 0);
                    _propertyBlock.SetFloat(_ditherAmountID, 0);
                    _renderer.SetPropertyBlock(_propertyBlock);

                    _doDitherEffect = false;
                    _timer = 0;
                }
            }
            // if (Time.timeSinceLevelLoad > 4f)
            // {
            //     Debug.Log("__hello");
            //     _renderer.GetPropertyBlock(_propertyBlock); // always get the property before changing it with MaterialPropertyBlocks
            //     // _propertyBlock.SetColor("_mainColor", MaterialColor);
            //     _propertyBlock.SetInt(_useDitherID, 1);
            //     _propertyBlock.SetFloat("_DitherAmount", 0.3f);
            //     _propertyBlock.SetVector(_textureOffsetID, Vector2.zero);
            //     // _propertyBlock.SetInt(_useDitherID, 1);
            //
            //     //apply propertyBlock to renderer
            //     _renderer.SetPropertyBlock(_propertyBlock);
            // }
        }
        #endregion
    }
}