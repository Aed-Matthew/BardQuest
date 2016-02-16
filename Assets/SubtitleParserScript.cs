using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SubtitleParserScript : MonoBehaviour {

    public enum search { Start, End };

    public string cursong = "BardsTheme";

    Text curString;
    ArrayList allSentences;
    
    public void setTime(float t)
    {
        GetComponent<Text>().text = getStringAtTime(t);
    }
	// Use this for initialization
	void Awake () {

       string subtitleData = (Resources.Load("Songs/SubtitleData/" + cursong) as TextAsset).text;
        allSentences = breakIntoSentences(subtitleData);


        /*
         sentence cur = (sentence)(allSentences[0]);
         word curWord = (word)cur.wordList[0];


          foreach(sentence s in allSentences)
          {
              string CIR = "";
              foreach (word w in s.wordList)
              {
                  CIR += w.text;
              }
              Debug.Log(CIR);
          }*/
          
    }


    public string getStringAtTime(float goalSecond)
    {
        //Find the earliest sentence with a 'word' at that time
        //Make that word italicized to mark it
        float foundWordDistance = 100f; //The distance
        sentence foundSentence = null;
        word foundWord = null;

        foreach (sentence s in allSentences)
        {
            foreach (word w in s.wordList)
            {

                    //Seconds is our target time
                    //10 (target) 
                    //10.5 (word)
                    //Finds the closest word to our taget time
                    if (w.spawnSecond > goalSecond)
                {
                    continue;
                }
                    float wordDistance = Mathf.Abs(w.spawnSecond - goalSecond);
                if (wordDistance > 2.0f) { continue; }
                    if (wordDistance < foundWordDistance) {
                        foundWordDistance = wordDistance;
                        foundSentence = s;
                        foundWord = w;

                    }
            }
        }

        string retString = "";
        if ((foundSentence != null) && (foundWord!= null))
        {
            foreach (word w in foundSentence.wordList)
            {
                if (w == foundWord)
                {
                    retString += "<b>";
                    retString += w.text;
                    retString += "</b>";
                }
                else
                {
                    retString += w.text;
                }
            }
        }

        retString = retString.Replace("\n", string.Empty);
        return retString;
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
                        for(int j = i+1; j<i+6+4; j++)
                        {
                            spawnTimer += data[j];
                        }
                        i+=5;
                        cur = search.Start;
                        //Store sentence
 
                        string minuteVal = (spawnTimer[0].ToString() + spawnTimer[1].ToString());
                        string secondVal = (spawnTimer[3].ToString() + spawnTimer[4].ToString());
                        string millisecondVal = ((spawnTimer[6]).ToString() + (spawnTimer[7]).ToString() + (spawnTimer[8]).ToString());
                        //I believe I have to add in the microsecond value
                        float spawnInSeconds = (float.Parse(minuteVal) * 60) +  float.Parse(secondVal) + float.Parse(millisecondVal)/1000f;

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
                    string[] numbers = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", ":" };
                    bool contains = false;
                    foreach(string s in numbers)
                    {
                        if (s.Equals(curChar.ToString()))
                            {
                            contains = true;
                        }
                    }
                    if (!contains)
                    {
                        curSentence += curChar;
                    }
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
