using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetRunner
{
    public class P_Background : MonoBehaviour
    {
        private float length, startpos;
        public GameObject cam;
        public float parallaxEffect;
        float smooth = 5.0f;
        float tiltAngle = 60.0f;
        private Transform player;

        void Start()
        {
            startpos = transform.position.x;
            length = GetComponent<SpriteRenderer>().bounds.size.x;

        }


        void FixedUpdate()
        {
            float dist = (cam.transform.position.x * parallaxEffect);

            float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
            float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;

            //transform.position = new Vector3(player.position.x, player.position.y, -10);
            //transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, cam.transform.rotation, startpos * dist);
        }

        private void FindPlayer()
        {

            if (player == null)
            {
                GameObject p = GameObject.FindGameObjectWithTag(Const.PLAYER);

                if (p != null)
                {
                    player = p.transform;
                }
            }
        }
    }
}
