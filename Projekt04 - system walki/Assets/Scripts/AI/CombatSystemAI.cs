using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace combat{
    public class CombatSystemAI: CombatSystem
    {
        public GameObject player;

        public float maxLife = 50;
        public HealthBar healthBar;

        [HideInInspector]
        public bool followingPlayer = false;

        public float cooldown = 1f;
        private float lastFired;

        private void Start() {
            damage = 5.0f; 
            life = maxLife;
            healthBar.SetMaxHealth(maxLife);
        }

        public override void DealDamage(float inDamage)
        {
            player.GetComponent<CombatSystem>().TakeDamage(damage);
        }

        public override void TakeDamage(float inDamage){
            this.life -= inDamage;
            healthBar.SetHealth(life);

            if(this.life <= 0){
                FindObjectOfType<PlayerInteractionComponent>().nearEnemies.Remove(gameObject);
                Destroy(gameObject);
            }
        }

        private void FixedUpdate() {
            if(player != null && followingPlayer == false){
                if(Vector3.Distance(gameObject.transform.position, player.transform.position) < 5.0f){
                    
                    if(Vector3.Angle(gameObject.transform.forward, player.transform.position - gameObject.transform.position) < 90.0f){
                        followingPlayer = true;
                        gameObject.GetComponent<AIMove>().stateFlags[2] = true;
                    }
                }
            }
            else{
                if(lastFired > cooldown){
                    if(player != null && followingPlayer == true){
                        if(Vector3.Distance(gameObject.transform.position, player.transform.position) < 3.0f){
                            Debug.Log(gameObject.transform.forward + "  "  + (player.transform.position - gameObject.transform.position));
                            if(Vector3.Angle(gameObject.transform.forward, player.transform.position - gameObject.transform.position) < 45.0f){
                                
                                DealDamage(damage);
                                lastFired = 0.0f;
                            }
                        }
                    }
                }
                else{
                    lastFired += Time.deltaTime;
                }
            }
        }
    }
}
