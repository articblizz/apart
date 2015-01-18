using UnityEngine;
using System.Collections;
using UnityEditor;

public class myMonsterScript : MonoBehaviour {

    public float HP = 100;
    public float Speed = 3;
    public Transform player;

    bool isInside = false;

    bool isStunned = false;

    public float GonnaSpawn = 100;
    float spawnProgress = 0;

    float stunnedTimer;
    public float TimeStunned;

    // Use this for initialization
    void Start () {
    
    }
    
    // Update is called once per frame
    void Update () {


        if(isInside)
            DrainHp(.2f);

        if (!isStunned)
        {
            float step = Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.position, step);
        }
        else if(isStunned)
        {
            stunnedTimer += Time.deltaTime;
            if (stunnedTimer >= TimeStunned)
            {
                isStunned = false;
            }
        }
    }

    void SpawnProgress(float progress)
    {
        spawnProgress += progress;
        if (spawnProgress >= GonnaSpawn)
        {
            Spawn();
        }
    }

    //[MenuItem("Monster/Spawn Monster")]
    static void Spawn()
    {
        //Debug.Log("SPawn");
    }

    void Despawn()
    {

    }

    void Stun()
    {
        isStunned = true;
        stunnedTimer = 0;
    }

    void ToggleInside()
    {
        Debug.Log(isInside);
        isInside = !isInside;
    }

    void DrainHp(float hp)
    {
        Stun();
        //Debug.Log("Draingin " + hp);
        HP -= hp;
        if(HP <= 0)
            Destroy(gameObject);
    }
}
