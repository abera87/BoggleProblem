/*
using System;
using System.Linq;
public class Simple {
  public static void Main() {
    string[] names = { "Burke", "Laptop", "Computer", 
                       "Mobile", "Ahemed", "Sania", 
                       "Kungada", "David","United","Sinshia" };

    var  query = from s in names 
                               where s.Length == 7
                               orderby s
                               select s.ToUpper();

    //foreach (string item in query)
      //Console.WriteLine(item);
	  
	  TrieNode tn=new TrieNode();
	  Build_TRIE("ANI",tn);
  }
	static TrieNode Build_TRIE(string text,TrieNode tn){
		Console.WriteLine(text);
		if(text.Length>0){
			int index = text[0] -'A';
			if(tn.childs[index]==null){
				TrieNode tempTn=new TrieNode();
				tempTn.name=text[0];
				tn.childs[index]=tempTn;
				if(text.Length==1){
					tempTn.leafNode=true;
					return tempTn;
				}
				Build_TRIE(text.Substring(1,text.Length-1),tempTn);
				//return tempTn;
			}
			else{
				Build_TRIE(text.Substring(1,text.Length-1),tn.childs[index]);
			}
			
			// for next character
				//Build_TRIE(text.Substring(1,text.Length-1),tn);
		}
		
		return tn;
	}
}

public class TrieNode{
public char name;
public TrieNode[] childs;
public Boolean leafNode;
	
	public TrieNode(){
		this.name = ' ';
		childs = new TrieNode[26];
	}
}


 */