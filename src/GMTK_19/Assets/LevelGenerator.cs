using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [Required] public GameObject[] bigEnvironments;
    [Required] public GameObject[] smallEnvironment;
    [Required] public GameObject[] reducers;
    [Required] public GameObject[] movers;
    [Required] public GameObject[] spaceObjects;
    [Required] public GameObject secondCharacter;
    [Required] public GameObject player;
    [Required] public GameObject[] partsPositions;
    [Required] public GameObject[] secondCharacterPotentialPositions;


    private const int EnvironmentsCount = 2;
    private const int ReducersCount = 2;
    private const int MoversCount = 1;
    private const int SpaceCount = 1;

    private void Awake()
    {
        GenerateCurrentLevel();

        DestroyArrayElements(partsPositions);
        DestroyArrayElements(secondCharacterPotentialPositions);
    }

    private void GenerateCurrentLevel()
    {
        var parentForBoosts = new GameObject("SceneObjects");
        foreach (var part in partsPositions)
        {
            int environmentsSpawned = 0;
            int reducersSpawned = 0;
            int moversSpawned = 0;
            int spacesSpawned = 0;
            int bigEnvironmentsSpawned = 0;
            foreach (Transform position in part.transform)
            {
                if(environmentsSpawned == EnvironmentsCount && reducersSpawned == ReducersCount && moversSpawned == MoversCount && spacesSpawned == SpaceCount)
                    break;
                int randomChoose = RandomChoose(environmentsSpawned, moversSpawned, reducersSpawned, spacesSpawned);
                switch (randomChoose)
                {
                    case 0:
                        if (bigEnvironmentsSpawned == 0)
                        {
                            Instantiate(bigEnvironments[Random.Range(0, bigEnvironments.Length)], position.position,
                                Quaternion.Euler(0f, 0f, Random.Range(0f, 360f))).transform.SetParent(parentForBoosts.transform);
                            bigEnvironmentsSpawned++;
                            environmentsSpawned++;
                        }
                        else
                        {
                            Instantiate(smallEnvironment[Random.Range(0, smallEnvironment.Length)], position.position,
                                Quaternion.Euler(0f, 0f, Random.Range(0f, 360f))).transform.SetParent(parentForBoosts.transform);
                            environmentsSpawned++;
                        }
                        break;
                    case 1:
                        Instantiate(reducers[Random.Range(0, reducers.Length)], position.position,
                            Quaternion.Euler(0f, 0f, Random.Range(0f, 360f))).transform.SetParent(parentForBoosts.transform);
                        reducersSpawned++;
                        break;
                    case 2:
                        Instantiate(movers[Random.Range(0, movers.Length)], position.position,
                            Quaternion.Euler(0f, 0f, Random.Range(0f, 360f))).transform.SetParent(parentForBoosts.transform);
                        reducersSpawned++;
                        break;
                    case 3:
                        Instantiate(spaceObjects[Random.Range(0, spaceObjects.Length)], position.position,
                            Quaternion.Euler(0f, 0f, Random.Range(0f, 360f))).transform.SetParent(parentForBoosts.transform);
                        reducersSpawned++;
                        break;
                }
            }            
        }

        
        
//        var envElementsCount = environmentElements.Length;
//        var currentElementIndex = 0;
//        var parentForBoosts = new GameObject("BoostsParent");
//
//        parentForBoosts.transform.SetParent(transform);
//
//        for (var elementPosition = 0; elementPosition < environmentElementsPositions.Length; elementPosition++)
//        {
//            Instantiate(environmentElements[currentElementIndex], environmentElementsPositions[elementPosition].transform.position,
//                Quaternion.Euler(0f,0f,Random.Range(0f,360f))).transform.SetParent(parentForBoosts.transform);
//
//            currentElementIndex++;
//            if (currentElementIndex >= envElementsCount)
//                currentElementIndex = 0;
//        }
//
//        var rand = Random.Range(0, secondCharacterPotentialPositions.Length);
//        Instantiate(secondCharacter, secondCharacterPotentialPositions[rand].transform.position, Quaternion.identity).transform.SetParent(parentForBoosts.transform);
    }
    
    private int RandomChoose(int environmentsSpawned, int reducersSpawned, int moversSpawned, int spacesSpawned)
    {
        int randomChoose = Random.Range(0,4);
        switch (randomChoose)
        {
            case 0:
                if (environmentsSpawned == EnvironmentsCount)
                    RandomChoose(environmentsSpawned, reducersSpawned, moversSpawned, spacesSpawned);
                break;
            case 1:
                if (reducersSpawned == ReducersCount)
                    RandomChoose(environmentsSpawned, reducersSpawned, moversSpawned, spacesSpawned);
                break;
            case 2:
                if (moversSpawned == MoversCount)
                    RandomChoose(environmentsSpawned, reducersSpawned, moversSpawned, spacesSpawned);
                break;
            case 3:
                if (spacesSpawned == SpaceCount)
                    RandomChoose(environmentsSpawned, reducersSpawned, moversSpawned, spacesSpawned);
                break;
        }

        return randomChoose;
    }

    private void DestroyArrayElements(IEnumerable<GameObject> arr)
    {
        foreach (var element in arr)
            Destroy(element);
    }
}
