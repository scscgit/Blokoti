using System;
using UnityEngine;

namespace Blokoti.Game.Scripts
{
    public class PositionSupport : IPositionSupport
    {
        public const float GridOffsetX = 4.5f;
        public const float GridOffsetZ = 4.5f;

        private int _oldRow;
        private int _oldCol;

        private readonly Func<Transform> _getTransform;
        private readonly Func<float> _getStepSpeed;

        public PositionSupport(Func<Transform> getTransform, Func<float> getStepSpeed)
        {
            _getTransform = getTransform;
            _getStepSpeed = getStepSpeed;
        }

        public Transform Transform
        {
            get { return _getTransform(); }
        }

        public float StepSpeed
        {
            get { return _getStepSpeed(); }
        }

        public int Row
        {
            get { return Mathf.RoundToInt(Transform.position.x + GridOffsetX); }
            set
            {
                var position = Transform.position;
                Transform.position = new Vector3(value - GridOffsetX, position.y, position.z);
            }
        }

        public int Col
        {
            get { return Mathf.RoundToInt(_getTransform().position.z + GridOffsetZ); }
            set
            {
                var position = Transform.position;
                Transform.position = new Vector3(position.x, position.y, value - GridOffsetZ);
            }
        }

        public bool Moving { get; private set; }

        public int TargetRow { get; private set; }

        public int TargetCol { get; private set; }

        public void SetGoal(int row, int col)
        {
            if (Moving)
            {
                throw new Exception("Goal has been already set!");
            }

            _oldRow = Row;
            _oldCol = Col;
            TargetRow = row;
            TargetCol = col;
            Moving = true;

            Debug.Log(Transform.gameObject.name + " moving to " + TargetRow + ":" + TargetCol);
        }

        /// <summary>
        /// Moves one step towards the goal.
        /// </summary>
        /// <returns>true when already in the goal, false if the movement still isn't finished</returns>
        public bool StepTowardsGoal()
        {
            if (!Moving)
            {
                // Was already in goal before the step started
                return true;
            }

            InterpolateMovement();
            if (!AlmostEquals(Transform.position.x + GridOffsetX, TargetRow) ||
                !AlmostEquals(Transform.position.z + GridOffsetZ, TargetCol))
            {
                return false;
            }

            // Finished walking towards the goal
            Row = TargetRow;
            Col = TargetCol;
            Moving = false;
            return true;
        }

        private bool AlmostEquals(float a, float b)
        {
            return Mathf.Abs(a - b) < StepSpeed * 1.5f;
        }

        private void InterpolateMovement()
        {
            Transform.Translate(
                _oldRow > TargetRow ? -StepSpeed : _oldRow < TargetRow ? StepSpeed : 0,
                0,
                _oldCol > TargetCol ? -StepSpeed : _oldCol < TargetCol ? StepSpeed : 0
            );
        }
    }
}
