using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyWalk : EnemyBase
    {
        [Header("Waypoint")]
        public List<Transform> waypoint;
        private int _index;
        private float _minDistance = .1f;

        protected override void Update()
        {
            if(Vector3.Distance(transform.position, waypoint[_index].position) < _minDistance)
            {
                _index++;

                if(_index >= waypoint.Count)
                {
                    _index = 0;
                }
            }

            transform.position = Vector3.MoveTowards(transform.position, waypoint[_index].position, Time.deltaTime * speed);

            LookDir();
            // transform.LookAt(waypoint[_index].position);
        }

        void LookDir()
        {
            var lookDir = waypoint[_index].position - new Vector3(transform.position.x, waypoint[_index].position.y, transform.position.z);
            transform.forward = Vector3.Slerp(transform.forward, lookDir.normalized, Time.deltaTime * turnSpeed);
        }
    }
}
