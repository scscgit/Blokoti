using System.Collections.Generic;
using System.Linq;
using Blokoti.Game.Scripts.Actors;
using UnityEngine;

namespace Blokoti.Game.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        private readonly IList<IActor> _actors = new List<IActor>();

        /// <summary>
        /// True if at least one actor is still acting.
        /// </summary>
        public bool Acting
        {
            get { return _actors.Any(actor => actor.Acting); }
        }

        public void RegisterActor(IActor actor)
        {
            _actors.Add(actor);
        }

        public void ActActors()
        {
            foreach (var actor in _actors)
            {
                actor.Act();
            }
        }
    }
}