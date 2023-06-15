using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Dev.Scripts.Tiles
{
    [CreateAssetMenu(fileName = "DefaultGem", menuName = "Create New Gem", order =0)]
    public class GemScriptable : ScriptableObject
    {
        [field : SerializeField]
        public string GemName { get; set; }
        
        
        [field: SerializeField]
        public float InitSellingPrice { get; set; }
        
        
        [field: SerializeField]
        public Sprite GemIcon { get; set; }
        
        
        [field: SerializeField]
        public GameObject GemPrefab { get; set; }
    }
}
