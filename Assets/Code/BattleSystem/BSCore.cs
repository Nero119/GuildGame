﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GG.BattleSystem.CharacterSystem;

namespace GG.BattleSystem
{
    public partial class BSCore : MonoBehaviour
    {
        /*
        Rules:
        1)When all members of either side are down or dead (dead state included), the battle has ended.
        2)All alive party members gain exp and class points at the end of combat.
        3) all unconscious members of combat are brought back to life at the end of combat on 1 hit point (might actually make the player take a rest in order for them to come out of the ko state instead. Will ask)
        while (battle) {
 
Playerchoose();
Playerattack();
 
EnemyChoose();
Enemyattack();
 
EndRound();
 
}
This code above is a just a guid line I might need to use
        */
        public PlayerParty pParty; //The player party
        public EnemyParty eParty;  //The enemy Party


        public int pLoseCount;
        public int eLoseCount;

        public List<BSCombatant> Combatants;

        void Start()
        {
            pParty = GameObject.Find("Host").GetComponent<PlayerParty>();
            eParty = GameObject.Find("Enemies").GetComponent<EnemyParty>();
            pLoseCount = 0;
            //populate the combatants list
            for (int i = 0; i < pParty.party.Count; i++)
            {
                Combatants.Add(pParty.partymember(i) as BSCombatant);
                //restart character death timers check
                if (pParty.partymember(i).GetVitals(0).GetCurVale <= 0)
                {
                    pParty.partymember(i).deathCount = 0;
                    pLoseCount++;
                }
                
            }
            //add the enemy party to the list
            for (int i = 0; i < eParty.party.Count; i++)
            {
                Combatants.Add(eParty.party.ElementAt(i) as BSCombatant);
            }
            //Quick reset on the action bars
            foreach (BSCombatant entity in Combatants)
            {
                entity.standardBar = 0;
                entity.castingBar = 0;
                entity.isCasting = false;
                entity.standardBar += entity.GetBaseStats(4).fullValue;
            }
            //sort the comatants in order
            SortCombatants();
            //until one of the gauges is full, keep adding the speed
            while (Combatants.ElementAt(0).standardBar <= 100)
            {
                foreach (BSCombatant combi in Combatants)
                {
                    combi.standardBar += combi.GetBaseStats(4).fullValue;
                }
            }
            //Do some quick maths to put the characters in order, then we begin!
        }
        void Update()
        {

            SortCombatants();
            //Update the gauges to their respective points
            if (Combatants.ElementAt(0).standardBar <= 100)
            {
                foreach (BSCombatant combatant in Combatants)
                {
                    combatant.standardBar += combatant.GetBaseStats(4).fullValue;
                }
            }
            //assuimg the first loop finishes, what kind of turn is it?
            if (Combatants.ElementAt(0) is BaseCharacter)
            {
                Debug.Log("I am a player character");
                PlayerTurn();
            }
            else
            {
                Debug.Log("I am a creature");
                CreatureTurn();
            }

            //Enguage the turn of the first Combatant
            //Side note: if they have a spell they are channeling, enguage spell
            //Apply rules given to the action
            //Put him at the bottom of the turn after his action has been forfilled
        }
        #region Non-Unity Functions
        void StartUp()
        {
            
            int globalSpeed = 0; //Totalspeed of all entities
            //Add the players to the mix
            foreach(BSCombatant charles in pParty.party)
            {
                globalSpeed += charles.GetBaseStats(4).fullValue;
            }
            //Add the creatures to the mix
            foreach(BSCombatant charles in eParty.party)
            {
                globalSpeed += charles.GetBaseStats(4).fullValue;
            }
            //Divide up the number of times a combatant can act within the global speed
        }
        void SortCombatants()
        {
            Combatants.Sort();
            //For not-casting
            Combatants.Sort(delegate (BSCombatant EntityA, BSCombatant EntityB)
            {
                //This does not include casting 
                if (EntityA.standardBar == 0 && EntityB.standardBar == 0) return 0;
                else if (EntityA.standardBar == 0) return -1;
                else if (EntityB.standardBar == 0) return 1;
                else return EntityA.standardBar.CompareTo(EntityB.standardBar);

            });
            //Update the gauges to their respective points
            if (Combatants.ElementAt(0).standardBar <= 100)
            {
                foreach (BSCombatant combatant in Combatants)
                {
                    combatant.standardBar += combatant.GetBaseStats(4).fullValue;
                }
            }
        }
        void InitializeCombat()
        {

        }

        public void DismissCombat()
        {

        }
        public void DismissVictoryCombat()
        {

        }
        public void DismissDefeatCombat()
        {

        }
        #endregion
    }
}
