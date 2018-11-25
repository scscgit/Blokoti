using UnityEngine;

namespace Blokoti.Game.Scripts
{
    public interface IPositionSupport
    {
        Transform Transform { get; }
        float StepSpeed { get; }

        int Row { get; set; }
        int Col { get; set; }
        bool Moving { get; }
        int TargetRow { get; }
        int TargetCol { get; }

        void SetGoal(int row, int col);
        bool StepTowardsGoal();

        void Start();
    }
}