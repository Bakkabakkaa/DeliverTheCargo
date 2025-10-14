using System;
using System.Collections;
using UnityEngine;

public class BoxDeliver : MonoBehaviour
{
    [SerializeField] private DrawLine _drawLine;
    [SerializeField] private Transform _rope;

    private float _speed = 2f;
    private Vector3 _targetPosition;

    private void OnEnable()
    {
        _drawLine.OnButtonUp += DeliverBox;
    }

    private void OnDisable()
    {
        _drawLine.OnButtonUp -= DeliverBox;
    }

    private void DeliverBox()
    {
        StartCoroutine(MoveRope());
    }

    private IEnumerator MoveRope()
    {
        for (int i = 0; i < _drawLine.LineRenderer.positionCount; i++)
        {
            var targetPosition = _drawLine.LineRenderer.transform.TransformPoint(_drawLine.LineRenderer.GetPosition(i));

            while (Vector3.Distance(_rope.position, targetPosition) > 0.01f)
            {
                _rope.position = Vector3.MoveTowards(_rope.position, targetPosition, _speed * Time.deltaTime);
                yield return null;
            }
            _rope.position = targetPosition;
        }
    }
}