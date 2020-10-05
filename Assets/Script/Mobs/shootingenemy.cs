using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class shootingenemy : MonoBehaviour

{
    public float chaseRadius;
    public float speed;

    public float stoppingDistance;

    public float retreaDistance;



    private float timeBtwShots;

    public float startTimeBtwShots;



    public Animator anim;





    public Transform player;
    
    public GameObject projectile;
    public GameObject projectile1;


    // Start is called before the first frame update

    void Start()

    {
        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBtwShots = startTimeBtwShots;

    }



    // Update is called once per frame

    void Update()

    {
        if (Vector3.Distance(player.position, transform.position) <= chaseRadius)
        {

            if (Vector2.Distance(transform.position, player.position) > stoppingDistance)

            {

                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            }
            else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreaDistance)

            {

                transform.position = this.transform.position;

            }

            else if (Vector2.Distance(transform.position, player.position) < retreaDistance)

            {

                transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);

            }



            if (timeBtwShots <= 0)
            {
               

                StartCoroutine(AttackCo());
                Instantiate(projectile, transform.position, Quaternion.identity);
                    Instantiate(projectile1, transform.position, Quaternion.identity);
             
               
                timeBtwShots = startTimeBtwShots;

            }
            else
            {
               

                timeBtwShots -= Time.deltaTime;

            }

        }

    }



    public IEnumerator AttackCo()
    {
       
        anim.SetBool("attack", true);
        yield return new WaitForSeconds(1f);
        
        anim.SetBool("attack", false);
    }
}