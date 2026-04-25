using System.Collections.Generic;
using System.Linq;

public class ImprovementsModel
{
    private readonly ImprovementsView _view;
    private readonly ImprovementsDatabase _database;
    
    public ImprovementsModel(ImprovementsView view, ISaveSystem saveSystem)
    {
        _view = view;
        _database = saveSystem.Load(SavingConstants.BoughtImprovementsId, 
            new ImprovementsDatabase { ImprovementsData = new List<ImprovementSaveData>() });
    }
    
    public ImprovementsDatabase Database => _database;
    
    public ImprovementSaveData GetImprovementData(string name)
    {
        return _database.ImprovementsData.FirstOrDefault(x => x.Name == name);
    }
    
    public void AddLevelToImprovement(string name)
    {
        ImprovementSaveData data = _database.ImprovementsData.FirstOrDefault(x => x.Name == name) 
                                   ?? new ImprovementSaveData(name);
        data.AddLevel();
        _view.DisplayNewImprovementLevel(name, data.Level);
    }
}