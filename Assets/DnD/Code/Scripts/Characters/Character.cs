using UnityEngine;

namespace DnD.Code.Scripts.Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField]
        private CharacterStats characterStats;

        public CharacterStats CharacterStats
        {
            get => characterStats;
            set => characterStats = value;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Debug.Log("Character start");
        }
        
        void Awake()
        {
            Debug.Log("Character awoken");
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}