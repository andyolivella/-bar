using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class CycleCarsAlternative : MonoBehaviour
{
    public GameObject[] cars;

    public GameObject[] carsPolice;

    // What car is active at start
    public int carNumber;

    public virtual void Start()
    {
        int i = 0;
        while (i < this.cars.Length)
        {
            this.cars[i].SetActive(false);
            this.carsPolice[i].SetActive(false);
            i++;
        }
        this.cars[this.carNumber].SetActive(true);
    }

    public virtual void Update()
    {
        if (Input.GetKeyUp("space"))
        {
            if (this.carNumber < (this.cars.Length - 1))
            {
                this.carNumber++;
            }
            else
            {
                this.carNumber = 0;
            }
            this.changeCars(this.carNumber);
        }
        // Toggle Police Version of the car
        if ((this.cars[this.carNumber].activeInHierarchy == true) && Input.GetKeyDown("p"))
        {
            this.cars[this.carNumber].SetActive(false);
            this.carsPolice[this.carNumber].SetActive(true);
        }
        else
        {
            if (Input.GetKeyDown("p"))
            {
                this.cars[this.carNumber].SetActive(true);
                this.carsPolice[this.carNumber].SetActive(false);
            }
        }
        //
        ///////
        if (Input.GetKeyDown("1"))
        {
            this.carNumber = 0;
            this.changeCars(this.carNumber);
        }
        //////
        if (Input.GetKeyDown("2"))
        {
            this.carNumber = 1;
            this.changeCars(this.carNumber);
        }
        //////
        if (Input.GetKeyDown("3"))
        {
            this.carNumber = 2;
            this.changeCars(this.carNumber);
        }
        //////
        if (Input.GetKeyDown("4"))
        {
            this.carNumber = 3;
            this.changeCars(this.carNumber);
        }
        //////
        if (Input.GetKeyDown("5"))
        {
            this.carNumber = 4;
            this.changeCars(this.carNumber);
        }
        //////
        if (Input.GetKeyDown("6"))
        {
            this.carNumber = 5;
            this.changeCars(this.carNumber);
        }
        //////
        if (Input.GetKeyDown("7"))
        {
            this.carNumber = 6;
            this.changeCars(this.carNumber);
        }
        //////
        if (Input.GetKeyDown("8"))
        {
            this.carNumber = 7;
            this.changeCars(this.carNumber);
        }
        //////
        if (Input.GetKeyDown("9"))
        {
            this.carNumber = 8;
            this.changeCars(this.carNumber);
        }
        //////
        if (Input.GetKeyDown("0"))
        {
            this.carNumber = 9;
            this.changeCars(this.carNumber);
        }
        //////
        if (Input.GetKeyDown("-"))
        {
            this.carNumber = 10;
            this.changeCars(this.carNumber);
        }
        //////
        if (Input.GetKeyDown("="))
        {
            this.carNumber = 11;
            this.changeCars(this.carNumber);
        }
    }

    public virtual void changeCars(int carNumber)
    {
        int i = 0;
        while (i < this.cars.Length)
        {
            this.cars[i].SetActive(false);
            this.carsPolice[i].SetActive(false);
            i++;
        }
        this.cars[carNumber].SetActive(true);
    }

    public CycleCarsAlternative()
    {
        this.carNumber = 2;
    }

}