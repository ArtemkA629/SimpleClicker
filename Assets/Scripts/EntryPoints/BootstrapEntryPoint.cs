using UnityEngine.SceneManagement;
using VContainer.Unity;

public class BootstrapEntryPoint : IStartable
{
    public void Start()
    {
        SceneManager.LoadScene(SceneNames.Game);
    }
}
