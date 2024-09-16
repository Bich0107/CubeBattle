using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] Vector3 rotateSpeed;
    [SerializeField] bool localRotation;
    [SerializeField] bool rotateOnAwake = false;
    bool rotating = false;

    void Awake()
    {
        if (rotateOnAwake)
        {
            rotating = true;
        }
    }

    public Vector3 RotateSpeed
    {
        set { rotateSpeed = value; }
    }

    public void Rotate() => rotating = true;

    void Rotating()
    {
        Quaternion rotation = Quaternion.Euler(rotateSpeed * Time.fixedDeltaTime);

        if (localRotation)
        {
            transform.localRotation *= rotation;
        }
        else
        {
            transform.rotation = rotation * transform.rotation;
        }
    }

    void FixedUpdate()
    {
        if (rotating) Rotating();
    }

    public void Stop() => rotating = false;
}

