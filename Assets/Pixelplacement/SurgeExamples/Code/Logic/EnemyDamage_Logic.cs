#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class EnemyDamage_Logic : MonoBehaviour
{
    //Private Variables:
    [SerializeField] float _shootForce = 100;
    [SerializeField] Transform _bullet;
    Camera _camera;

    //Init:
    void Awake ()
    {
        _camera = Camera.main;
    }

    //Loops:
    void Update ()
    {
        if (Input.GetMouseButtonDown (0))
        {
            //get shoot direction velocity:
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 1;
            Ray ray = _camera.ScreenPointToRay (mousePosition);

            //create bullet (this is where an object pool should be used):
            Transform newBullet = Instantiate (_bullet) as Transform;
            newBullet.position = ray.origin + ray.direction;
            GameObject.Destroy (newBullet.gameObject, 10);

            //shoot bullet:
            newBullet.GetComponent<Rigidbody> ().AddForce (ray.direction * _shootForce);
        }
    }
}