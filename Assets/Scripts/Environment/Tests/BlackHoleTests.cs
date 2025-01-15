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
        // ������� ������
        player = new GameObject("Player");
        player.tag = "Player"; // ��������� ��� "Player"

        // ������� ������ ����
        blackHole1 = new GameObject("BlackHole1");
        var blackHole1Script = blackHole1.AddComponent<BlackHole>();

        blackHole2 = new GameObject("BlackHole2");
        var blackHole2Script = blackHole2.AddComponent<BlackHole>();

        // ��������� ������ ����
        blackHole1Script.linkedBlackHole = blackHole2.transform;
        blackHole2Script.linkedBlackHole = blackHole1.transform;
    }

    [Test]
    public void TestTeleportation()
    {
        // ������������� ������� ������ ����� � ������ ������ �����
        player.transform.position = blackHole1.transform.position;

        // �������� ������������ ����� ��������������� �����
        blackHole1.GetComponent<BlackHole>().TeleportObject(player);

        // ���������, ��� ����� ������������ �� ������ ������ ����
        Assert.AreEqual(blackHole2.transform.position, player.transform.position, "����� �� ��� �������������� �� ������ ������ ����.");
    }

    [Test]
    public void TestNonPlayerInteraction()
    {
        // ������� ������ ��� ���� "Player"
        var nonPlayerObject = new GameObject("NonPlayer");

        // ������������� ������� ������� ����� � ������ ������ �����
        nonPlayerObject.transform.position = blackHole1.transform.position;

        // �������� ������������ ����� ��������������� �����
        blackHole1.GetComponent<BlackHole>().TeleportObject(nonPlayerObject);

        // ���������, ��� ������ �� ��� �������������� (������ ����� ���� ���������, ���� �����)
        Assert.AreNotEqual(blackHole2.transform.position, nonPlayerObject.transform.position, "������ ��� ���� 'Player' ��� ��������������.");
    }

    [TearDown]
    public void TearDown()
    {
        // ������� ��� ������� ����� ������
        Object.Destroy(player);
        Object.Destroy(blackHole1);
        Object.Destroy(blackHole2);
    }
}
