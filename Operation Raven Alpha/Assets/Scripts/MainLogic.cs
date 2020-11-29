using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLogic : MonoBehaviour
{
    public GameObject player;

    //Roofs variables
    private GameObject[] roofs;
    private string actualRoof;
    private string oldRoof;

    public Material closedRoof;
    public Material openRoof;


    //energy variables
    public int energy;
    public UnityEngine.UI.Slider energyBar;

    //time variables
    public float startTime;
    public float stepTime; 

    // Start is called before the first frame update
    void Start()
    {
        /*set comparison tags to nothing/blank*/
        actualRoof = "";
        oldRoof = "NONE";
        energy = 100;

        /*initialice array to objects with "roof" tag*/
        roofs = GameObject.FindGameObjectsWithTag("Roof");
        startTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        /*get actual roof*/
        actualRoof = getClosestRoof();

        if (actualRoof != oldRoof){
            roomChange(actualRoof);
            oldRoof = actualRoof;
        }


        /*reduce engergy as time passes*/
        stepTime = Time.realtimeSinceStartup - startTime;
        if (stepTime > 1){
            energy--;
            refreshEnergy();
            startTime = Time.realtimeSinceStartup;
        }
    }

    //refill energy
    public void refillEnergy(int value)
    {
        energy = energy + value;

        if (energy > 100)
        {
            energy = 100;
        }
        refreshEnergy();    
    }

    //make energy bar display the same as energy
    public void refreshEnergy(){
        energyBar.value = energy;
    }


    //Find closest roof
    public string getClosestRoof() {
        string roofMin = "";
        float minDistance = Mathf.Infinity;

        //for each object in roofs
        foreach (GameObject gameObj in roofs) {
            Transform roof = gameObj.transform;

            float dist = Vector3.Distance(roof.position, player.transform.position);

            if (dist < minDistance) {
                roofMin = roof.gameObject.name;
                minDistance = dist;
            }
        }
        return roofMin;
    }

    //room change
    public void roomChange(string actual) {

        foreach (GameObject gameObj in roofs) {
            gameObj.GetComponent<MeshRenderer>().material = closedRoof;
        }
        GameObject.Find(actual).GetComponent<MeshRenderer>().material = openRoof;
    }
}
