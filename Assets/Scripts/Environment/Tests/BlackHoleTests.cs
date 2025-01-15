using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BlackHoleTests  : MonoBehaviour 
{
    private GameObject player;
    private GameObject blackHole1;
    private GameObject blackHole2;

    [SetUp]
    public void SetUp()
    {
        // Создаем игрока
        player = new GameObject("Player");
        player.tag = "Player"; // Добавляем тег "Player"

        // Создаем черные дыры
        blackHole1 = new GameObject("BlackHole1");
        var blackHole1Script = blackHole1.AddComponent<BlackHole>();

        blackHole2 = new GameObject("BlackHole2");
        var blackHole2Script = blackHole2.AddComponent<BlackHole>();

        // Связываем черные дыры
        blackHole1Script.linkedBlackHole = blackHole2.transform;
        blackHole2Script.linkedBlackHole = blackHole1.transform;
    }

    [Test]
    public void TestTeleportation()
    {
        // Устанавливаем позицию игрока рядом с первой черной дырой
        player.transform.position = blackHole1.transform.position;

        // Вызываем телепортацию через вспомогательный метод
        blackHole1.GetComponent<BlackHole>().TeleportObject(player);

        // Проверяем, что игрок переместился ко второй черной дыре
        Assert.AreEqual(blackHole2.transform.position, player.transform.position, "Игрок не был телепортирован ко второй черной дыре.");
    }

    [Test]
    public void TestNonPlayerInteraction()
    {
        // Создаем объект без тега "Player"
        var nonPlayerObject = new GameObject("NonPlayer");

        // Устанавливаем позицию объекта рядом с первой черной дырой
        nonPlayerObject.transform.position = blackHole1.transform.position;

        // Вызываем телепортацию через вспомогательный метод
        blackHole1.GetComponent<BlackHole>().TeleportObject(nonPlayerObject);

        // Проверяем, что объект не был телепортирован (логика может быть расширена, если нужно)
        Assert.AreNotEqual(blackHole2.transform.position, nonPlayerObject.transform.position, "Объект без тега 'Player' был телепортирован.");
    }

    [TearDown]
    public void TearDown()
    {
        // Удаляем все объекты после тестов
        Object.Destroy(player);
        Object.Destroy(blackHole1);
        Object.Destroy(blackHole2);
    }
}
