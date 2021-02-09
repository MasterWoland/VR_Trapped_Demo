using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

namespace nl.allon.models
{
    public class BlockModel
    {
        public float Width;
        public float Height;
        public float Depth;
        public Vector3 StartPosition;
        public int BlockNumber;

        public BlockModel(Vector3 dimensions, int number)
        {
            Width = dimensions.x;
            Height = dimensions.y;
            Depth = dimensions.z;
            BlockNumber = number;
        }
    }
}