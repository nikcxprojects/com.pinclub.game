using UnityEngine;

public class RotateMe : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(100 * Time.deltaTime * Vector3.back);
    }
}
