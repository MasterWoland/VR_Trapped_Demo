using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace nl.allon.utils
{
    public class MaterialBlockTest : MonoBehaviour
    {
        public Color MaterialColor;
        public MaterialPropertyBlock _propertyBlock;
        public Renderer _renderer;
        private int _mainColorID;
        
        private void Awake()
        {
        }

        private void Start()
        {
            _propertyBlock = new MaterialPropertyBlock();
            _renderer = GetComponent<Renderer>();

            _mainColorID = Shader.PropertyToID("_MainColor");
            Debug.Log("Renderer: "+_renderer.name);
            Debug.Log("color ID: "+_mainColorID);
            
        }

        // OnValidate is called in the editor after the component is edited
        void OnValidate()
        {
            //create propertyblock only if none exists
            if (_propertyBlock == null)
                _propertyBlock = new MaterialPropertyBlock();
            
            //Get a renderer component either of the own gameobject or of a child
            Renderer renderer = GetComponent<Renderer>();
            //set the color property
            _renderer.GetPropertyBlock(_propertyBlock);// always get the property before changing it with MaterialPropertyBlocks
            _propertyBlock.SetColor(_mainColorID, MaterialColor);
            //apply propertyBlock to renderer
            renderer.SetPropertyBlock(_propertyBlock);
        }

        private void Update()
        {
            _renderer.GetPropertyBlock(_propertyBlock);// always get the property before changing it with MaterialPropertyBlocks
            // _propertyBlock.GetColor(_mainColorID); 
            _propertyBlock.SetColor(_mainColorID, Random.ColorHSV());
            _renderer.SetPropertyBlock(_propertyBlock);
            
            Color c = _propertyBlock.GetColor(_mainColorID);
            Color c2 = _propertyBlock.GetColor(_mainColorID);
            Debug.Log("Color: "+c2);
            
            // _renderer.material.SetColor("_MainColor", Color.red);
            // _renderer.material.SetColor("_BaseColor", Color.red);
        }
    }
}