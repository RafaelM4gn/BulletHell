using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private List<GameObject> inventory = new List<GameObject>();

    public GameObject inHand = null;
    [SerializeField] public GameObject model = null;

    private KeyCode[] keys = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5};
    //private KeyCode[] keys = { KeyCode.Keypad1, KeyCode.Keypad2, KeyCode.Keypad3, KeyCode.Keypad4, KeyCode.Keypad5};
    void Start(){
        inHand = null;
    }
    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.F)){
            PickUp();
        }
        for (int i = 0; i < 5; i++)
        {
            if(Input.GetKeyDown(keys[i])){
                if (inventory.Count >= i + 1){
                    inHand.SetActive(false);
                    inHand = inventory[i];
                    inHand.SetActive(true);
                    UpdateStatus(inHand);
                }
            }
        }
    }
    void PickUp(){
        if(inventory.Count <= 5){
            GameObject target = FindClosestTagged(transform.position, "Item", 3);
            if (target != null && !target.GetComponent<Item>().onInventory){
                inventory.Add(target);
                target.GetComponent<Item>().onInventory = true;
                target.transform.SetParent(transform.transform);
                //target.transform.position = transform.position;
                if (inventory.Count == 1){
                    UpdateStatus(target);
                    inHand = target;
                    inHand.SetActive(true);
                    
                } else {
                    target.SetActive(false);
                }
            } 
        } else {
            Debug.Log("FULL");
        }
    }
    GameObject FindClosestTagged(Vector3 position, string tag, float range){
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(tag);
        GameObject closest = null;
        float distance = range;
        foreach (GameObject go in gos) {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance) {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
    private void UpdateStatus(GameObject target){
        GetComponent<ClickAttack>().SetStatus(target.GetComponent<StandardGunShoot>().GetAttackRange(), target.GetComponent<StandardGunShoot>().GetAttackCD(), target.GetComponent<StandardGunShoot>().GetWindUp());
        model.GetComponent<AnimationStateController>().SetPlayerAttackSpeed(1/target.GetComponent<StandardGunShoot>().GetAttackCD());
    }
}