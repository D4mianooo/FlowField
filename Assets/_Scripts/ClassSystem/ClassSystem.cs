using System;

[Serializable]
public class ClassSystem {
    public ClassSystem(ClassScriptableObject classSO) {
        Health = new Stat(classSO._health);
        AttackDamage = new Stat(classSO._attackDamage);
        Speed = new Stat(classSO._speed);
    }
    public Stat Health { get; private set; }
    public Stat AttackDamage { get; private set; }
    public Stat Speed { get; private set; }
    
}
