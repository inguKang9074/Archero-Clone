using UnityEngine;
using System.Collections;

// FSM 베이스 클래스
public abstract class State<T>
{
    protected T character;

    // 해당 상태 진입시
    public virtual void OnEnter(T target)
    {
        this.character = target;
    }

    // 해당 상태일때
    public abstract void Update();

    // 상태 종료시
    public abstract void OnExit();
}
