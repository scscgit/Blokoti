using System.Collections.Generic;
using Blokoti.Game.Scripts.Managers;
using UnityEngine;

namespace Blokoti.Game.Scripts.Actors
{
    public class SimpleWalkingEnemy : AbstractActor
    {
        public List<GridPosition> walkingTemplate;
        public int walkingTemplateIndex;

        private GameManager _gameManager;

        public new void Awake()
        {
            base.Awake();
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        public override void Start()
        {
            base.Start();
            _gameManager.RegisterActor(this);
        }

        public override void Act()
        {
            Acting = true;
            var walkingGoal = walkingTemplate[walkingTemplateIndex++];
            if (walkingTemplateIndex >= walkingTemplate.Count)
            {
                walkingTemplateIndex = 0;
            }

            SetGoal(walkingGoal.Row, walkingGoal.Column);
        }

        private void Update()
        {
            if (StepTowardsGoal())
            {
                Acting = false;
            }
        }
    }
}
