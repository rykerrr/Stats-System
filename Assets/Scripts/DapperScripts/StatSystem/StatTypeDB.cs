using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StatSystem.TakeOne
{
    public static class StatTypeDB
    {
        private static Dictionary<string, StatType> types = new Dictionary<string, StatType>();
        private static string locationInResources = "Game Data/Stat Types";
        
        static StatTypeDB()
        {
            var typesToLoad = Resources.LoadAll<StatType>(locationInResources);
            
            foreach (var type in typesToLoad)
            {
                types.Add(type.Name, type);
            }
        }

        public static StatType GetType(string typeName)
        {
            if (!types.ContainsKey(typeName))
            {
                #if UNITY_EDITOR
                    Debug.LogWarning("StatType wasn't loaded." +
                                     " Perhaps it doesn't exist in the folder?");
                #endif

                return null;
            }
            
            return types[typeName];
        }
    }
}