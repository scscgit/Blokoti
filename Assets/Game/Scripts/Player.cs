using UnityEngine;

namespace Blokoti.Game.Scripts
{
    public class Player : MonoBehaviour
    {
        public int Row
        {
            get { return Mathf.RoundToInt(transform.position.x + 4.5f); }
            set
            {
                var position = transform.position;
                transform.position = new Vector3(value - 4.5f, position.y, position.z);
            }
        }

        public int Col
        {
            get { return Mathf.RoundToInt(transform.position.z + 4.5f); }
            set
            {
                var position = transform.position;
                transform.position = new Vector3(position.x, position.y, value - 4.5f);
            }
        }

        public int oldRow;
        public int oldCol;
        public int targetRow;
        public int targetCol;

        public float stepSpeed = 0.04f;

        private bool _movement;
        private TileManager _tileManager;

        private void Start()
        {
            _tileManager = GameObject.Find("TileManager").GetComponent<TileManager>();
        }

        private void Update()
        {
            if (_movement)
            {
                InterpolateMovement();
                if (AlmostEquals(transform.position.x + 4.5f, targetRow)
                    && AlmostEquals(transform.position.z + 4.5f, targetCol))
                {
                    _movement = false;
                    Row = targetRow;
                    Col = targetCol;
                }
            }
            else
            {
                ProcessInputPrepareTarget();
            }
        }

        private bool AlmostEquals(float a, float b)
        {
            return Mathf.Abs(a - b) < stepSpeed * 2;
        }

        private void InterpolateMovement()
        {
            transform.Translate(
                oldRow > targetRow ? -stepSpeed : oldRow < targetRow ? stepSpeed : 0,
                0,
                oldCol > targetCol ? -stepSpeed : oldCol < targetCol ? stepSpeed : 0
            );
        }

        private void ProcessInputPrepareTarget()
        {
            oldRow = Row;
            oldCol = Col;
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            if (horizontal < 0)
            {
                targetRow = Row - 1;
                targetCol = Col;
                _movement = true;
            }
            else if (horizontal > 0)
            {
                targetRow = Row + 1;
                targetCol = Col;
                _movement = true;
            }
            else if (vertical < 0)
            {
                targetRow = Row;
                targetCol = Col - 1;
                _movement = true;
            }
            else if (vertical > 0)
            {
                targetRow = Row;
                targetCol = Col + 1;
                _movement = true;
            }

            if (_movement)
            {
                Debug.Log("Moving to " + targetRow + ":" + targetCol);
                // Handle wrong target and cancel the movement
                if (!_tileManager.IsAvailable(targetRow, targetCol))
                {
                    Debug.Log("Player's movement target unavailable");
                    _movement = false;
                }
            }
        }
    }
}
