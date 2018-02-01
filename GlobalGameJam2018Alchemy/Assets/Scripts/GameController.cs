using BansheeGz.BGSpline.Components;
using GlobalGameJam2018Networking;
using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LayoutController))]
[RequireComponent(typeof(NetworkController))]
[RequireComponent(typeof(UserInterfaceController))]
public class GameController : MonoBehaviour
{
    /// <summary>Travel time of enemies in seconds.</summary>
    private const float EnemyTravelTime = 60f;

    [Tooltip("In Singleplayer: Spawn igredient every x seconds.")]
    public int singlePlayerIgredientSpawnInterval = 5;

    [Tooltip("Gold balance of the alchemist.")]
    public int gold;

    [Tooltip("Cursor of the path on which the enemies travel.")]
    public BGCcCursor cursor;

    [Tooltip("Enemies that are spawned by the game controller.")]
    public Enemy[] enemies;

    public event System.Action<bool> GameOver;

    private NetworkController network;
    private LayoutController layout;

    private Coroutine ingredientSpawning;
    private Coroutine enemySpawning;

    private void Start()
    {
        network = GetComponent<NetworkController>();
        layout = GetComponent<LayoutController>();

        // Handle game start
        GetComponent<UserInterfaceController>().GameStarted += () =>
        {
            if (ingredientSpawning != null) { StopCoroutine(ingredientSpawning); }
            ingredientSpawning = StartCoroutine(SpawnIngredients());
            if (network.IsSinglePlayer)
            {
                layout.CreateSingleplayerLevel();
                StartEnemySpawning();
            }
        };

        // Handle level start
        network.StartMultiplayerLevel += level =>
        {
            layout.CreateLevel(level);
            StartEnemySpawning();
        };
    }

    /// <summary>Starts to spawn enemy waves.</summary>
    private void StartEnemySpawning()
    {
        if (enemySpawning != null) { StopCoroutine(enemySpawning); }
        enemySpawning = StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(EnemyTravelTime);
            if (enemies.Length > 0)
            {
                SpawnEnemy(enemies[Random.Range(0, enemies.Length)]);
            }
        }
    }

    /// <summary>Spawns an enemy using the given prefab.</summary>
    /// <param name="enemyPrefab">Prefab used to spawn the enemy.</param>
    private void SpawnEnemy(Enemy enemyPrefab)
    {
        var enemy = Instantiate(enemyPrefab);
        enemy.cursor = cursor;
        enemy.arrivalTime = Time.time + EnemyTravelTime;
    }

    /// <summary>
    /// Spawn ingredients automatically if it is a singleplayer game.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnIngredients()
    {
        var types = new[] { ItemType.Herb, ItemType.Liquid, ItemType.Powder, ItemType.Steam };
        var colours = System.Enum.GetValues(typeof(IngredientColour)) as IngredientColour[];
        while (true)
        {
            var inputs = layout.InteractivePipes.Values
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
