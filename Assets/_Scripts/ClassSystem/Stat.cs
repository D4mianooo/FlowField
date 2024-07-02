using System;

public class Stat {
    public event EventHandler<float> OnStatValueChanged;
    
    public float Value { get; private set; }
    
    public Stat(float value) {
        Value = value;
    }
    public void Increase(float value) {
        Value += value;
        OnStatValueChanged?.Invoke(this, Value);
    }
}
