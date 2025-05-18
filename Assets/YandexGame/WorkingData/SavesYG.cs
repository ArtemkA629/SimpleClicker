
using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Ваши сохранения

        public int ClicksCount;
        public int Level = 1;
        public int CurrentNextLevelClicksCount = 10;
        public int PerClickStat = 1;
        public List<int> BoughtUpgradeNumbers = new List<int>();
        public List<int> BoughtStylesNumbers = new List<int>() { 1 };
        public int ChosenStyle = 1;
        public int Coins;

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны

        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG() {}

        public SavesYG(SaveData saveData)
        {
            ClicksCount = saveData.ClicksCount;
            Level = saveData.Level;
            CurrentNextLevelClicksCount = saveData.CurrentNextLevelClicksCount;
            PerClickStat = saveData.PerClickStat;
            BoughtUpgradeNumbers = saveData.BoughtUpgradeNumbers;
            BoughtStylesNumbers = saveData.BoughtStylesNumbers;
            ChosenStyle = saveData.ChosenStyle;
            Coins = saveData.Coins;
        }
    }
}
