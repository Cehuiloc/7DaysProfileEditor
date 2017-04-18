﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SevenDaysProfileEditor {

    /// <summary>
    /// Collection of utillity methods.
    /// </summary>
    internal class Util {

        /// <summary>
        /// Quicksorts a string array
        /// </summary>
        /// <param name="array">Array to sort</param>
        /// <returns>Sorted array</returns>
        public static string[] Quicksort(string[] array) {
            return Quicksort(array, 0, array.Length);
        }

        public static int ReadAssetInt(BinaryReader reader, char endingChar) {
            List<char> charList = new List<char>();
            char character = reader.ReadChar();

            while (character != endingChar) {
                charList.Add(character);
                character = reader.ReadChar();
            }

            char[] charArray = charList.ToArray();
            string numberString = new string(charArray);

            return int.Parse(numberString);
        }

        public static string ReadAssetString(BinaryReader reader, char endingChar) {
            try
            {
                List<char> charList = new List<char>();
                char character = reader.ReadChar();

                while (character != endingChar) {
                    charList.Add(character);

                    char next = (char)reader.ReadByte();

                    if (Char.IsSurrogate(next))
                    {
                        return "Non-valid string value";
                    }

                    else
                    {
                        character = next;
                    }
                }

                char[] charArray = charList.ToArray();
                return new string(charArray);
            }
            catch (ArgumentException)
            {
                return "Non-valid string value";
            }
        }

        public static string ReadAssetString(BinaryReader reader, int length) {
            try {
                char[] charArray = new char[length];

                for (int i = 0; i < length; i++) {
                    char next = (char)reader.ReadByte();

                    if (Char.IsSurrogate(next)) {
                        return "Non-valid string value";
                    }

                    else {
                        charArray[i] = next;
                    }
                }

                return new string(charArray);
            }

            catch (ArgumentException) {
                return "Non-valid string value";
            }
        }

        private static bool Compare(string smaller, string bigger) {
            smaller += "  ";
            bigger += "  ";

            smaller = smaller.ToLower();
            bigger = bigger.ToLower();

            int i = 0;

            while (true) {
                if (smaller.Length == i && bigger.Length == i) {
                    return true;
                }

                int big = bigger.ElementAt(i);
                int small = smaller.ElementAt(i);

                if (big > small) {
                    return true;
                }
                else if (big < small) {
                    return false;
                }
                else {
                    i++;
                }
            }
        }

        private static int Partition(string[] array, int start, int end) {
            int i = start + 1;
            string piv = array[start];

            for (int j = start + 1; j < end; j++) {
                int length = array.Length;

                if (Compare(array[j], piv)) {
                    Swap(array, i, j);
                    i += 1;
                }
            }

            Swap(array, start, i - 1);
            return i - 1;
        }

        private static string[] Quicksort(string[] array, int start, int end) {
            if (start < end) {
                int pivotPosition = Partition(array, start, end);
                Quicksort(array, start, pivotPosition);
                Quicksort(array, pivotPosition + 1, end);
            }

            return array;
        }

        private static void Swap(string[] array, int one, int two) {
            string temp = array[one];
            array[one] = array[two];
            array[two] = temp;
        }
    }
}