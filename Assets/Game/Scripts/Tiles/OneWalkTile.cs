using Blokoti.Game.Scripts.Actors.Players;
using UnityEngine;

namespace Blokoti.Game.Scripts.Tiles
{
    [SelectionBase]
    public class OneWalkTile : AbstractTile
    {
        private bool _stepped;

        public override void OnPlayerEnter(Player player)
        {
            _stepped = true;
            var animator = GetComponent<Animator>();
            animator.enabled = true;
        }

        public override void OnPlayerExit(Player player)
        {
            if (_stepped)
            {
                Destroy();
            }
        }
    }
}
