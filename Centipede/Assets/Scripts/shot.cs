using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 50;
        transform.rotation =  Quaternion.Euler(new Vector3(90, 0, 0)); ;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = transform.position;
        transform.Translate(0, speed * Time.deltaTime, 0);
        if (pos.z>-25) {
            Death();
        }
    }

    private void Death() {
        Destroy(gameObject, 0.01f);
    }

    private void OnCollisionEnter(Collision coll)
    {
        Death();
    }
}
