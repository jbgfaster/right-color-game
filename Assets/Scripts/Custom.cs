using UnityEngine;
using UnityEngine.Events;

public class Custom
{
    
}

[System.Serializable]
public class CustomEventInt : UnityEvent<int>
{
    
}

public interface IEventInt
{
    public CustomEventInt GetDestroyEvent();
}

public interface ISetColor
{
    public void SetColor(Material materialColor, bool value = true);
}