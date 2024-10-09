using System.Collections.Generic;
using UnityEngine;

public class RobotsPuller : MonoBehaviour
{
    [SerializeField] private List<Robot> robotsObjects;

    [Header("Things for robots' initialization")]
    [SerializeField] private LabyrinthPuller _LabyrinthAssigner;
    [SerializeField] private GameplayLoopManager _GameplayLoopManager;
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private PopUpManager _PopUpManager;
    [SerializeField] private Transform _ConveyerStartPoint;
    [SerializeField] private Transform _ConveyerEndPoint;


    private void Start()
    {
        foreach (var robot in robotsObjects)
        {
            robot.Instantiate(_GameplayLoopManager, _LabyrinthAssigner, _PopUpManager, _ConveyerStartPoint, _ConveyerEndPoint);
        }
    }

    public bool TryToGetRobot(out Robot newRobot)
    {
        UsefulStuff.ShuffleList(robotsObjects);
        foreach(var robot in robotsObjects)
        {
            if(!robot.InUse)
            {
                robot.InUse = true;
                robot.OnExitConveyer += AddBackToPool;
                newRobot = robot;
                return true;
            }
        }
        Debug.Log("YOure not supposed to be here!");
        newRobot = null;
        return false;
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
