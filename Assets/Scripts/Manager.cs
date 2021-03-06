using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Manager : MonoBehaviour
{
    public Sprite[] cardFace;
    public Sprite cardBack;
    public GameObject[] cards;
    public AudioClip[] lstEmitirNomesTurma;
    public AudioSource pesquisaItensNome;

    private bool _init = false;
    private int _matches = 19;

    void Start(){
        pesquisaItensNome = gameObject.GetComponent<AudioSource>();
    }


    void Update() {

        if (!_init)
            initializeCards ();

        if (Input.GetMouseButtonUp (0))
            checkCards ();        
        
    }

    void initializeCards () {
        for (int id = 0; id < 2; id++){
            for (int i = 1; i < 20; i++){
                
                bool test = false;
                int choice = 0;
                while (!test){
                    choice = Random.Range (0, cards.Length);                    
                    test = !(cards[choice].GetComponent<Card>().initialized);
                }                
                cards[choice].GetComponent<Card>().cardValue = i;
                cards[choice].GetComponent<Card>().initialized = true;                
            }
        }
        foreach(GameObject c in cards)
            c.GetComponent<Card> ().setupGraphics ();

        if (!_init)
            _init = true;        
    }

    public Sprite getCardBack (){
        return cardBack;
    }

    public Sprite getCardFace (int i){
        return cardFace [i - 1];
    }

    void checkCards() {
        List<int> c = new List<int> ();

        for (int i = 0; i < cards.Length; i++){
            if (cards [i].GetComponent<Card> ().state == 1)
            c.Add (i);

        }
        
        if (c.Count == 2)
            cardComparison (c);
    }

    void cardComparison(List<int> c){
        Card.DO_NOT = true;

        int x = 0;

        pesquisaItensNome.clip = lstEmitirNomesTurma[0];
        pesquisaItensNome.Play();        

        if (cards [c [0]].GetComponent<Card>().cardValue == cards [c [1]].GetComponent<Card>().cardValue){
            x = 2;
            _matches--;

            

            //emitir o som
            for (int k = 0; k < lstEmitirNomesTurma.Length; k++){
                if (cards [c [0]].GetComponent<Card>().cardValue == k){

                    pesquisaItensNome.clip = lstEmitirNomesTurma[k];
                    pesquisaItensNome.Play();
                    Debug.Log("acerto k: "+ k);
                    Debug.Log("acerto: "+ cards [c [0]].GetComponent<Card>().cardValue);
                }
            }

        
            if (_matches == 0){
                new WaitForSeconds (3);
                SceneManager.LoadScene(0);
            }
            

        }

        for (int i = 0; i < c.Count; i++){
            cards [c [i]].GetComponent<Card> ().state = x;
            cards [c [i]].GetComponent<Card> ().falseCheck ();
        }


    }
}
