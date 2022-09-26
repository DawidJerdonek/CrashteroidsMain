using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestSuite
{
    private Game game;

    [SetUp]
    public void Setup()
    {
        GameObject gameGameObject =
            MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
        game = gameGameObject.GetComponent<Game>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(game.gameObject);
    }

    [UnityTest]
    public IEnumerator AsteroidsMoveDown()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        float initialYPos = asteroid.transform.position.y;
        yield return new WaitForSeconds(0.1f);

        Assert.Less(asteroid.transform.position.y, initialYPos);
    }

    [UnityTest]
    public IEnumerator GameOverOccursOnAsteroidCollision()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = game.GetShip().transform.position;
        yield return new WaitForSeconds(0.1f);

        Assert.True(game.isGameOver);
    }

    [UnityTest]
    public IEnumerator NewGameRestartsGame()
    {
        //1
        game.isGameOver = true;
        game.NewGame();
        //2
        Assert.False(game.isGameOver);
        yield return null;
    }

    [UnityTest]
    public IEnumerator LaserMovesUp()
    {
        // 1
        GameObject laser = game.GetShip().SpawnLaser();
        // 2
        float initialYPos = laser.transform.position.y;
        yield return new WaitForSeconds(0.1f);
        // 3
        Assert.Greater(laser.transform.position.y, initialYPos);
    }

    [UnityTest]
    public IEnumerator LaserDestroysAsteroid()
    {
        // 1
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        // 2
        UnityEngine.Assertions.Assert.IsNull(asteroid);
    }

    [UnityTest]
    public IEnumerator DestroyedAsteroidRaisesScore()
    {
        // 1
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        // 2
        Assert.AreEqual(game.score, 1);
    }

    [UnityTest]
    public IEnumerator PlayerLeftMovement()
    {
        Vector2 oldPlayerPos = game.GetShip().transform.position;

        game.GetShip().MoveLeft();

        Vector2 newPlayerPos = game.GetShip().transform.position;

        yield return new WaitForSeconds(0.1f);

        Assert.Less(newPlayerPos.x, oldPlayerPos.x);
    }

    [UnityTest]
    public IEnumerator PlayerRightMovement()
    {
        Vector2 oldPlayerPos = game.GetShip().transform.position;

        game.GetShip().MoveRight();

        Vector2 newPlayerPos = game.GetShip().transform.position;

        yield return new WaitForSeconds(0.1f);

        Assert.Greater(newPlayerPos.x, oldPlayerPos.x);
    }


    [UnityTest]
    public IEnumerator ScoreResetsOnReset()
    {
        game.score = 5;

        game.NewGame();

        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(game.score, 0);

    }

    [UnityTest]
    public IEnumerator PlayerUpMovement()
    {
        Vector2 oldPlayerPos = game.GetShip().transform.position;

        game.GetShip().MoveUp();

        Vector2 newPlayerPos = game.GetShip().transform.position;

        yield return new WaitForSeconds(0.1f);

        Assert.Greater(newPlayerPos.y, oldPlayerPos.y);
    }

    [UnityTest]
    public IEnumerator PlayerDownMovement()
    {
        Vector2 oldPlayerPos = game.GetShip().transform.position;

        game.GetShip().MoveDown();

        Vector2 newPlayerPos = game.GetShip().transform.position;

        yield return new WaitForSeconds(0.1f);

        Assert.Less(newPlayerPos.y, oldPlayerPos.y);
    }

    [UnityTest]
    public IEnumerator PlayerMovementBounds()
    {
        game.GetShip().MoveDown();
        game.GetShip().MoveDown();
        game.GetShip().MoveDown();
        game.GetShip().MoveDown();

        Vector2 newPlayerPos = game.GetShip().transform.position;

        yield return new WaitForSeconds(0.1f);

        Assert.GreaterOrEqual(newPlayerPos.y, game.GetShip().minTop);
    }

	[UnityTest]
    public IEnumerator SpawnAndMovePickup()
    {
        GameObject pickup = game.GetSpawner().SpawnBonusPickup();
        float initialYPos = pickup.transform.position.y;
        yield return new WaitForSeconds(0.1f);

        Assert.Less(pickup.transform.position.y, initialYPos);
    }

    [UnityTest]
    public IEnumerator PickupScoreAdd()
    {

        GameObject pickup = game.GetSpawner().SpawnBonusPickup();
        pickup.transform.position = Vector3.zero;
        GameObject player = game.GetShip().gameObject;
        player.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(game.score, 10);
    }

    [UnityTest]
    public IEnumerator PickupDeletedOffscreen()
    {

        GameObject pickup = game.GetSpawner().SpawnBonusPickup();
        pickup.transform.position = new Vector3( 0,-6.0f,0);
        yield return new WaitForSeconds(0.1f);

        UnityEngine.Assertions.Assert.IsNull(pickup);
    }

    [UnityTest]
    public IEnumerator SpawnAndMoveBomb()
    {
        GameObject bomb = game.GetSpawner().SpawnBombPickup();
        float initialYPos = bomb.transform.position.y;
        yield return new WaitForSeconds(0.1f);

        Assert.Less(bomb.transform.position.y, initialYPos);
    }





}