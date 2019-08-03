using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Required] public GameObject[] environmentElements;
    [Required] public GameObject[] environmentElementsPositions;
    [Required] public GameObject[] secondCharacterPotentialPositions;
    [Required] public GameObject secondCharacter;

    private void Awake()
    {
        GenerateCurrentLevel();

        DestroyArrayElements(environmentElementsPositions);
        DestroyArrayElements(secondCharacterPotentialPositions);
    }

    private void GenerateCurrentLevel()
    {
        var envElementsCount = environmentElements.Length;
        var currentElementIndex = 0;
        var parentForBoosts = new GameObject("BoostsParent");

        parentForBoosts.transform.SetParent(transform);

        for (var elementPosition = 0; elementPosition < environmentElementsPositions.Length; elementPosition++)
        {
            Instantiate(environmentElements[currentElementIndex], environmentElementsPositions[elementPosition].transform.position,
                Quaternion.identity).transform.SetParent(parentForBoosts.transform);

            currentElementIndex++;
            if (currentElementIndex >= envElementsCount)
                currentElementIndex = 0;
        }

        var rand = Random.Range(0, secondCharacterPotentialPositions.Length);
        Instantiate(secondCharacter, secondCharacterPotentialPositions[rand].transform.position, Quaternion.identity).transform.SetParent(parentForBoosts.transform);
    }

    private void DestroyArrayElements(IEnumerable<GameObject> arr)
    {
        foreach (var element in arr)
            Destroy(element);
    }
}
