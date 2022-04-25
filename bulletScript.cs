using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    [SerializeField] private float limitDestroy;
    private float timer;
    public static Collision bulletCollide;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= limitDestroy) Destroy(this.gameObject);

    }

    private void OnCollisionEnter(Collision collision)
    {
        bulletCollide = collision;
        Destroy(this.gameObject);
    }
}
