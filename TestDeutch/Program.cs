string[] array1 = { "zwei", "hundert", "zwei"};
string[] array2 = { "zwei", "hundert" };
string[] array3 = {"zwei", "drei"};

HashSet<string> uniqueWords = new HashSet<string>();
List<string> repeatingWords = new List<string>();
List<string> diffList = new List<string>();

foreach (var word in array1)
{
    if (!uniqueWords.Add(word))
    {
        repeatingWords.Add(word);
    }
}

//diffList.Add(array3[1]);
foreach (var word in array1)
{
    
    if (!array2.Contains(word) || repeatingWords.Contains(word))
    {
        diffList.Add(word);
    }
}
string[] diff = diffList.Prepend(array3[1]).ToArray();



System.Console.WriteLine(string.Join(", ", diff));
