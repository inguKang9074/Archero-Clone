using UnityEngine;
using System.Collections;

public class StateMachine<T>
{
    // 현재 상태
    public State<T> CurrentState { get; private set; }

    // 상태 변경 함수
    public void SetState(State<T> state, T type)
    {
        if (CurrentState == state)
        {
            Debug.Log("현재 상태" + state);
            return;
        }

        //상태가 바뀌기 전에, 이전 상태의 Exit를 호출한다.
        if (CurrentState != null)
            CurrentState.OnExit();

        //상태 교체.
        CurrentState = state;

        //새 상태의 Enter를 호출한다.
        CurrentState.OnEnter(type);
    }

    // 매프레임마다 호출되는 함수.
    public void DoOperateUpdate()
    {
        if (CurrentState == null)
            return;

        CurrentState.Update();
    }
}
