using System;

namespace BoggleProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string[] inputText = { "aaey,rrum,tgmn,ball", "all,ball,mur,raeymni,rumk,tall,true,trum,yes" };
            /*
            aaey
            rrum
            tgmn
            ball
             */
            char[,] bg = BuildBoggle(inputText[0]);

            TrieNode root = new TrieNode();
            // build Trie from inoutText
            foreach (var item in inputText[1].Split(','))
            {
                Build_TRIE(item.ToUpper(), root);
            }

            // Build_TRIE("ANIMESH",tn);
            /*
            // print boggle
            for (int i = 0;i<4; i++)
            {
                for (int j = 0; j < 4; j ++)
                {
                    Console.Write(bg[i, j]);
                }
                Console.Write('\n');
            }
            */

            Boolean[,] visited = new Boolean[inputText[0].Split(',')[0].Length, inputText[0].Length];
            SearchMatchedWord(bg, root, visited);
        }

        private static void SearchMatchedWord(char[,] boggle, TrieNode root, bool[,] visited)
        {
            int bgR = boggle.GetLength(0);
            int bgC = boggle.GetLength(1);
            string wordFromBoggle = "";
            // build word from boggle and find in Trie
            for (int i = 0; i < bgR; i++)
            {
                for (int j = 0; j < bgC; j++)
                {

                    // find in Trie
                    int position = boggle[i, j] - 'A';
                    if (root.childs[position] != null)// character available in Trie
                    {
                        wordFromBoggle = wordFromBoggle + boggle[i, j];// append character in wordbuilder to display
                        FindWordInTrie(root.childs[position], boggle, visited, wordFromBoggle, i, j);

                         wordFromBoggle = "";
                    }

                }
            }
        }

        static void FindWordInTrie(TrieNode trieNode, char[,] boggle, Boolean[,] visited,
                                    string wordFromBoggle, int bgR, int bgC)
        {
            if (trieNode.leafNode)
            {
                Console.WriteLine(wordFromBoggle);
            }
            if (IsSafeCell(boggle.GetLength(0), boggle.GetLength(1), bgC, bgR, visited))
            {
                visited[bgR, bgC] = true;

                // loop through all childs of current node
                for (int child = 0; child < 26; child++)// a node can have maximum 26 childs because we have only 26 letter
                {
                    char currentChar = ' ';
                    if (trieNode.childs[child] != null)
                    {
                        currentChar = trieNode.childs[child].name;

                        //check all adjecent 8 cell of boggle match with current char value also check cell is not visited (issafe)
                        /* position respect to start(*)
                        *-
                        */
                        if (IsSafeCell(boggle.GetLength(0), boggle.GetLength(1), bgC + 1, bgR, visited) && boggle[bgR, bgC + 1] == currentChar)
                            FindWordInTrie(trieNode.childs[child], boggle, visited, wordFromBoggle + currentChar, bgR, bgC + 1);

                        /* position respect to start(*)
                        *\
                        */
                        if (IsSafeCell(boggle.GetLength(0), boggle.GetLength(1), bgC + 1, bgR + 1, visited) && boggle[bgR + 1, bgC + 1] == currentChar)
                            FindWordInTrie(trieNode.childs[child], boggle, visited, wordFromBoggle + currentChar, bgR + 1, bgC + 1);

                        /* position respect to start(*)
                        *|
                        */
                        if (IsSafeCell(boggle.GetLength(0), boggle.GetLength(1), bgC, bgR + 1, visited) && boggle[bgR + 1, bgC] == currentChar)
                            FindWordInTrie(trieNode.childs[child], boggle, visited, wordFromBoggle + currentChar, bgR + 1, bgC);

                        /* position respect to start(*)
                        `/*
                        */
                        if (IsSafeCell(boggle.GetLength(0), boggle.GetLength(1), bgC - 1, bgR + 1, visited) && boggle[bgR + 1, bgC - 1] == currentChar)
                            FindWordInTrie(trieNode.childs[child], boggle, visited, wordFromBoggle + currentChar, bgR + 1, bgC - 1);

                        /* position respect to start(*)
                        -*
                        */
                        if (IsSafeCell(boggle.GetLength(0), boggle.GetLength(1), bgC-1 , bgR, visited) && boggle[bgR, bgC-1] == currentChar)
                            FindWordInTrie(trieNode.childs[child], boggle, visited, wordFromBoggle + currentChar, bgR, bgC-1);

                        /* position respect to start(*)
                        \*
                        */
                        if (IsSafeCell(boggle.GetLength(0), boggle.GetLength(1), bgC - 1, bgR - 1, visited) && boggle[bgR - 1, bgC - 1] == currentChar)
                            FindWordInTrie(trieNode.childs[child], boggle, visited, wordFromBoggle + currentChar, bgR - 1, bgC - 1);

                        /* position respect to start(*)
                        *|
                        */
                        if (IsSafeCell(boggle.GetLength(0), boggle.GetLength(1), bgC, bgR - 1, visited) && boggle[bgR - 1, bgC ] == currentChar)
                            FindWordInTrie(trieNode.childs[child], boggle, visited, wordFromBoggle + currentChar, bgR - 1, bgC );

                        /* position respect to start(*)
                        *`/
                        */
                        if (IsSafeCell(boggle.GetLength(0), boggle.GetLength(1), bgC + 1, bgR - 1, visited) && boggle[bgR - 1, bgC + 1] == currentChar)
                            FindWordInTrie(trieNode.childs[child], boggle, visited, wordFromBoggle + currentChar, bgR - 1, bgC + 1);

                    }
                    /*if (trieNode.childs[boggle[bgR, bgC + 1] - 'A'] != null) // next char available in trie
                    {
                        wordFromBoggle = wordFromBoggle + boggle[bgR, bgC + 1];
                        FindWordInTrie(trieNode.childs[boggle[bgR, bgC + 1] - 'A'], boggle, visited, wordFromBoggle, bgR, bgC + 1);
                    }*/
                }
            }
            // reset cell visited false once completed
            visited[bgR, bgC] = false;
        }

        static bool IsSafeCell(int row, int col, int x, int y, bool[,] visited)
        {
            var value = true;
            if (x >= 0 && x < col && y >= 0 && y < row && !visited[x, y])
                value = true;
            else
                value = false;

            return value;
        }

        static char[,] BuildBoggle(string inputText1)
        {

            char[,] tempBG;
            string[] words = inputText1.Split(',');
            tempBG = new char[words[0].Length, words.Length];
            int i = 0, j = 0;
            foreach (var word in words)
            {
                j = 0;
                foreach (char item in word.ToUpper())
                {
                    tempBG[i, j] = item;
                    j++;
                }
                i++;
            }

            return tempBG;
        }

        static TrieNode Build_TRIE(string text, TrieNode tn)
        {
            //Console.WriteLine(text);
            if (text.Length > 0)
            {
                int index = text[0] - 'A';
                if (tn.childs[index] == null)
                {
                    TrieNode tempTn = new TrieNode();
                    tempTn.name = text[0];
                    tn.childs[index] = tempTn;
                    if (text.Length == 1)
                    {
                        tempTn.leafNode = true;
                        return tempTn;
                    }
                    Build_TRIE(text.Substring(1, text.Length - 1 >= 0 ? text.Length - 1 : 1), tempTn);
                    //return tempTn;
                }
                else
                {
                    Build_TRIE(text.Substring(1, text.Length - 1 >= 0 ? text.Length - 1 : 1), tn.childs[index]);
                }
            }

            return tn;
        }
    }
}
