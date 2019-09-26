using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Encoding2
{
	class Program
	{
		static char[] EncodeFragment(char[] encodingArray, int[] permutation) {
			char[] resultBlock = new char[encodingArray.Length];
			for (int i = 0; i < encodingArray.Length; i++)
				resultBlock[permutation[i]] = encodingArray[i];
			return resultBlock;
		}

		static char[] DecodeFragment(char[] decodingArray, int[] permutation) {
			char[] resultBlock = new char[decodingArray.Length];
			for (int i = 0; i < decodingArray.Length; i++)
				resultBlock[i] = decodingArray[permutation[i]];
			return resultBlock;
		}

		static char[] Check(char[] symbols, int n, int readed) {
			if (readed < n)
				for (int i = 0; i < n - readed; i++)
					symbols[symbols.Length - 1 - i] = ' ';
			return symbols;
		}

		static void Decode(int[] permutation) {
			StreamReader fileInput = new StreamReader("encodedText.txt", Encoding.Default);
			StreamWriter fileOutput = new StreamWriter("decodedText.txt", false, Encoding.Default);
			char[] symbols = new char[permutation.Length];
			while (fileInput.Peek() != -1) {
				int count = fileInput.ReadBlock(symbols, 0, symbols.Length);
				Check(symbols, permutation.Length, count);
				fileOutput.Write(DecodeFragment(symbols, permutation));
			}
			fileInput.Close();
			fileOutput.Close();
		}

		static void Encode(int[] permutation) {
			StreamReader fileInput = new StreamReader("sourceText.txt", Encoding.Default);
			StreamWriter fileOutput = new StreamWriter("encodedText.txt", false, Encoding.Default);
			char[] symbols = new char[permutation.Length];
			while (fileInput.Peek() != -1) {
				int count = fileInput.ReadBlock(symbols, 0, symbols.Length);
				Check(symbols, permutation.Length, count);
				fileOutput.Write(EncodeFragment(symbols, permutation));
			}
			fileInput.Close();
			fileOutput.Close();
		}

		static bool FindIndexInPermutation(int[] array, int index) {
			for (int i = 0; i < array.Length; i++)
				if (array[i] == index)
					return true;
			return false;
		}

		static void Main(string[] args)
		{
			Console.Write("Block length = ");
			int n = int.Parse(Console.ReadLine());
			int[] permutation = new int[n];
			for (int i = 0; i < permutation.Length; i++)
				permutation[i] = -1;

			for (int i = 0; i < permutation.Length; i++) {
				Console.Write("{0} -> ", i);
				int position = int.Parse(Console.ReadLine());
				if (position >= permutation.Length || position < 0 || FindIndexInPermutation(permutation, position)) {
					Console.WriteLine("Invalid index");
					i--;
					continue;
				}
				permutation[i] = position;
			}
			Encode(permutation);
			Decode(permutation);
		}
	}
}
