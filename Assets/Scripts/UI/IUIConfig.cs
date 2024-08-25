namespace Builder.UI
{
    public interface IUIConfig
    {
        TScreen GetUIPrefabByType<TScreen>() where TScreen : UIScreen;
    }
}