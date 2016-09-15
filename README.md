# CalorieCounter
Simple nutritional information software developed in C# and WinForms, using WolframAlpha.NET API to retrieve information about foods

Software will pull data from wolfram alpha and stores it in local database, to reduce number of pulls. It has option to add multiple people and counts full nutritional information including macro(Protein, Fats...) and micro nutrients(Vitamins and minerals), and calculates weekly averages by percenntage based on foods


Too start working you need to add your own WolframAlphaAPI key and put it in Search.cs on line 69. You can get WolframAlpha  API here 
http://products.wolframalpha.com/api/

Noticed bugs and issues: 
While searching Wolfram Alpha, UI freezes. Need to add WA search method in new thread. 
