using BansheeGz.BGSpline.Components;
using GlobalGameJam2018Networking;
using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LayoutController))]
[RequireComponent(typeof(NetworkController))]
public class GameController : MonoBehaviour
{
    /// <summary>Travel time of enemies in seconds.</summary>
    private const float EnemyTravelTime = 60f;

    [Tooltip("In Singleplayer: Spawn igredient every x seconds.")]
    public int singlePlayerIgredientSpawnInterval = 5;

    [Tooltip("Gold balance of the alchemist.")]
    public int gold;

    public BGCcCursor cursor;
    public Enemy[] enemies;

    public event System.Action<bool> GameOver;

    private void Start()
    {
        StartCoroutine(SpawnIngredients());
        var enemy = Instantiate(enemies[0]);
        enemy.cursor = cursor;
        enemy.arrivalTime = Time.time + EnemyTravelTime;
    }


    /// <summary>
    /// Spawn ingredients automatically if it is a singleplayer game.
    /// </summary>
    /// <returns></returns>
    public IEnumerator SpawnIngredients()
    {
        var types = new[] { ItemType.Herb, ItemType.Liquid, ItemType.Powder, ItemType.Steam };
        var colours = System.Enum.GetValues(typeof(IngredientColour)) as IngredientColour[];
        while (true)
        {
            if (!GetComponent<NetworkController>().IsSinglePlayer)
            {
                // Ingredients are coming via network and thus we only keep this coroutine running
                // because later a singleplayer game might be started.
                yield return new WaitForSeconds(singlePlayerIgredientSpawnInterval);
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
