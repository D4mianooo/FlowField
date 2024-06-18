using System;
using System.Collections.Generic;

public class TreeFactory {
    private List<TreeScriptableObject> _trees;
    private static Random rnd;
    public TreeFactory(List<TreeScriptableObject> trees) {
        rnd = new Random();
        _trees = trees;
    }
    public List<Tree> Manufacture(int quantity) {
        List <Tree> trees = new List<Tree>();
        for (int i = 0; i < quantity; i++) {
            int r = rnd.Next(_trees.Count);
            Tree tree = new Tree(_trees[r]);
            trees.Add(tree);
        }

        return trees;
    }
}
