using Blokoti.Game.Scripts.Managers;
using UnityEngine;

namespace Blokoti.Game.Scripts.Actors
{
    public class Player : MonoBehaviour, IPositionSupport
    {
        public float stepSpeed = 0.04f;

        private TileManager _tileManager;
        private PositionSupport _positionSupport;

        public Transform Transform
        {
            get { return _positionSupport.Transform; }
        }

        public float StepSpeed
        {
            get { return _positionSupport.StepSpeed; }
        }

        public int Row
        {
            get { return _positionSupport.Row; }
            set { _positionSupport.Row = value; }
        }

        public int Col
        {
            get { return _positionSupport.Col; }
            set { _positionSupport.Col = value; }
        }


        public bool Moving
        {
            get { return _positionSupport.Moving; }
        }

        public int TargetRow
        {
            get
            {
                return _positionSupport.TargetRow;
                ;
            }
        }

        public int TargetCol
        {
            get { return _positionSupport.TargetCol; }
        }

        public void SetGoal(int row, int col)
        {
            _positionSupport.SetGoal(row, col);
        }

        public bool StepTowardsGoal()
        {
            return _positionSupport.StepTowardsGoal();
        }

        private void Start()
        {
            _tileManager = GameObject.Find("TileManager").GetComponent<TileManager>();
            _positionSupport = new PositionSupport(() => transform, () => stepSpeed);
        }

        private void Update()
        {
            StepTowardsGoal();
            ProcessInputPrepareTarget();
        }

        private void ProcessInputPrepareTarget()
        {
            // Cannot start a next movement if still moving
            if (Moving)
            {
                return;
            }

            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            var targetRow = Row;
            var targetCol = Col;
            var move = false;
            if (horizontal < 0)
            {
                targetRow--;
                move = true;
            }
            else if (horizontal > 0)
            {
                targetRow++;
                move = true;
            }
            else if (vertical < 0)
            {
                targetCol--;
                move = true;
            }
            else if (vertical > 0)
            {
                targetCol++;
                move = true;
            }

            if (move)
            {
                // Handle wrong target and cancel the movement
                if (_tileManager.GetTile(TargetRow, TargetCol) == null)
                {
                    Debug.Log("Player's movement target to " + TargetRow + ":" + TargetCol + " unavailable");
                    return;
                }

                // Start the movement
                _positionSupport.SetGoal(targetRow, targetCol);
            }
        }
    }
}
