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
        }

        public override void OnPlayerExit(Player player)
        {
            if (_stepped)
            {
                var animator = Transform.Find("Broken").GetComponent<Animator>();
                animator.speed *= 5;
                animator.enabled = true;
                TileManager.UnregisterTile(Row, Col);
            }
        }

//        private IEnumerator DelayedDestroy()
//        {
//            yield return new WaitForSeconds(2);
//            Destroy();
//        }
    }
}
