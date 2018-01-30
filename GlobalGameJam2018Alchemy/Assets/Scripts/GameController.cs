using GlobalGameJam2018Networking;
using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LayoutController))]
[RequireComponent(typeof(NetworkController))]
public class GameController : MonoBehaviour
{
    [Tooltip("In Singleplayer: Spawn igredient every x seconds.")]
    public int singlePlayerIgredientSpawnInterval = 5;

    [Tooltip("In Multiplayer: The spawn ingredient loop has to continue running to later support singleplayer.")]
    public int multiPlayerIngredientSpawnSleepInterval = 60;

    [Tooltip("Gold balance of the alchemist.")]
    public int gold;

    public event System.Action<bool> GameOver;

    private void Start() => StartCoroutine(SpawnIngredients());

    public IEnumerator SpawnIngredients()
    {
        var types = System.Enum.GetValues(typeof(ItemType)) as ItemType[];
        var colours = System.Enum.GetValues(typeof(IngredientColour)) as IngredientColour[];
        while (true)
        {
            if (!GetComponent<NetworkController>().IsSinglePlayer)
            {
                // Ingredients are coming via network and thus we only keep this coroutine running
                // if later a singleplayer game might be started.
                yield return new WaitForSeconds(multiPlayerIngredientSpawnSleepInterval);
                continue;
            }
            var inputs = GetComponent<LayoutController>().InteractivePipes.Values
                .Where(pipe => pipe.Pipe.Direction == PipeDirection.ToAlchemist).ToArray();
            if (inputs.Length > 0)
            {
                var ingredient = new Ingredient(
                    types[Random.Range(0, types.Length)],
                    colours[Random.Range(0, colours.Length)]);
                inputs[Random.Range(0, inputs.Length)].AddItem(ingredient);
            }
            yield return new WaitForSeconds(singlePlayerIgredientSpawnInterval);
        }
    }

    internal void TriggerGameOver(bool success) => GameOver?.Invoke(success);
}
