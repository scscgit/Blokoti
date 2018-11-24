using UnityEngine;

namespace Blokoti.Game.Scripts
{
    public class DynamicTile : MonoBehaviour
    {
        public int type;

        private bool _isSelected;

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

        private void Start()
        {
            ApplyType(type);
        }

        private void Update()
        {
            if (_isSelected && Input.GetMouseButtonDown(0))
            {
                ApplyType(type + 1);
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
                    break;
                case 1:
                    // todo
                    transform.Find("Green").gameObject.SetActive(false);
                    transform.Find("White").gameObject.SetActive(true);
                    break;
                case 2:
                    // todo
                    break;
            }
        }
    }
}
