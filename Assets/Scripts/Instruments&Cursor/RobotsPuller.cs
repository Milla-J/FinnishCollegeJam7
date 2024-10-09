using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotsPuller : MonoBehaviour
{
    [SerializeField] private List<Robot> robotsPull;
    public Robot PullRobot()
    {
        UsefulStuff.ShuffleList(robotsPull);
        foreach(var robot in robotsPull)
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
        if (robotsPull.Contains(robot))
        {
            robot.InUse = true;
        }
        else
        {
            Debug.LogWarning("Robot mistaaake!!");
        }
    }
}
