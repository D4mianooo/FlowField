using System;

public class ExperienceSystem {
    
    public event EventHandler<float> OnExperienceGained;
    
    private float _currentExperience;
    private float _neededExperience;
    private float _difficultyFactor;

    public ExperienceSystem(float difficultyFactor) {
        _difficultyFactor = difficultyFactor;
        _currentExperience = 0f;
        _neededExperience = 100f;
    }
    
    public void AddExperience(float experience) {
        _currentExperience += experience;
        OnExperienceGained?.Invoke(this, GetNormalizedExperience());
        if (_currentExperience >= _neededExperience) {
            _currentExperience = _currentExperience - _neededExperience;
            _neededExperience *= _difficultyFactor; // DELEGATE
        }
    }
    public float GetNormalizedExperience() {
        return _currentExperience / _neededExperience;
    }
}
