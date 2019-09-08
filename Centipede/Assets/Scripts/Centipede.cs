using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centipede : MonoBehaviour
{
    [SerializeField] public GameObject body;
    private Rigidbody ceRb;
    private Manager Gm; 

    private int speed;
    private int turn;
    private int healt;
    private int Cbody;

    private bool sendScore;
    private bool isturn;
    private bool first;

    private float start = 0.0f;



    // Start is called before the first frame update
    void Start()
    {
        Gm = GameObject.Find("GameManager").GetComponent(typeof(Manager)) as Manager;
        sendScore = false;
        healt = 1;
        ceRb = GetComponent<Rigidbody>();
        speed = 3;
        turn = 0;
        isturn=false;
        first = false;
        Cbody=0;
        while (turn==0) {
            turn = UnityEngine.Random.Range(-1, 1);
        }
        transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0)); 

    }
    
    // Update is called once per frame
    void Update()
    {
        
        ceRb.velocity = transform.forward * speed;
        Vector3 pos = transform.position;
        transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));




        if (pos.z<-103) {
            Gm.Gameover();
            print("vse");
        }
        if (healt == 0)
        {
            SendScore();
            Destroy(gameObject, 0.2f);
        }
    }


    private void OnCollisionEnter(Collision coll)
    {
        GameObject colobj = coll.gameObject;
        if (colobj.name == "shot(Clone)")
        {
            Damage();
        }
        if (colobj.name == "mushrum2(Clone)")
        {
            Add();
        }
    }

    public void Damage()
    {
        healt--;
    }

    public void Add() {
        healt++;
        
    }

    public void SendScore()
    {
        if (!sendScore)
        {
            Gm.Scoreadd(50);
            sendScore = true;
        }
    }
}
