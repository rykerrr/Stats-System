using System;
using System.IO;
using UnityEditor;
using UnityEngine;

#pragma warning disable 0649
namespace StatSystem.TakeOne
{
    public abstract class StatType : ScriptableObject
    {
        public abstract string Name { get; protected set; }
        public abstract int Value { get; }

        protected virtual void OnValidate()
        {
            var assetPath = AssetDatabase.GetAssetPath(this.GetInstanceID());
            // Debug.Log(assetPath);
            Name = Path.GetFileNameWithoutExtension(assetPath);
        }
        
        // Note to self: projectChanged runs every time the file is renamed
        private void OnEnable()
        {
            EditorApplication.projectChanged += OnValidate;
        }

        private void OnDisable()
        {
            EditorApplication.projectChanged -= OnValidate;
        }

        public abstract override string ToString();
    }
}
