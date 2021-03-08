using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nl.allon.utils
{
    public class MaterialBlockTest : MonoBehaviour
    {
        public Color MaterialColor;
        public MaterialPropertyBlock _propertyBlock;
        public Renderer _renderer;
        
        private void Awake()
        {
        }

        private void Start()
        {
            _propertyBlock = new MaterialPropertyBlock();
            _renderer = GetComponent<Renderer>();
            _renderer.SetPropertyBlock(_propertyBlock);
            Debug.Log("Renderer: "+_renderer.name);
            
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
            _propertyBlock.SetColor("_MainColor", MaterialColor);
            //apply propertyBlock to renderer
            renderer.SetPropertyBlock(_propertyBlock);
        }

        private void Update()
        {
            _propertyBlock.SetColor("_BaseColor", Color.red);
            _propertyBlock.SetColor("_MainColor", Color.red);
            _renderer.SetPropertyBlock(_propertyBlock);
            
            Color c = _propertyBlock.GetColor("_MainColor");
            Color c2 = _propertyBlock.GetColor("_BaseColor");
            Debug.Log("Color: "+c2);
            
            // _renderer.material.SetColor("_MainColor", Color.red);
            // _renderer.material.SetColor("_BaseColor", Color.red);
        }
    }
}