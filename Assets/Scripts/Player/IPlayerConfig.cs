using System.Collections.Generic;

namespace Builder.Player
{
    public interface IPlayerConfig
    {
        float MoveVelocity { get; }
        float RotateSensitivity { get; }
        IReadOnlyCollection<string> ItemTags { get; }
    }
}