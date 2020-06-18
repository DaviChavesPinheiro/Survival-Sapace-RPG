using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float demage = 5;
    public float velocity = 15;
    public float life_time = 5;
    public float coolDown = 0.5f;
    [SerializeField] List<string> friendlyTags;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(!isFriendly(collider.tag)){
            GiveDemage(collider);
            Explode();
        }
    }

    private bool isFriendly(string tag)
    {
        foreach (string friendlyTag in friendlyTags)
        {
            if (friendlyTag == tag)
            {
                return true;
            }
        }
        return false;
    }

    public void SetFriend(GameObject friend){
        friendlyTags.Add(friend.tag);
    }

    private void GiveDemage(Collider2D collider)
    {
        Health healthComponent = collider.GetComponent<Health>();

        if (healthComponent)
        {
            healthComponent.TakeDamage(demage);
        }
    }
    
    private void Explode(){
        Destroy(gameObject);
    }
}
