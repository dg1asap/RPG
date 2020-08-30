using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameramovement : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        transform.position = new Vector3(target.position.x,target.position.y,transform.position.z);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }

    public void BeginKick()
    {
        anim.SetBool("kick_active",true);
        StartCoroutine(KickCo());
    }
    public IEnumerator KickCo()
    {
        yield return null;
        anim.SetBool("kick_active",false);
    }

}
