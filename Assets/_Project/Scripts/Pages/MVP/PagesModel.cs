using UnityEngine;

public class PagesModel
{
    private readonly PagesView _view;
    
    private int _currentPage = 1;

    public PagesModel(PagesView view)
    {
        _view = view;
    }
    
    public void Initialize(int pageNumber)
    {
        ChangePage(pageNumber);
        _view.DisplayPageSelectedInstant(_currentPage);
    }
    
    public void SetCurrentPage(int pageNumber)
    {
        ChangePage(pageNumber);
        _view.DisplayPageSelected(pageNumber);
    }

    private void ChangePage(int pageNumber)
    {
        if (pageNumber < 1)
        {
            Debug.LogError("Page number can't be less than 1");
            return;
        }
        
        _currentPage = pageNumber;
    }
}