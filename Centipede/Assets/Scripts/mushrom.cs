using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mushrom : MonoBehaviour
{
    private Manager Gm;
    private Centipede cen;

    private int maxhealt = 3;
    private int healt;
    private Renderer renderer;
    private Color covCol;
    private bool sendScore;

    void Start()
    {
        sendScore= false;
        Gm = GameObject.Find("GameManager").GetComponent(typeof (Manager)) as Manager;
        renderer = GetComponent<Renderer>();
        healt = maxhealt;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (healt==0) {
            SendScore();
            Death();
        }
        float dam = healt / maxhealt;
        covCol.r = dam;
        covCol.g = dam;
        covCol.b = dam;
        renderer.material.color = covCol;

    }
    public void Damage() {
        healt--;
    }
    private void OnCollisionEnter(Collision coll)
    {       
        GameObject colobj = coll.gameObject;
        if (colobj.name == "shot(Clone)")
        {
            Damage();
        }
        if (colobj.name == "head2(Clone)")
        {
            cen = GameObject.Find("GameManager").GetComponent(typeof(Centipede)) as Centipede;
            Death();
        }
    }
    public void SendScore() {
        if (!sendScore) {
            Gm.Scoreadd(10);
            sendScore = true;
        }
    }

    public void Death() {
        Destroy(gameObject, 0.2f);
    }
}
