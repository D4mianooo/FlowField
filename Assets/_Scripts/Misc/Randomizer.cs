using System;

public static class Randomizer {
    private static Random _rnd;

    public static Random Singleton() {
        if (_rnd == null) {
            _rnd = new Random();
        }
        return _rnd;
    }
}
