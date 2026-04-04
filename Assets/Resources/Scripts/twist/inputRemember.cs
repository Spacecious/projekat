using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputRecorder : MonoBehaviour
{
    [Header("Keybinding")]
    public Key replayKey = Key.P;

    private List<Vector3> _positions = new List<Vector3>();
    private bool _replaying;
    private int _replayIndex;
    private PlayerMovement _movement;

    void Start()
    {
        _movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Keyboard.current[replayKey].wasPressedThisFrame)
        {
            if (_replaying) StopReplay();
            else StartReplay();
        }

        if (_replaying) DoReplay();
        else _positions.Add(transform.position); 
    }

    void StartReplay()
    {
        if (_positions.Count == 0) { Debug.LogWarning("[InputRecorder] Nothing recorded yet."); return; }

        _replayIndex = 0;
        _replaying = true;
        _movement.enabled = false;
        Debug.Log("[InputRecorder] Replaying " + _positions.Count + " frames.");
    }

    void StopReplay()
    {
        _replaying = false;
        _movement.enabled = true;
        Debug.Log("[InputRecorder] Replay stopped.");
    }

    void DoReplay()
    {
        if (_replayIndex >= _positions.Count)
        {
            StopReplay();
            Debug.Log("[InputRecorder] Replay finished.");
            return;
        }

        transform.position = _positions[_replayIndex];
        _replayIndex++;
    }

    
}

