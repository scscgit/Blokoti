using Blokoti.Game.Scripts.Managers;
using UnityEngine;

namespace Blokoti.Game.Scripts
{
    public class DynamicTile : MonoBehaviour, IPositionSupport
    {
        public int type;

        private bool _isSelected;
        private TileManager _tileManager;

        private readonly PositionSupport _positionSupport;

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
            get { return _positionSupport.TargetRow; }
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

        public DynamicTile()
        {
            _positionSupport = new PositionSupport(() => transform, () => 0, () => _tileManager, this);
        }

        private void OnMouseEnter()
        {
            //transform.Translate(0, 0.25f, 0);
            _isSelected = true;
        }

        private void OnMouseExit()
        {
            //transform.Translate(0, -0.25f, 0);
            _isSelected = false;
        }

        public void Start()
        {
            ApplyType(type);
            _tileManager = GameObject.Find("TileManager").GetComponent<TileManager>();
            _tileManager.RegisterTile(Row, Col, this);
        }

        private void Update()
        {
            if (_isSelected && Input.GetMouseButtonDown(0))
            {
                ApplyType(type + 1);
            }

            if (transform.Find("Grey").gameObject.activeInHierarchy.Equals(true))
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    transform.localScale += new Vector3(0, 0.5F, 0);
                    transform.position += new Vector3(0, 0.25F, 0);
                }

                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (transform.localScale.y > 0.5F)
                    {
                        transform.localScale -= new Vector3(0, 0.5F, 0);
                        transform.position -= new Vector3(0, 0.25F, 0);
                    }
                }
            }
        }

        private void ApplyType(int newType)
        {
            if (newType > 2)
            {
                newType = 0;
            }

            type = newType;
            switch (newType)
            {
                case 0:
                    transform.Find("Green").gameObject.SetActive(true);
                    transform.Find("White").gameObject.SetActive(false);
                    transform.Find("Grey").gameObject.SetActive(false);
                    break;
                case 1:
                    transform.Find("Green").gameObject.SetActive(false);
                    transform.Find("White").gameObject.SetActive(true);
                    transform.Find("Grey").gameObject.SetActive(false);
                    break;
                case 2:
                    transform.Find("Green").gameObject.SetActive(false);
                    transform.Find("White").gameObject.SetActive(false);
                    transform.Find("Grey").gameObject.SetActive(true);
                    break;
            }
        }
    }
}
