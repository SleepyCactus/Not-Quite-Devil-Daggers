using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowStates
{
    public StateMachine Owner;
}

public class BeginMenuState : GameFlowStates, IState
{
    public BeginMenuState(StateMachine _owner)
    {
        Owner = _owner;
        GameManager.Instance.SetupMenu();
    }

    public void Enter()
    {
        Debug.Log("Entered " + this.ToString());
    }

    public void Execute()
    {
        Debug.Log("Executed " + this.ToString());
    }

    public void Exit()
    {
        Debug.Log("Exited " + this.ToString());

    }
}

public class BeginGameState : GameFlowStates , IState
{
    public BeginGameState(StateMachine _owner)
    {
        Owner = _owner;
        GameManager.Instance.SetupGame();
    }

    public void Enter()
    {
        Debug.Log("Entered " + this.ToString());
    }

    public void Execute()
    {
        Debug.Log("Executed " + this.ToString());
        IState nextState = new UpdateGameState(Owner);
        Owner.ChangeState(nextState);
    }

    public void Exit()
    {
        Debug.Log("Exited " + this.ToString());
        
    }
}

public class UpdateGameState : GameFlowStates, IState
{
    float time = 0;
    public UpdateGameState(StateMachine _owner)
    {
        Owner = _owner;
    }
    public void Enter()
    {
        Debug.Log("Entered " + this.ToString());
    }

    public void Execute()
    {
        
        Debug.Log("Executed " + this.ToString());
        GameManager.Instance.CurrentTime += Time.deltaTime;
        time += Time.deltaTime;
        if(time >= 2)
        {
            time = 0;
            GameManager.Instance.EnemyManager.SpawnEnemy(GameManager.Instance.EnemyManager.RandomPointOnCircleEdge(60));
        }
        if(GameManager.Instance.CurrentTime >= GameManager.Instance.GameWinTime)
        {
            IState nextState = new WinGameState(Owner);
            Owner.ChangeState(nextState);
        }
        
    }

    public void Exit()
    {
        Debug.Log("Exited " + this.ToString());

    }
}

public class LoseGameState : GameFlowStates, IState
{
    public LoseGameState(StateMachine _owner)
    {
        Owner = _owner;
    }
    public void Enter()
    {
        Debug.Log("Entered " + this.ToString());
    }

    public void Execute()
    {
        Debug.Log("Executed " + this.ToString());
        if(Input.GetMouseButtonDown(0))
        {
            IState nextState = new BeginMenuState(Owner);
            Owner.ChangeState(nextState);
        }
        
    }

    public void Exit()
    {
        Debug.Log("Exited " + this.ToString());
    }
}

public class WinGameState : GameFlowStates, IState
{
    public WinGameState(StateMachine _owner)
    {
        Owner = _owner;
    }
    public void Enter()
    {
        Debug.Log("Entered " + this.ToString());
        GameManager.Instance.EndGame(false);

    }

    public void Execute()
    {
        Debug.Log("Executed " + this.ToString());
        if (Input.GetMouseButtonDown(0))
        {
            IState nextState = new BeginMenuState(Owner);
            Owner.ChangeState(nextState);
        }
    }

    public void Exit()
    {
        Debug.Log("Exited " + this.ToString());
    }
}
