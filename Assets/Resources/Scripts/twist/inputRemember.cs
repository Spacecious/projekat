using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputRecorder : MonoBehaviour
{
    public float recordDuration = 3f;
    public Key replayKey = Key.P;

    private List<Vector3> _positions = new List<Vector3>();
    private bool _recording;
    private bool _replaying;
    private int _replayIndex;
    private PlayerMovement _movement;

    void Start()
    {
        _movement = GetComponent<PlayerMovement>();
        StartCoroutine(RecordThenReplay());
    }
    private void Update()
    {
        if (_recording)
        {
            _positions.Add(transform.position);
        }

        
        if (_replaying)
        {
            DoReplay();
        }
    }

    IEnumerator RecordThenReplay()
    {
        
        yield return null;

        _positions.Clear();
        _recording = true;
        

        yield return new WaitForSeconds(recordDuration);

        _recording = false;
        

        StartReplay();
    }

    void StartReplay()
    {
        if (_positions.Count == 0) { return; }

        _replayIndex = 0;
        _replaying = true;
        _movement.enabled = false;
        
    }

    void StopReplay()
    {
        _replaying = false;
        _movement.enabled = true;
        
    }

    void DoReplay()
    {
        if (_replayIndex >= _positions.Count)
        {
            StopReplay();
            return;
        }

        transform.position = _positions[_replayIndex];
        _replayIndex++;
    }

    
}
