using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClienEventManager
{
    private Dictionary<int, Delegate> dicEvents = new Dictionary<int, Delegate>();
    private void LogTypeError(EventID eventId, HandleType handleType, Delegate targetEventType, Delegate listener)
    {
        Debug.LogError(string.Format("EventID {0},[{1}]Worning Listener Type  {2},needed Type  {3}.", eventId.ToString(), ClientEventSystemDefine.dicHandleType[(int)handleType], targetEventType.GetType(), listener.GetType()));
    }
    private bool CheckAddEventListener(EventID eventID, Delegate listener)
    {
        if (!this.dicEvents.ContainsKey((int)eventID)) this.dicEvents.Add((int)eventID, null);
        Delegate tempDele = this.dicEvents[(int)eventID];
        if (tempDele != null && tempDele.GetType() != listener.GetType())
        {
            LogTypeError(eventID, HandleType.Add, dicEvents[(int)eventID], listener);
            return false;
        }
        return true;
    }

    private bool CheckRemoveEventListener(EventID eventID, Delegate listener)
    {
        if (!dicEvents.ContainsKey((int)eventID)) return false;
        Delegate tempDele = dicEvents[(int)eventID];
        if (tempDele != null && tempDele.GetType() != listener.GetType())
        {
            LogTypeError(eventID, HandleType.Add, dicEvents[(int)eventID], listener);
            return false;
        }
        return true;
    }

    #region  无参数
    public void AddEventListener(EventID eventID, Action listener)
    {
        if (CheckAddEventListener(eventID, listener))
        {
            Delegate del = dicEvents[(int)eventID];
            dicEvents[(int)eventID] = (Action)Delegate.Combine((Action)del, listener);
        }
    }

    public void RemoveEventListener(EventID eventID, Action listener)
    {
        if (CheckRemoveEventListener(eventID, listener))
        {
            Delegate del = dicEvents[(int)eventID];
            dicEvents[(int)eventID] = Delegate.Remove(del, listener);
        }
    }

    public void TriggerEvent(EventID eventID)
    {
        Delegate del = null;
        if (dicEvents.TryGetValue((int)eventID, out del))
        {
            if (del == null)
            {
                return;
            }
            Delegate[] triggerLst = del.GetInvocationList();
            for (int i = 0; i < triggerLst.Length; i++)
            {
                Action action = (Action)triggerLst[i];
                if (action == null)
                {
                    Debug.LogErrorFormat("Trigger Event {0} Parameters type [void] are not match target type {1}", eventID.ToString(), triggerLst[i].GetType());
                    return;
                }
                action();
            }
        }
    }
    #endregion

    #region 一个参数

    public void AddEventListener<T>(EventID eventID, Action<T> listener)
    {
        if (CheckAddEventListener(eventID, listener))
        {
            Delegate del = dicEvents[(int)eventID];
            dicEvents[(int)eventID] = (Action<T>)Delegate.Combine((Action<T>)del, listener);
        }
    }

    public void RemoveEventListener<T>(EventID eventID, Action<T> listener)
    {
        if (CheckRemoveEventListener(eventID, listener))
        {
            Delegate del = dicEvents[(int)eventID];
            dicEvents[(int)eventID] = Delegate.Remove(del, listener);
        }
    }

    public void TriggerEvent<T>(EventID eventID, T p)
    {
        Delegate del = null;
        if (dicEvents.TryGetValue((int)eventID, out del))
        {
            if (del == null) return;
            Delegate[] triggerLst = del.GetInvocationList();
            for (int i = 0; i < triggerLst.Length; i++)
            {
                Action<T> action = triggerLst[i] as Action<T>;
                if (action == null)
                {
                    Debug.LogErrorFormat("Trigger Event{0} Parameters type[{1}] are not match target type {2}", eventID.ToString(), p.GetType(), triggerLst[i].GetType());
                    return;
                }
                action(p);
            }
        }
    }
    #endregion

    #region 两个参数
    public void AddEventListener<T0, T1>(EventID eventID, Action<T0, T1> listener)
    {
        if (CheckAddEventListener(eventID, listener))
        {
            Delegate del = dicEvents[(int)eventID];
            dicEvents[(int)eventID] = (Action<T0, T1>)Delegate.Combine((Action < T0, T1 >) del, listener);
        }
    }

    public void RemoveEventListener<T0, T1>(EventID eventID, Action<T0, T1> listener)
    {
        if (CheckRemoveEventListener(eventID, listener))
        {
            Delegate del = dicEvents[(int)eventID];
            dicEvents[(int)eventID] = Delegate.Remove(del, listener);
        }
    }

    public void TriggerEvent<T0, T1>(EventID eventID, T0 p0, T1 p1)
    {
        Delegate del = null;
        if (dicEvents.TryGetValue((int)eventID, out del))
        {
            if (del == null)
            {
                return;
            }
            Delegate[] triggerLst = del.GetInvocationList();
            for (int i = 0; i < triggerLst.Length; i++)
            {
                Action<T0, T1> action = triggerLst[i] as Action<T0, T1>;
                if(action == null)
                {
                    Debug.LogErrorFormat("Trigger Event {0} Parameters type[{1},{2}] are not match target type {3}", eventID.ToString(),p0.GetType(),p1.GetType(), triggerLst[i].GetType());
                    return;
                }
                action(p0, p1);
            }
        }
    }
    #endregion

    #region 三个参数
    public void AddEventListener<T0, T1, T2>(EventID eventID, Action<T0, T1, T2> listener)
    {
        if (CheckAddEventListener(eventID, listener))
        {
            Delegate del = dicEvents[(int)eventID];
            dicEvents[(int)eventID] = (Action<T0, T1, T2>)Delegate.Combine((Action < T0, T1, T2 >) del, listener);
        }
    }

    public void RemoveEventListener<T0, T1, T2>(EventID eventID, Action<T0, T1, T2> listener)
    {
        if (CheckRemoveEventListener(eventID, listener))
        {
            Delegate del = dicEvents[(int)eventID];
            dicEvents[(int)eventID] = Delegate.Remove(del, listener);
        }
    }

    public void TriggerEvent<T0, T1, T2>(EventID eventID, T0 p0, T1 p1, T2 p2)
    {
        Delegate del = null;
        if (dicEvents.TryGetValue((int)eventID, out del))
        {
            if (del == null) return;
            Delegate[] triggerLst = del.GetInvocationList();
            for(int i =0;i< triggerLst.Length;i++)
            {
                Action<T0, T1, T2> action = triggerLst[i] as Action<T0, T1, T2>;
                if (action == null)
                {
                    Debug.LogErrorFormat("Trigger Event {0} Parameters type[{1},{2},{3}] are not match target type {4}", eventID.ToString(), p0.GetType(), p1.GetType(), p2.GetType(), triggerLst[i].GetType());
                    return;
                }
                action(p0, p1, p2);
            }
        }
    }
    #endregion

    #region 四个参数
    public void AddEventListener<T0, T1, T2, T3>(EventID eventID, Action<T0, T1, T2, T3> listener)
    {
        if (CheckAddEventListener(eventID, listener))
        {
            Delegate del = dicEvents[(int)eventID];
            dicEvents[(int)eventID] = (Action<T0, T1, T2, T3>)Delegate.Combine((Action<T0, T1, T2, T3>)del, listener);
        }
    }

    public void RemoveEventListener<T0, T1, T2, T3>(EventID eventID, Action<T0, T1, T2, T3> listener)
    {
        if (CheckRemoveEventListener(eventID, listener))
        {
            Delegate del = dicEvents[(int)eventID];
            dicEvents[(int)eventID] = Delegate.Remove(del, listener);
        }
    }

    public void TriggerEvvent<T0, T1, T2, T3>(EventID eventID, T0 p0, T1 p1, T2 p2, T3 p3)
    {
        Delegate del = null;
        if (dicEvents.TryGetValue((int)eventID, out del))
        {
            if (del == null) return;
            Delegate[] triggerLst = del.GetInvocationList();
            for (int i = 0; i < triggerLst.Length; i++)
            {
                Action<T0, T1, T2, T3> action = triggerLst[i] as Action<T0, T1, T2, T3>;
                if (action == null)
                {
                    Debug.LogErrorFormat("Trigger Event {0} Parameters type[{1},{2},{3},{4}] are not match target type {5}", eventID.ToString(), p0.GetType(), p1.GetType(), p2.GetType(), p3.GetType(), triggerLst[i].GetType());
                    return;
                }
                action(p0, p1, p2, p3);
            }
        }
    }
    #endregion
}
