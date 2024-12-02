using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class PaperAirplaneScript : BulletScript
{
    public WeaponBaseScript mySpawner;
    float lifespan = 0.0f;
    public GameObject myPlane;
    public List<GameObject> myBullets;

    public override void SetOwnStats(float[] myNumStats, bool isInfinite)
    {
        base.SetOwnStats(myNumStats, isInfinite);
    }

    public void SetSelfUp() {
        int totalAttacks = mySpawner.numAttacks + mySpawner.numExtraAttacks;
        //Debug.Log("Total bullets to ");
        float angleBetween = 360 / totalAttacks;
        for (int i = 0; i < totalAttacks; i++)
        {
            GameObject current = Instantiate(myPlane, transform.position + (Vector3.up * 2f), Quaternion.identity, transform);
            current.transform.RotateAround(transform.position, Vector3.forward, angleBetween * i);
            current.GetComponent<BulletScript>().damage = damage;
            current.GetComponent<BulletScript>().infinitePass = true;
            transform.localScale.Set(area, area, area);
            myBullets.Add(current);
        }
    }

    void FixedUpdate()
    {

        transform.position = FindFirstObjectByType<PlayerManager>().gameObject.transform.position;
        transform.Rotate(Vector3.forward, -5);
        //transform.position = FindFirstObjectByType<PlayerManager>().transform.position;
        lifespan += Time.deltaTime;
        if (lifespan >= maxLife)
        {
            Destroy(gameObject);
        }
    }
}
