using System.Collections.Generic;
using System.Linq;

public class ImprovementsDatabase
{
    public List<ImprovementSaveData> ImprovementsData;

    public ImprovementSaveData GetData(string improvementName)
    {
        ImprovementSaveData data = ImprovementsData.FirstOrDefault(x => x.Name == improvementName);

        if (data == null)
        {
            data = new ImprovementSaveData(improvementName);
            ImprovementsData.Add(data);
        }

        return data;
    }
}