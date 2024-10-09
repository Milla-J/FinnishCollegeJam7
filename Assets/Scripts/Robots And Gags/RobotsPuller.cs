using System.Collections.Generic;
using UnityEngine;

public class RobotsPuller : MonoBehaviour
{
    [SerializeField] private List<Robot> robotsObjects;

    [Header("Things for robots' initialization")]
    [SerializeField] private LabyrinthAssigner _LabyrinthAssigner;
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private Transform _ConveyerStartPoint;
    [SerializeField] private Transform _ConveyerEndPoint;


    private void Start()
    {
        foreach (var robot in robotsObjects)
        {
            robot.Instantiate(_LabyrinthAssigner, _GameManager, _ConveyerStartPoint, _ConveyerEndPoint);
        }
    }

    public Robot GetRobot()
    {
        UsefulStuff.ShuffleList(robotsObjects);
        foreach(var robot in robotsObjects)
        {
            if(!robot.InUse)
            {
                robot.InUse = true;
                robot.OnExitConveyer += AddBackToPool;
                return robot;
            }
        }
        Debug.Log("YOure not supposed to be here!");
        return null;
    }
    private void AddBackToPool(Robot robot)
    {
        if (robotsObjects.Contains(robot))
        {
            robot.InUse = true;
        }
        else
        {
            Debug.LogWarning("Robot mistaaake!!");
        }
    }
}
