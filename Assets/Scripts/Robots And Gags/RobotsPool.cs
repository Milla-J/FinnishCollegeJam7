using System.Collections.Generic;
using UnityEngine;

public class RobotsPool : MonoBehaviour
{
    [SerializeField] private List<Robot> pool;

    [Header("Things for robots' initialization")]
    [SerializeField] private GameplayLoopController _GameplayLoopManager;
    [SerializeField] private LabyrinthPool _LabyrinthAssigner;
    [SerializeField] private PopUpController _PopUpManager;
    [SerializeField] private Transform _ConveyerStartPoint;
    [SerializeField] private Transform _ConveyerEndPoint;

    public int PoolCount
    { 
        get => _poolCount;
    }

    private int _poolCount;
    private int _maxPooled;

    public void Initialize(int maxRobots)
    {
        foreach (var robot in pool)
        {
            robot.Instantiate(_GameplayLoopManager, _LabyrinthAssigner, _PopUpManager, _ConveyerStartPoint, _ConveyerEndPoint);
        }
        _maxPooled = maxRobots;

    }

    public bool TryToGetRobot(out Robot newRobot)
    {
        if (_poolCount >= _maxPooled)
        {
            newRobot = null;
            return false;
        }

        UsefulStuff.ShuffleList(pool);
        foreach(var robot in pool)
        {
            if(!robot.InUse)
            {
                robot.InUse = true;
                robot.OnExitConveyer += AddBackToPool;
                newRobot = robot;
                _poolCount++;
                return true;
            }
        }
        Debug.Log("YOure not supposed to be here!");
        newRobot = null;
        return false;
    }

    private void AddBackToPool(Robot robot)
    {
        if (pool.Contains(robot))
        {
            robot.InUse = true;
        }
        else
        {
            Debug.LogWarning("Robot mistaaake!!");
        }
    }
}
