using System.Linq;
using DnD.Code.Scripts.Characters;
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
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject is null)
            {
                DeeDeeR.MessageBroker.MessageBroker.Instance.Logger.Send_OnLog(this, nameof(Logger), $"PlayerCharacter GameObject not found.", LogType.Error);
                return;
            }
            
            var character = playerObject.GetComponent<Character>();
            
            if (character is null)
            {
                DeeDeeR.MessageBroker.MessageBroker.Instance.Logger.Send_OnLog(this, nameof(Logger), $"Player not found.", LogType.Error);
                return;
            }
            
            var saveGameDataToEntityConverter = new SaveGameDataToEntityConverter();
            var characterStats = saveGameDataToEntityConverter.Convert(saveGameData.CharacterStats);

            if (characterStats is null)
            {
                DeeDeeR.MessageBroker.MessageBroker.Instance.Logger.Send_OnLog(this, nameof(Logger), $"Unable to extract characterStats from saveGameData.", LogType.Error);
                return;
            }
            character.CharacterStats = characterStats;
        }
    }
}