﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Projector.Script
{
    public class RefScriptMaskMatch
    {

        public const int NO_MATCH = 101;
        public const int MAYBE_MATCH = 102;
        public const int PARTIAL_MATCH = 103;
        public const int MATCH = 200;

        private String sourceStr;

        public ReflectionScript parent;

        public static List<String> KeyWords = new List<string>();

        // contains the main sections
        private string[] mainParts;

        // contains a mask to check matching code
        private string[] maskPart;

        // contains what flags muist be assigned.
        private string[] definePart;

        // defines the types of paramaters
        private string[] varDefines;

        public RefScriptMaskMatch(String source, ReflectionScript useMe)
        {
            this.sourceStr = source;
            this.parent = useMe;
            this.buildElements();
        }

        /// <summary>
        /// how many words matching before ends in success
        /// or fail
        /// </summary>
        public int matchingUntil = 0;

        public String lastMatchDefinition = "";

        public int possibleMatch(string[] words)
        {

            if (words.Count() == maskPart.Count())
            {
                if (this.checkFullMatch(words))
                {
                    return Projector.Script.RefScriptMaskMatch.MATCH;
                }                
            }

            else if (words.Count() < maskPart.Count())
            {
                if (this.checkFullMatch(words))
                {
                    return Projector.Script.RefScriptMaskMatch.PARTIAL_MATCH;
                }                
            }


            return Projector.Script.RefScriptMaskMatch.NO_MATCH;
        }


        private Boolean checkFullMatch(string[] words)
        {
            int wordPosition = 0;            

            /*
             * the rules. an Word must match in any case (if in source the word is uppercase then it matches independend from lower or upper case)
             * so mas FUNCTION matches to funtion and Function and so on. But Mask Funtion will only match with Function
             * 
             * 
             * 
             */
            this.matchingUntil = 0;
            foreach (string testWord in words)
            {
                String compareMask = this.maskPart[wordPosition];
                Boolean Hit = false;
                // first case ... words in mask are matching 
                if (testWord == compareMask || testWord.ToUpper() == compareMask)
                {
                    if (!Projector.Script.RefScriptMaskMatch.KeyWords.Contains(testWord))
                    {
                        Projector.Script.RefScriptMaskMatch.KeyWords.Add(testWord);
                    }
                    Hit = true;                    
                }
                else if (compareMask == "?" || compareMask == "§" || compareMask == "%")
                {
                    Hit = true;
                }
                else if (compareMask[0] == '&')
                {
                    Hit = true;

                    string rest = "";
                    if (compareMask.Length > 1)
                    {
                        rest = compareMask.Substring(1);
                        List<string> possibleObjects = this.parent.getCurrentObjectsByType(rest);
                        if (possibleObjects != null)
                        {
                            Hit = possibleObjects.Contains(testWord);
                        }
                    }

                }
                
                if (Hit == false)
                {
                    return false;
                }
                else
                {
                    this.matchingUntil++;
                }
                wordPosition++;
            }
            return true;
        }

        private void buildElements()
        {
            mainParts = this.sourceStr.Split(Projector.Script.ReflectionScript.MASK_DELIMITER[0]);
            this.lastMatchDefinition = mainParts[0];
            maskPart = mainParts[0].Split(' ');
            definePart = mainParts[1].Split(' ');
            varDefines = null;
            if (mainParts.Count() > 2)
            {
                varDefines = mainParts[2].Split(' ');
            }
        }

    }
}
