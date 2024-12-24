using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPool : MonoBehaviour
{
    public static CardPool instance;

    private List<Card> list = new List<Card>();
    private int amount = 20;

    [SerializeField] private 
    void Start()
    {  
      if(instance == null) {
            instance = this;
        }
    }

    private void Innitialise() {
        for (int i = 0; i < amount; i++) {
        
        }
    }

    public void GetObject() {

    }
}
