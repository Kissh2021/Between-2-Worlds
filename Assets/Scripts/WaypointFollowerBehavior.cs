using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollowerBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _waypoints;
    private int _currentWaypointIndex = 0;

    [SerializeField]
    private float _speed = 2f;

    private bool _revert = false;

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(_waypoints[_currentWaypointIndex].transform.position, transform.position) <= .1f)
        {
            if (_currentWaypointIndex == _waypoints.Length - 1)
                _revert = true;
            else if (_currentWaypointIndex == 0)
                _revert = false;

            if (_revert)
                _currentWaypointIndex--;
            else
                _currentWaypointIndex++;
        }

        transform.position = Vector2.MoveTowards(transform.position, _waypoints[_currentWaypointIndex].transform.position, Time.deltaTime * _speed);
    }
}
