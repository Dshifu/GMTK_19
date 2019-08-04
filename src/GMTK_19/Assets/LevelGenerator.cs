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
    [Required] public GameObject baseObjectWithParts;
    [Required] public GameObject[] secondCharacterPotentialPositions;


    private const int EnvironmentsCount = 2;
    private const int BigEnvironmentsCount = 1;
    private const int ReducersCount = 1;
    private const int MoversCount = 1;
    private const int SpaceCount = 1;

    private void Awake()
    {
        GenerateCurrentLevel();

        //Destroy(baseObjectWithParts);
        DestroyArrayElements(secondCharacterPotentialPositions);
    }

    private void GenerateCurrentLevel()
    {
        var sceneObject1 = new GameObject("Part1");
        var temp = Instantiate(baseObjectWithParts);
        GenerateParts(temp, sceneObject1);
        Destroy(temp);
        
        temp = Instantiate(baseObjectWithParts);
        var sceneObject2 = new GameObject("Part2");
        int randomScaleX = Random.Range(0, 2) == 0 ? -1 : 1;
        int randomScaleY = Random.Range(0, 2) == 0 ? -1 : 1;
        temp.transform.position = new Vector3(40f, 0 + (randomScaleY == -1 ? 20 : 0), 0f);
        temp.transform.localScale = new Vector3(randomScaleX, randomScaleY, 1f);
        GenerateParts(temp, sceneObject2);
        Destroy(temp);
        
        temp = Instantiate(baseObjectWithParts);
        var sceneObject3 = new GameObject("Part3");
        randomScaleX = Random.Range(0, 2) == 0 ? -1 : 1;
        randomScaleY = Random.Range(0, 2) == 0 ? -1 : 1;
        temp.transform.position = new Vector3(0f, 40f + (randomScaleY == -1 ? 20 : 0), 0f);
        temp.transform.localScale = new Vector3(randomScaleX, randomScaleY, 1f);
        GenerateParts(temp, sceneObject3);
        Destroy(temp);
        
        temp = Instantiate(baseObjectWithParts);
        var sceneObject4 = new GameObject("Part4");
        randomScaleX = Random.Range(0, 2) == 0 ? -1 : 1;
        randomScaleY = Random.Range(0, 2) == 0 ? -1 : 1;
        temp.transform.position = new Vector3(40f, 40f + (randomScaleY == -1 ? 20 : 0), 0f);
        temp.transform.localScale = new Vector3(randomScaleX, randomScaleY, 1f);
        GenerateParts(temp, sceneObject4);
        Destroy(temp);
        
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

    private void GenerateParts(GameObject parts, GameObject sceneObject)
    {
        foreach (Transform part in parts.transform)
        {
            int environmentsSpawned = 0;
            int reducersSpawned = 0;
            int moversSpawned = 0;
            int spacesSpawned = 0;
            int bigEnvironmentsSpawned = 0;
            foreach (Transform position in part)
            {
                if(environmentsSpawned == EnvironmentsCount && reducersSpawned == ReducersCount && moversSpawned == MoversCount && spacesSpawned == SpaceCount)
                    break;
                int randomChoose = RandomChoose(environmentsSpawned, moversSpawned, reducersSpawned, spacesSpawned);
                switch (randomChoose)
                {
                    case 0:
                        if (bigEnvironmentsSpawned != BigEnvironmentsCount)
                        {
                            Instantiate(bigEnvironments[Random.Range(0, bigEnvironments.Length)], position.position,
                                Quaternion.Euler(0f, 0f, Random.Range(0f, 360f))).transform.SetParent(sceneObject.transform);
                            bigEnvironmentsSpawned++;
                            environmentsSpawned++;
                        }
                        else
                        {
                            Instantiate(smallEnvironment[Random.Range(0, smallEnvironment.Length)], position.position,
                                Quaternion.Euler(0f, 0f, Random.Range(0f, 360f))).transform.SetParent(sceneObject.transform);
                            environmentsSpawned++;
                        }
                        break;
                    case 1:
                        Instantiate(reducers[Random.Range(0, reducers.Length)], position.position,
                            Quaternion.Euler(0f, 0f, Random.Range(0f, 360f))).transform.SetParent(sceneObject.transform);
                        reducersSpawned++;
                        break;
                    case 2:
                        Instantiate(movers[Random.Range(0, movers.Length)], position.position,
                            Quaternion.Euler(0f, 0f, Random.Range(0f, 360f))).transform.SetParent(sceneObject.transform);
                        reducersSpawned++;
                        break;
                    case 3:
                        Instantiate(spaceObjects[Random.Range(0, spaceObjects.Length)], position.position,
                            Quaternion.Euler(0f, 0f, Random.Range(0f, 360f))).transform.SetParent(sceneObject.transform);
                        reducersSpawned++;
                        break;
                }
            }            
        }
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
