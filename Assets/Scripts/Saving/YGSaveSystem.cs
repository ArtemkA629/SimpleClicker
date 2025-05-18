using YG;

public class YGSaveSystem : ISaveSystem
{
    public void Save(SaveData saveData)
    {
        YandexGame.savesData = new SavesYG(saveData);
    }

    public SaveData Load()
    {
        return new SaveData(YandexGame.savesData);
    }
}
