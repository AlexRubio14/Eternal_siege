using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceBall : MonoBehaviour
{
    [SerializeField] private float speed;

    private float experience;
    private bool follow;
    private GameObject target;

    private void Start()
    {
        follow = false;
    }

    private void Update()
    {
        ExperienceBallFollows();
    }

    private void ExperienceBallFollows()
    {
        if (follow)
        {
            Vector3 direction = target.transform.localPosition - transform.localPosition;
            GetComponent<Rigidbody>().velocity = direction * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(collision is CapsuleCollider)
            {
                ExperienceManager.instance.SetExperience(experience);
                Destroy(gameObject);
            }
            else if(collision is SphereCollider && !follow) 
            {
                follow = true;
                target = collision.gameObject;
            }
        }
    }

    public void SetExperience(float _experience)
    {
        experience = _experience;
    }
}
