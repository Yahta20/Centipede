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
    private int ARot;
    private int xlbord = -15;
    private int xhbord = 21;
    private int zlbord = -30;
    private int zhbord = -103;

    private bool sendScore;
    private bool isturn;
    private bool LeftMove;
    private bool DownMove;
    private bool MshOnWay;

    private double stpdz = -30.0f;
    private float dx = 0.0f;
    private float dz = 0.0f;


    void Start()
    {
        Gm = GameObject.Find("GameManager").GetComponent(typeof(Manager)) as Manager;
        sendScore = false;
        healt = 1;
        ceRb = GetComponent<Rigidbody>();
        speed = 3;
        isturn = false;
        LeftMove = false;
        DownMove = true;
        MshOnWay = false;

        Cbody = 0;
        turn = 0;

        transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        Vector3 pos = transform.position;
        dx = pos.x;
        dz = pos.z;
        ARot = 90;
        xlbord = -15;
        xhbord = 21;
        zlbord = -30;
        zhbord = -103;
        stpdz = -30.0f;
    }

    void FixedUpdate()
    {

        //poziciya v prostranstve, postanova
        ceRb.velocity = transform.forward * speed;
        Vector3 pos = transform.position;
        //transform.rotation = Quaternion.Euler(new Vector3(0, ARot, 0));

        // Proigrish
        if (pos.z < zhbord)
        {
            Gm.Gameover();
            print("vse");
        }
        // smerti
        if (healt == 0)
        {
            SendScore();
            Destroy(gameObject, 0.2f);
        }
        //povorot 
        if (pos.z <= -30 & !isturn & DownMove)
        {
            isturn = true;
            Turn(LeftMove);
            stpdz -= 3;
        }
        
        //opredelenie napravlenia
        if (15.5<pos.x & pos.x < 20.5) {
                if (pos.x < dx & DownMove == false )
            {
                LeftMove = false;
            }

            if (pos.x > dx & DownMove == false)
            {
                LeftMove = true;
            }
        }
        

        //peremeshenie gusinitci
        if (!DownMove)
        {
            if (pos.x <= -15 & !LeftMove)
            {

                Turn(false);
                //print("<+P");
            }
            if (pos.x >= 21 & LeftMove)
            {

                Turn(true);
                //print("P+>");

            }

        }

        else {
            if (pos.x <= -15 & pos.z <= stpdz & !LeftMove)
            {

                Turn(false);
                LeftMove = true;
                stpdz -= 3;

            }
            if (pos.x >= 21 & pos.z <= stpdz & LeftMove)
            {

                LeftMove = false;
                Turn(true);
                stpdz -= 3;

            }
        }

        if (MshOnWay) {
            //detector griba
            if (pos.y<=2.55) { }
        }
        MushrumDet();
        dz = pos.z;
        dx = pos.x;
        //print(stpdz);
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

    public void Turn(bool a) {


        DownMove = !DownMove;
        //DownMove = !DownMove;
        if (a) {
            Quaternion Yrot = Quaternion.AngleAxis(90, Vector3.up);
            transform.rotation *= Yrot;
            //print("povorot+");
        }
        else
        {
            Quaternion Yrot = Quaternion.AngleAxis(-90, Vector3.up);
            transform.rotation *= Yrot;
            //print("povorot-");
        }
    }

    public void MushrumDet(){

        //Vector3 endLine = transform.position * 3;

        Vector3 pew = transform.position;
        
        if (!DownMove)
        {
            if (LeftMove)
            {
                pew.x += 3;
            }
            else
            {
                pew.x -= 3;
            }
        }

        else {
            pew.z -= 3;
        }

        if (pew.y>=1) {
            //kogda na zemle
            Ray r = new Ray(transform.position, transform.forward);
            RaycastHit hit = new RaycastHit();
            //Debug.DrawLine(transform.position, pew, Color.red);
            Debug.DrawLine(transform.position, pew, Color.red);
            //print("R: " +  r + " ; "+ " t: " + transform.position+ "/n p: "+pew);


            if (Physics.Raycast(r,out hit, 3 )) {
                if (hit.collider.gameObject.name == "mushrum2(Clone)")
                {
                    MshOnWay = true;
                }
            }
        }
        
        
    } 

}
















