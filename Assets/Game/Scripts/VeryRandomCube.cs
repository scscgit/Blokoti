using UnityEngine;

public class VeryRandomCube : MonoBehaviour
{
    public float speed = 2;

    private Transform _transform;

    // Use this for initialization
    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    private void Update()
    {
        _transform.Translate(
            x: Input.GetAxis("Horizontal") * Time.deltaTime * speed,
            y: 0,
            z: Input.GetAxis("Vertical") * Time.deltaTime * speed
        );
    }
}