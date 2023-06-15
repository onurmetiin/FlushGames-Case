using UnityEngine;

namespace Assets.Dev.Scripts.Tiles
{
    [CreateAssetMenu(fileName = "DefaultGem", menuName = "Create New Gem", order = 0)]
    public class GemTypeSO : ScriptableObject
    {
        [field: SerializeField]
        public string Name { get; set; }

        [field: SerializeField]
        public float InitialPrice { get; set; }

        [field: SerializeField]
        public Sprite Icon { get; set; }

        [field: SerializeField]
        public GameObject Model { get; set; }
    }
}
