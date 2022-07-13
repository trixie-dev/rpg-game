using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroScript : MonoBehaviour
{
    public class Hero
    {
        public string name { get; set; }

        public float currentHP { get; set; }
        public float maxHP { get; set; }
        public int gold { get; set; }
        public float EX { get; set; }
        public int level { get; set; }


    }
}
