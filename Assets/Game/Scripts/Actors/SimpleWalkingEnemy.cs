using System.Collections.Generic;

namespace Blokoti.Game.Scripts.Actors
{
    public class SimpleWalkingEnemy : AbstractActor
    {
        public List<GridPosition> walkingTemplate;
        public int walkingTemplateIndex;

        public override void Act()
        {
            Acting = true;
            var walkingGoal = walkingTemplate[walkingTemplateIndex++];
            if (walkingTemplateIndex > walkingTemplate.Count)
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
