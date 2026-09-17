using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.PlayerLoop;
public class HabitatScript : MonoBehaviour
{
    public GameObject animal;

    public string animalName;
    private int minWelfare = 0;
    private int maxWelfare = 1;
    private int minShelterScore = 0;
    private int maxShelterScore = 1;
    private int minFoodScore = 0;
    private int maxFoodScore = 1;
    private int minWaterScore = 0;
    private int maxWaterScore = 1;
    private int minCleanScore = 0;
    private int maxCleanScore = 1;

    public int shelterScore;
    public int foodScore;
    public int waterScore;
    public int tempScore;
    public int cleanScore;

    void Start()
    {
        animal = GetComponent<Animal>();
    }
    void Update()
    {
        // Update the welfare scores based on the animal's needs
        shelterScore = Mathf.Clamp(shelterScore, minShelterScore, maxShelterScore);
        foodScore = Mathf.Clamp(foodScore, minFoodScore, maxFoodScore);
        waterScore = Mathf.Clamp(waterScore, minWaterScore, maxWaterScore);
        cleanScore = Mathf.Clamp(cleanScore, minCleanScore, maxCleanScore);
        // Calculate the overall welfare score
        int welfareScore = shelterScore + foodScore + waterScore + cleanScore;
        // Update the animal's welfare score
    }
}
