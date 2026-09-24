using JetBrains.Annotations;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;
using System;
public class HabitatScript : MonoBehaviour
{
    [SerializeField] private Animal animal;

    public string animalName;

    //CHANGE WHEN CHANGING NUMBER OF DIFFERENT HABITAT MODIFICATIONS
    public float numOfValues;

    private float minWelfare = 0;
    private float maxWelfare = 1;
    private float minShelterScore = 0;
    private float maxShelterScore = 1;
    private float minFoodScore = 0;
    private float maxFoodScore = 1;
    private float minWaterScore = 0;
    private float maxWaterScore = 1;
    private float minCleanScore = 0;
    private float maxCleanScore = 1;

    //Score of each facet calculated with the values
    public float shelterScore;
    public float foodScore;
    public float waterScore;
    public float tempScore;
    public float cleanScore;

    //Real Habitat Values
    private float shelterValue;
    private float foodValue;
    private float waterValue;
    private float tempValue;
    private float cleanValue;

    void Start()
    {
        animal = GetComponent<Animal>();
    }
    void Update()
    {
        shelterScore =  CalculateScore(Math.Abs(shelterValue - animal.idealShelterValue));
        foodScore = CalculateScore(Math.Abs(foodValue - animal.idealFoodValue));
        waterScore = CalculateScore(Math.Abs(waterValue - animal.idealWaterValue));
        cleanScore = CalculateScore(Math.Abs(cleanValue - animal.idealCleanValue));
        tempScore = CalculateScore(Math.Abs(tempValue - animal.idealTempValue));
        // Calculate the overall welfare score
        float welfareScore = shelterScore + foodScore + waterScore + cleanScore;
        // Update the animal's welfare score
    }

    public float CalculateScore(float valueDifference)
    {
        return 1 - (valueDifference / 9); //9 is the largest difference between habitat values assuming that the lowest is 1 and highest is 10
    }

}
