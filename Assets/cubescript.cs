using UnityEngine;

public class cubescript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(1, 2, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
