using System.Collections;
using System.Collections.Generic;
using System;

public class EventRuter<T>
{
    private Dictionary<T, Delegate> mEventTable = new Dictionary<T, Delegate>();

    private bool OnAdd(T type , Delegate handle)
    {
        bool flag = true;

        if (!mEventTable.ContainsKey(type)) mEventTable.Add(type, null);

        Delegate d = mEventTable[type];

        if (d != null && d.GetType() != handle.GetType()) flag = false;

        return flag;
    }

    private bool OnRemove(T type, Delegate handle)
    {
        bool flag = true;

        if (mEventTable.ContainsKey(type) == false) return false;

        Delegate d = mEventTable[type];

        if (d != null && d.GetType() != handle.GetType())
        {
            flag = false;
        }

        return flag;
    }

    private bool OnDispatch(T type)
    {
        return this.mEventTable.ContainsKey(type);
    }

    public void On(T type , Delegate handle)
    {
        if(this.OnAdd(type , handle))
        {
            var action = (Action)this.mEventTable[type];

            action = (Action)Delegate.Combine(action, handle);

            this.mEventTable[type] = action;
        }
    }

    public void On<T1>(T type ,Action<T1> handle)
    {
        if (OnAdd(type, handle))
        {
            var action = (Action<T1>)this.mEventTable[type];

            action = (Action<T1>)Delegate.Combine(action, handle);

            mEventTable[type] = action;
        }
    }

    public void Off(T type, Action handle)
    {
        if(this.OnRemove(type , handle))
        {
            var action = (Action)this.mEventTable[type];

            action = (Action)Delegate.Remove(action, handle);

            mEventTable[type] = action;
        }
    }

    public void Off<T1>(T type , Action<T1> handle)
    {
        if (this.OnRemove(type, handle))
        {
            var action = (Action<T1>)this.mEventTable[type];

            action = (Action<T1>)Delegate.Remove(action, handle);

            mEventTable[type] = action;
        }
    }


    public void DisPatch(T type)
    {
        if (this.OnDispatch(type))
        {
            var action = (Action)mEventTable[type];

            if(action != null)
            {
                action.Invoke();
            }
        }
    }

    public void DisPatch<T1>(T type,T1 args)
    {
        if (this.OnDispatch(type))
        {
            var action = (Action<T1>)mEventTable[type];

            if (action != null)
            {
                action(args);
            }
        }
    }

}
