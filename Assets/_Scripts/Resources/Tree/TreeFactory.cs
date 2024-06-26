using System;
using System.Collections.Generic;

public class TreeFactory {
    private List<TreeScriptableObject> _trees;
    public TreeFactory(List<TreeScriptableObject> trees) {
        _trees = trees;
    }
    public List<Tree> Manufacture(int quantity) {
        List <Tree> trees = new List<Tree>();
        for (int i = 0; i < quantity; i++) {
            int r = Randomizer.Singleton().Next(_trees.Count);
            Tree tree = new Tree(_trees[r]);
            trees.Add(tree);
        }

        return trees;
    }
}
