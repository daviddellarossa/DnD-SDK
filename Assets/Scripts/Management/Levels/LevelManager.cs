using System.Linq;
using DnD.Code.Scripts.Characters;
using Infrastructure.SaveManager;
using Management.Scene;
using UnityEngine;

namespace Management.Levels
{
    public class LevelManager : SceneManager
    {
        private SaveGameData saveGameData;
        public void SetSaveGameData(SaveGameData data)
        {
            this.saveGameData = data;

        }
        void Start()
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject is null)
            {
                Debug.LogError("PlayerCharacter GameObject not found.");
                return;
            }
            
            var character = playerObject.GetComponent<Character>();
            
            if (character is null)
            {
                Debug.LogError("Player not found");
                return;
            }
            
            var saveGameDataToEntityConverter = new SaveGameDataToEntityConverter();
            var characterStats = saveGameDataToEntityConverter.Convert(saveGameData.CharacterStats);

            if (characterStats is null)
            {
                Debug.LogError("Unable to extract characterStats from saveGameData");
                return;
            }
            character.CharacterStats = characterStats;
        }
    }
}