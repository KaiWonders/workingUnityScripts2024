using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public float maxHealth = 100;
    public float currentHealth = 100;
    private DespawnDestroy kill;

    public void HealthChanged(float changeHealthBy){
        this.currentHealth += changeHealthBy;
        this.currentHealth = Mathf.Clamp(this.currentHealth, 0, maxHealth); // Clamps the maxiumum and minimum health to maxHealth and 0
        if (this.currentHealth <= 0){
            kill.DespawnObject();
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
