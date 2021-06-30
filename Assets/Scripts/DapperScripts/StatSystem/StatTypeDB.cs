using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

        public static StatType GetType(string typeName) => types?[typeName];
    }
}