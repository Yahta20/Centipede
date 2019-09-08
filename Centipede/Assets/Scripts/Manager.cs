using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{

    [SerializeField] public GameObject Player;
    [SerializeField] public GameObject Cent;
    [SerializeField] public GameObject Mushroom;
    [SerializeField] public Text scoreLabel;
    [SerializeField] public Text OverLabel;
    [SerializeField] public GameObject GameOn;
    [SerializeField] public GameObject GameEnd;

    private int intMushroom = 0;
    private int xl = -15;
    private int zl = -90;
    private int Score;

    private bool isOver;

    public float rEnemy = 3f;
    private float nEnemy = 0.0f;

    // Start is called before the first frame update
    void Awake()
    {
        GameOn.SetActive(true);
        GameEnd.SetActive(false);
        Score = 0;
        isOver = false;
        if (intMushroom == 0) {

            intMushroom = UnityEngine.Random.Range(35, 50);

        }

        MushGen();
        int mx1 = UnityEngine.Random.Range(1, 18);
        int x1 = (mx1 * 2) + xl;

        Instantiate(Cent, new Vector3(x1, 1.6f, -24), Quaternion.identity);

        Player = Instantiate(Player, new Vector3(3,1.6f,-100),Quaternion.identity) as GameObject;
        
    }

    public void MushGen() {
        //генерация грибов
        float[,] Mushpos = new float[intMushroom, 2];
        for (int i = 0; i < intMushroom; i++)
        {
            int mx = UnityEngine.Random.Range(0, 19);
            int mz = UnityEngine.Random.Range(0, 18);
            float x = (mx * 2) + xl;
            float z = (mz * 3) + zl;


            Mushpos[i, 0] = x;
            Mushpos[i, 1] = z;

            if (i > 0)
            {

                for (int j = 0; j <= i - 1; j++)
                {
                    if (x == Mushpos[j, 0] & z == Mushpos[j, 1])
                    {

                        while (x == Mushpos[j, 0] & z == Mushpos[j, 1])
                        {
                            mx = UnityEngine.Random.Range(0, 19);
                            mz = UnityEngine.Random.Range(0, 18);
                            x = (mx * 2) + xl;
                            z = (mz * 3) + zl;
                        }

                    }
                }

            }


            Instantiate(Mushroom, new Vector3(x, 1, z), Quaternion.identity);
            //Debug.Log("/i"+i+" /x"+x+" /z"+z);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        scoreLabel.text = Score.ToString();
        if (isOver|Player==null)
        {
            player player = GameObject.Find("GameManager").GetComponent(typeof(player)) as player;
            player.Lose();
            GameOn.SetActive(false);
            GameEnd.SetActive(true);
            OverLabel.text = "You`r score: " + Score.ToString();
        }
        if (Time.time > nEnemy)
        {
            rEnemy = UnityEngine.Random.Range(5, 7);
            nEnemy = Time.time + rEnemy;
            int mx1 = UnityEngine.Random.Range(1, 18);
            int x1 = (mx1 * 2) + xl;

            Instantiate(Cent, new Vector3(x1, 1.6f, -24), Quaternion.identity);
        }
    }
    public void Gameover() {
        isOver = true;
    }
    public bool getOver() {
        return isOver;
    }

    public void Scoreadd(int a) {
        Score += a;
    }
}
