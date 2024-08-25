using System.Collections.Generic;
using Builder.Utility;

namespace Builder.UI
{
    public class UIModel : IUIModel
    {
        Pool<string, IUIScreen> IUIModel.ScreenPool { get; } = new();
        IDictionary<string, IUIScreen> IUIModel.CurrentOpenedScreens { get; set; } = new Dictionary<string, IUIScreen>();
    }
}