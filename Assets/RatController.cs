using System;
using System.Collections;
using System.Collections.Generic;
using Stuart.Scripts;
using UnityEngine;
using UnityEngine.AI;

public class RatController : MonoBehaviour
{
   private PlayerMovement player;
   [SerializeField] private float detectionRange;
   [SerializeField] private float explodeRange=3f;
   [SerializeField] private float damageAmount;
   [SerializeField] private GameObject vfx;
   private bool exploded = false;
   private NavMeshAgent agent;
   private Damageable damageable;
   private void Awake()
   {
      player = FindObjectOfType<PlayerMovement>();
      agent = GetComponent<NavMeshAgent>();
      damageable = GetComponent<Damageable>();
   }

   private void OnEnable()
   {
      damageable.OnDeath += Die;
   }

   private void OnDisable()
   {
      damageable.OnDeath -= Die;
   }

   private void Update()
   {
      if (exploded) return;
      float distance = Vector3.Distance(transform.position, player.transform.position);
      if (distance <= detectionRange)
      {
         agent.SetDestination(player.transform.position);
      }

      if (distance <= explodeRange)
      {
         Explode(distance);
      }
      
   }

   private void Explode(float distance)
   {
      exploded = true;
      agent.SetDestination(transform.position);
      agent.isStopped = true;
      Damageable damageable = player.GetComponent<Damageable>();
      if (damageable != null)
      {
         vfx.gameObject.SetActive(true);
         damageable.TakeDamage(damageAmount/distance);
         GetComponentInChildren<MeshRenderer>().enabled = false;
         GetComponentInChildren<MeshFilter>().mesh = null;
         Invoke(nameof(Kill),1.5f);
      }
      
   }

   private void Kill()
   {
      Destroy(gameObject);
   }

   private void Die()
   {
      
      Explode(Vector3.Distance(transform.position, player.transform.position));
   }
}
