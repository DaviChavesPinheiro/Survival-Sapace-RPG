using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float demage = 5;
    public float velocity = 15;
    public float lifeTime = 5;
    public float coolDown = 0.5f;
    [SerializeField] List<string> friendlyTags;
    
    GameObject instigator;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Drop") return;
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
