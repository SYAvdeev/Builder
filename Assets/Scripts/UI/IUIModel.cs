using System.Collections.Generic;
using Builder.Utility;

namespace Builder.UI
{
    public interface IUIModel
    {
        internal Pool<string, IUIScreen> ScreenPool { get; }
        internal IDictionary<string, IUIScreen> CurrentOpenedScreens { get; set; }
    }
}