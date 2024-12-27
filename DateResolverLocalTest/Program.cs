using DateResolver;



string test1 = "file-NOW.txt";
string result1 = DateKeywordResolver.Resolve(test1);
Console.WriteLine($"Test 1: {result1}");

string test2 = "backup-NOW-1d.zip";
string result2 = DateKeywordResolver.Resolve(test2);
Console.WriteLine($"Test 2: {result2}");

string test3 = "log-Format(NOW, \"yyyy-MM-dd\").txt";
string result3 = DateKeywordResolver.Resolve(test3);
Console.WriteLine($"Test 3: {result3}");
