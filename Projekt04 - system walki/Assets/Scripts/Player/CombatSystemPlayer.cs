using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace combat{
    public class CombatSystemPlayer : CombatSystem
    {
        public float maxLife = 50.0f;
        public HealthBar healthBar;

        private void Start() {
            damage = 10.0f;
            life = maxLife;
            healthBar.SetMaxHealth(maxLife);
        }

        private void FixedUpdate() {
            if(Input.GetKeyDown("space")){
                DealDamage(damage);
            }
        }

        public override void DealDamage(float inDamage){
            List<GameObject> nearEnemies = gameObject.GetComponent<PlayerInteractionComponent>().nearEnemies;

            for(int i = 0; i < nearEnemies.Count; i++){
                if(Vector3.Distance(gameObject.transform.position, nearEnemies[i].transform.position) < 3.0f){
                    if(Vector3.Angle(gameObject.transform.forward, nearEnemies[i].transform.position - gameObject.transform.position) < 45.0f){
                        nearEnemies[i].GetComponent<CombatSystem>().TakeDamage(damage);
                    }
                }
            }
        }

        public override void TakeDamage(float inDamage){
            this.life -= inDamage;
            healthBar.SetHealth(life);

            if(this.life <= 0){
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    
}

