using System;
public class TrieNode
{
    public char name;
    public TrieNode[] childs;
    public Boolean leafNode;

    public TrieNode()
    {
        this.name = ' ';
        childs = new TrieNode[26];
    }
}