using Blokoti.Game.Scripts.Actors.Players;
using UnityEngine;
using UnityEngine.Events;

namespace Blokoti.Game.Scripts.Tiles
{
    [SelectionBase]
    public class SwitchTile : AbstractTile
    {
        public UnityEvent enter;
        public UnityEvent exit;

        public override void OnPlayerEnter(Player player)
        {
            enter.Invoke();
        }

        public override void OnPlayerExit(Player player)
        {
            exit.Invoke();
        }
    }
}