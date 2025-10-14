using System;
using UnityEngine;
using UnityEngine.Serialization;

public class DrawLine : MonoBehaviour
{
    [SerializeField] private BoxController _boxController;
    [SerializeField] private Transform _rope;

    public LineRenderer LineRenderer { get; private set; }
    private Camera _camera;
    public event Action OnButtonUp;
    private float _depth;

    private void Awake()
    {
        LineRenderer = GetComponent<LineRenderer>();
        _camera = Camera.main;
        _depth = Math.Abs(_camera.transform.position.z - _rope.position.z);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _depth);
            var drawingPoint = _camera.ScreenToWorldPoint(mousePosition);

            if (Vector3.Distance(drawingPoint, LineRenderer.GetPosition(LineRenderer.positionCount - 1)) > 0.01f)
            {
                LineRenderer.positionCount++;
                LineRenderer.SetPosition(LineRenderer.positionCount - 1, drawingPoint);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnButtonUp?.Invoke();
        }
    }
}