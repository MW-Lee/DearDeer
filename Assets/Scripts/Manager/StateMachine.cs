////////////////////////////////////////////
//
// StateMachine
//
// 상태에 관련된 최상위 개념을 탑재하고 정의하는 스크립트
// 20. 09. 03
////////////////////////////////////////////
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 상태를 나타내는 최상위 인터페이스
/// </summary>
public interface IState
{
    /// <summary>
    /// 해당 상태에 돌입할 때 작동하는 함수
    /// </summary>
    void OperatorEnter();
    /// <summary>
    /// 해당 상태에서 계속 작동하는 함수
    /// </summary>
    void OperatorUpdate();
    /// <summary>
    /// 해당 상태를 나올 때 작동하는 함수
    /// </summary>
    void OperatorExit();
}

/// <summary>
/// 유한 상태 머신
/// </summary>
/// <typeparam name="T">사용하고자 하는 객체 클래스</typeparam>
public class StateMachine<T>
{
    // 현재 상태를 저장
    public IState CurrentState;

    // 사용하는 객체가 필요한 상태를 저장해놓는 Dictionary
    public Dictionary<string, IState> StateDictionary;

    /// <summary>
    /// 상태머신 생성자.
    /// </summary>
    /// <param name="_state">돌입하고자 하는 초기 상태를 기입</param>
    public StateMachine(IState _state)
    {
        StateDictionary = new Dictionary<string, IState>();

        _state.OperatorEnter();
        CurrentState = _state;
    }

    /// <summary>
    /// 상태머신의 Dictionary에 새로운 상태를 추가
    /// </summary>
    /// <param name="_key">호출할 키 값</param>
    /// <param name="_state">저장할 상태</param>
    public void AddState(string _key, IState _state)
    {
        StateDictionary.Add(_key, _state);
    }

    /// <summary>
    /// 현재 상태를 바꿔주는 함수
    /// </summary>
    /// <param name="_state">바꾸고자 하는 상태</param>
    public void SetState(string _state)
    {
        // 바꾸고자 하는 새로운 상태
        IState newState = StateDictionary[_state];

        // 만약 현재 상태와 같다면 바꾸지 않음
        if (CurrentState == newState)
        {
            Debug.Log("Not Change : Current state is Same" + CurrentState.ToString());
            return;
        }

        // 현재 상태를 종료
        CurrentState.OperatorExit();
        // 현재 상태를 변경
        CurrentState = newState;
        // 현재 상태에 돌입
        CurrentState.OperatorEnter();
    }

    /// <summary>
    /// 상태 머신이 가지고 있는 상태를 가져옴
    /// 찾고자 하는 함수가 존재하지 않으면 null 반환
    /// </summary>
    /// <param name="_state">찾고자 하는 상태 키 값</param>
    /// <returns>찾는 상태</returns>
    public IState GetState(string _state)
    {
        // 찾고자 하는 상태가 존재하지 않는 경우 null값 반환
        if (StateDictionary[_state] == null)
        {
            Debug.Log("That state is not exist");
            return null;
        }
        else
            return StateDictionary[_state];
    }
}