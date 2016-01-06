using UnityEngine;
using System.Collections;

public class SubtitleParserScript : MonoBehaviour {

    public enum search { Start, End };

    string cursong = "BardsTheme";


	// Use this for initialization
	void Start () {

       string subtitleData = (Resources.Load("Songs/SubtitleData/" + cursong) as TextAsset).text;
       ArrayList all = breakIntoSentences(subtitleData);

       sentence cur = (sentence)(all[0]);
       word curWord = (word)cur.wordList[0];
       Debug.Log(curWord.text);
	}

    //Returns a list of sentences that contain a list of words

    private ArrayList breakIntoSentences(string songData)
    {
        ArrayList allSentences = new ArrayList();


        string curSentence = "";
        search cur = search.Start;
        for (int i = 0; i < songData.Length; i++)
        {
            char curChar = songData[i];
            if (curChar.Equals('*'))
            {
                if (cur == search.Start)
                {
                    cur = search.End;
                }
                else
                {
                    sentence s = new sentence(curSentence);
                    cur = search.Start;
                    curSentence = "";
                    //Store sentence
                    allSentences.Add(s);
                }
            }
            else
            {
                curSentence += curChar;
            }
        }

        return allSentences;


    }
	
	// Update is called once per frame
	void Update () {
	
	}


    private class sentence
    {
        public int totalWords = 0;
        public ArrayList wordList;
        string data = "";
        public sentence(string data)
        {
            wordList = new ArrayList();
            this.data = data;
         //   Debug.Log("New sentence: " + data);
            parseWords();
        }

        //Break self into a list of words
        private void parseWords()
        {
            string curSentence = "";
            search cur = search.Start;
            for (int i = 0; i < data.Length; i++)
            {
                char curChar = data[i];
                if (curChar.Equals('@'))
                {
                    if (cur == search.Start)
                    {
                        cur = search.End;
                    }
                    else
                    {
                        string spawnTimer = "";
                        for(int j = i+1; j<i+6; j++)
                        {
                            spawnTimer += data[j];
                        }
                        i+=5;
                        cur = search.Start;
                        //Store sentence
 
                        string minuteVal = (spawnTimer[0].ToString() + spawnTimer[1].ToString());
                        string secondVal = (spawnTimer[3].ToString() + spawnTimer[4].ToString());

                        float spawnInSeconds = (float.Parse(minuteVal) * 60) +  float.Parse(secondVal);


                        word w = new word(curSentence, spawnInSeconds);
                        wordList.Add(w);
                        totalWords++;
                       // Debug.Log("New word: " + curSentence);
                       // Debug.Log("Spawn time: " + spawnInSeconds);

                         curSentence = "";
                    }
                }
                else
                {
                    curSentence += curChar;
                }
            }
        }
    }

    private class word
    {
        public string text;
        public float spawnSecond;

        public word(string text, float spawnSecond)
        {
            this.text = text;
            this.spawnSecond = spawnSecond;
        }
    }

}
