using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public GameObject Shot;
    private Manager Gm;

    public int speed = 5;

    public float rFire = 0.3f;
    private float nFire = 0.0f;
    private bool fail;

    // Start is called before the first frame update
    void Start()
    {
        fail = false;
        Gm = GameObject.Find("GameManager").GetComponent(typeof(Manager)) as Manager;
    }
    public void Lose() {
        fail = true;
    }
    public bool isLose() {
        return fail;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Gm.isPaused())
        {
            if (!fail)
            {
                float dx = Input.GetAxis("Horizontal") * speed;
                float dz = Input.GetAxis("Vertical") * speed;
                Vector3 move = new Vector3(dx, 0, dz);
                move = Vector3.ClampMagnitude(move, speed);
                move *= Time.deltaTime;

                transform.Translate(move);

                Vector3 pos = transform.position;

                if (transform.position.x < -15)
                {
                    pos.x = 20.9f;
                    transform.position = pos;
                }
                if (transform.position.x > 21)
                {
                    pos.x = -14.9f;
                    transform.position = pos;
                }

                if (transform.position.z < -100)
                {
                    pos.z = -99.9f;
                    transform.position = pos;
                }
                if (transform.position.z > -29)
                {
                    pos.z = -29.1f;
                    transform.position = pos;
                }
                transform.rotation = Quaternion.identity;

                if (Input.GetKeyDown(KeyCode.Space) & Time.time > nFire)
                {
                    nFire = Time.time + rFire;
                    pos = transform.position;
                    pos.z += 1;
                    Instantiate(Shot, pos, Quaternion.identity);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        GameObject colobj = coll.gameObject;
        if (colobj.name == "head2(Clone)")
        {
            Death();
            Lose();
        }
    }
    public void Death()
    {
        Destroy(gameObject, 0.2f);
    }
}
