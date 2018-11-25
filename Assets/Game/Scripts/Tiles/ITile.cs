using Blokoti.Game.Scripts.Actors.Players;
using UnityEngine;

namespace Blokoti.Game.Scripts.Tiles
{
    public interface ITile
    {
        Component Component { get; }
        void Destroy();
        void OnPlayerEnter(Player player);
        void OnPlayerExit(Player player);
    }
}
