using System;
using System.Collections;
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
    [Required] public List<GameObject> uniqObjects;
    [Required] public GameObject secondCharacter;
    [Required] public GameObject player;
    [Required] public GameObject baseObjectWithParts;


    private const int EnvironmentsCount = 2;
    private const int BigEnvironmentsCount = 1;
    private const int ReducersCount = 1;
    private const int MoversCount = 1;
    private const int SpaceCount = 1;

    private IEnumerator Start()
    {
        GenerateCurrentLevel();
        yield return new WaitForSeconds(0.1f);
        GenerateCharacters();
    }

    private void GenerateCharacters()
    {
        var charactersParts = GameObject.FindGameObjectsWithTag(PrefsName.PartForCharacter);

        int playerPosIndex = Random.Range(0, charactersParts.Length);
        int secondCharacterIndex = charactersParts.Length - playerPosIndex;

        Instantiate(player, charactersParts[playerPosIndex].transform.position, Quaternion.identity);
        Instantiate(secondCharacter, charactersParts[secondCharacterIndex].transform.position,
            Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
        
        for (int i = 0; i < charactersParts.Length; i++)
        {
            if(i == playerPosIndex || i== secondCharacterIndex)
                continue;
            var position = charactersParts[i].transform.GetChild(Random.Range(0, charactersParts[i].transform.childCount));

            int uniqComponentsNumber = Random.Range(0, uniqObjects.Count);
            Instantiate(uniqObjects[uniqComponentsNumber], position);
            uniqObjects.RemoveAt(uniqComponentsNumber);
        }

    }

    private void GenerateCurrentLevel()
    {
        
        int count = 0;
        for (int i = 0; i < 3; i++)
        for (int j = 0; j < 3; j++)
            GenerateWord(new Vector3(i%3f * 40f, j%3f * 40f, 0f), count++);
    }

    private void GenerateWord(Vector3 position, int generatedNumber)
    {
        var temp = Instantiate(baseObjectWithParts);
        var sceneObject = new GameObject("Part" + generatedNumber);
        var randomScaleX = Random.Range(0, 2) == 0 ? -1 : 1;
        var randomScaleY = Random.Range(0, 2) == 0 ? -1 : 1;
        temp.transform.position = new Vector3(position.x, position.y + (randomScaleY == -1 ? 20 : 0), 0f);
        temp.transform.localScale = new Vector3(randomScaleX, randomScaleY, 1f);
        GenerateParts(temp, sceneObject, generatedNumber != 5);
        Destroy(temp);
    }

    private void GenerateParts(GameObject parts, GameObject sceneObject, bool isCanBeCharacter)
    {
        foreach (Transform part in parts.transform)
        {
            if (part.CompareTag(PrefsName.PartForCharacter))
            {
                if (isCanBeCharacter)
                {
                    Instantiate(part, sceneObject.transform, true);
                    continue;
                }
                var position = part.transform.GetChild(Random.Range(0, part.transform.childCount));

                int uniqComponentsNumber = Random.Range(0, uniqObjects.Count);
                Instantiate(uniqObjects[uniqComponentsNumber], position);
                uniqObjects.RemoveAt(uniqComponentsNumber);
                Destroy(part.gameObject);
            }
            
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
