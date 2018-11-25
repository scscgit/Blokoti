using System;
using Blokoti.Game.Scripts.Managers;
using UnityEngine;

namespace Blokoti.Game.Scripts
{
    public class PositionSupport : IPositionSupport
    {
        public const float GridOffsetX = 4.5f;
        public const float GridOffsetZ = 4.5f;

        private int _oldRow;
        private int _oldCol;
        private int _intermediateRow;
        private int _intermediateCol;

        private readonly Func<Transform> _getTransform;
        private readonly Func<float> _getStepSpeed;
        private readonly Func<TileManager> _getTileManager;
        private readonly Component _thisComponent;

        public PositionSupport(Func<Transform> getTransform, Func<float> getStepSpeed, Func<TileManager> getTileManager,
            Component thisComponent)
        {
            _getTransform = getTransform;
            _getStepSpeed = getStepSpeed;
            _getTileManager = getTileManager;
            _thisComponent = thisComponent;
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

        public bool Moving { get; set; }

        public int TargetRow { get; private set; }

        public int TargetCol { get; private set; }

        public Func<bool> OnGoalFinish { get; set; }

        public void SetGoal(int row, int col)
        {
            if (Moving)
            {
                throw new Exception("Goal has been already set!");
            }

            _oldRow = Row;
            _oldCol = Col;
            _intermediateRow = Row;
            _intermediateCol = Col;
            TargetRow = row;
            TargetCol = col;
            Moving = true;
            Debug.Log(Transform.gameObject.name + " moving to target " + TargetRow + ":" + TargetCol);
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
            // In the case of a long jump we can still notify TileManager about Row/Col change
            if (_intermediateRow != Row || _intermediateCol != Col)
            {
                Debug.Log(_thisComponent.gameObject.name
                          + " has moved from " + _intermediateRow + ":" + _intermediateCol
                          + " to " + Row + ":" + Col);
                _getTileManager().UnregisterActor(_intermediateRow, _intermediateCol, _thisComponent);
                _getTileManager().RegisterActor(Row, Col, _thisComponent);
                _intermediateRow = Row;
                _intermediateCol = Col;
            }

            if (_oldRow != TargetRow && !AlmostEquals(Transform.position.x + GridOffsetX, TargetRow) ||
                _oldCol != TargetCol && !AlmostEquals(Transform.position.z + GridOffsetZ, TargetCol))
            {
                // Walk is still not finished
                return false;
            }

            // Finished walking towards the goal
            Row = TargetRow;
            Col = TargetCol;
            Moving = false;

            if (OnGoalFinish != null)
            {
                OnGoalFinish();
                OnGoalFinish = null;
            }

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

        public void Start()
        {
            _getTileManager().RegisterActor(Row, Col, _thisComponent);
        }
    }
}
