public class PagesPresenter
{
    private readonly PagesModel _model;
    private readonly PagesConfig _pagesConfig;
    
    public PagesPresenter(PagesModel model, IConfigProvider configProvider)
    {
        _model = model;
        _pagesConfig = configProvider.Get<PagesConfig>();
    }

    public void Initialize()
    {
        _model.Initialize(_pagesConfig.PageAtStartNumber);
    }
    
    public void SelectPage(int pageNumber)
    {
        _model.SetCurrentPage(pageNumber);
    }
}