using Infrastructure.SaveManager;
using Infrastructure.SaveManager.Models;
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
            // Find the player object, either by Tag, or by Component (PlayerAgent/CompanionAgent)
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject is null)
            {
                DeeDeeR.MessageBroker.MessageBroker.Instance.Logger.Send_OnLog(this, nameof(Logger), $"PlayerCharacter GameObject not found.", LogType.Error);
                return;
            }
            
            var characterIdentity = playerObject.GetComponent<Management.Character.CharacterIdentity>();
            
            if (characterIdentity is null)
            {
                DeeDeeR.MessageBroker.MessageBroker.Instance.Logger.Send_OnLog(this, nameof(Logger), $"Player identity not found.", LogType.Error);
                return;
            }
            
            var saveGameDataToEntityConverter = new SaveGameDataToEntityConverter();
            var characterStats = saveGameDataToEntityConverter.Convert(saveGameData.CharacterStats);

            if (characterStats is null)
            {
                DeeDeeR.MessageBroker.MessageBroker.Instance.Logger.Send_OnLog(this, nameof(Logger), $"Unable to extract characterStats from saveGameData.", LogType.Error);
                return;
            }
            characterIdentity.CharacterStats = characterStats;
        }
    }
}