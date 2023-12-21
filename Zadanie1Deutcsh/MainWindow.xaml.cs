using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;




namespace Zadanie1Deutcsh
{
    
    public partial class MainWindow : Window
    {

        bool hundertFlag = false;

        Dictionary<string, int> hundertDict = new Dictionary<string, int>{
            {"hundert", 100},
            {"zweihundert", 200},
            {"dreihundert", 300},
            {"vierhundert", 400},
            {"funfhundert", 500},
            {"sechshundert", 600},
            {"siebenhundert", 700},
            {"achthundert", 800},
            {"neunhundert", 900}
        };

        Dictionary<string, int> decimalsDict = new Dictionary<string, int>{
            {"zwanzig", 20},
            {"dreizig", 30},
            {"vierzig", 40},
            {"funfzig", 50},
            {"sechzig", 60},
            {"siebzig", 70},
            {"achtzig", 80},
            {"neunzig", 90}
        };

        Dictionary<string, int> gapDict = new Dictionary<string, int>{
            {"zehn", 10},
            {"elf", 11},
            {"zwolf", 12},
            {"dreizehn", 13},
            {"vierzehn", 14},
            {"funfzehn", 15},
            {"sechzehn", 16},
            {"siebzehn", 17},
            {"achtzehn", 18},
            {"neunzehn", 19}
        };

        Dictionary<string, int> unitsDict = new Dictionary<string, int>{
            {"null", 0},
            {"ein", 1},
            {"eins", 1},
            {"zwei", 2},
            {"drei", 3},
            {"vier", 4},
            {"funf", 5},
            {"sechs", 6},
            {"sieben", 7},
            {"acht", 8},
            {"neun", 9}
        };

        //private bool isInputValid = true;

        //private Timer inputTimer;
        public MainWindow()
        {
            InitializeComponent();

            // inputTimer = new Timer(2000); // 2000 миллисекунд = 2 секунды
            // inputTimer.Elapsed += InputTimerElapsed;

            InputTextBox.PreviewKeyDown += InputTextBox_PreviewKeyDown;

            InputTextBox.TextChanged += InputTextBox_TextChanged;
        }

        void InputTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // inputTimer.Stop();
            // inputTimer.Start();
    // Если пользователь нажал пробел и ввод валиден, вызываем проверку и отображение ошибки
            string input = InputTextBox.Text;
            string[] words = input.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (e.Key == Key.Space || e.Key == Key.Enter)
            {
                ErrorWord(InputTextBox.Text);
                RepeatError(input);
                int result = ConvertNumber(words);
                if(result >= 100){
                    OutputTextBox.Text = Convert.ToString(result);

                    AfterHundert(input);
                    ErrorWord(input);
                    RepeatError(input);
                }
                else{
                    ErrorWord(input);
                    RepeatError(input);
                    if(OutputTextBox.Text == ""){
                        OutputTextBox.Text = $"Вместо '{input}' введите трёхзначное число. Пример: hundert";
                    }
                }
                //ErrorWord(InputTextBox.Text);
                
            }
            else OutputTextBox.Text = "";
            
        }

        // void InputTimerElapsed(object sender, ElapsedEventArgs e)
        // {
        //     // Таймер сработал, показываем сообщение
        //     if(OutputTextBox.Text == string.Empty){
        //         Dispatcher.Invoke(() =>
        //         {
        //             OutputTextBox.Text = "Для продолжения нажмите ПРОБЕЛ!";
        //         });
        //     }

        // }

        void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Вызываем метод ConvertButton_Click при изменении текста
            ConvertButton_Click(sender, e);
            
        }



        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            string inputText = InputTextBox.Text;
            string[] words = inputText.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // if (AfterHundert(words) != -1)
            // {
            //     int result = ConvertNumber(words);
            //     OutputTextBox.Text = Convert.ToString(result);
            // }
            // else{
            // int res = AfterHundert(words);
            // OutputTextBox.Text = Convert.ToString(res);
            // }
            // ErrorWord(inputText);
            // int result = ConvertNumber(words);
            // if(result != 0 && !inputText.EndsWith(words[words.Length - 1])){
            //     OutputTextBox.Text = Convert.ToString(result);
            // }
            // else AfterHundert(inputText);
            //CheckAndDisplayError(inputText);            
            
        }

        void AfterHundert(string inputText)
        {
            string[] word = inputText.ToLower().Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            hundertFlag = false;
            //foreach(var word in input)
            for(int i = 0; i < word.Length; i++){
                if(hundertDict.ContainsKey(word[i])){
                    if(hundertFlag == true){
                        if(word[i-1]=="und"){
                            OutputTextBox.Text = "Здесь не должно быть слова 'und'!";
                            return;
                        }
                        else{
                        OutputTextBox.Text = $"Ошибка! После слов формата СОТЕН {word[0]}, не может быть слово формата СОТЕН {word[i]}!";
                        //hundertFlag = false;
                        //return -1;
                        return;
                        }

                        //break;
                    }
                    else{
                        hundertFlag = true;
                        //break;
                        continue;
                    }
                }
                else if(hundertFlag == true){
                    if(decimalsDict.ContainsKey(word[i])){
                        //decimalFlag = true;
                        // if(!hundertDict.ContainsKey(word[i-1])){
                        //     OutputTextBox.Text = $"Ошибка! Слово {word[i]} не может повторяться в трехзначном числе!";
                        //     return;
                        // }

                        if(word[i] != word[word.Length - 1]){
                            OutputTextBox.Text = $"Ошибка! После слова ДЕСЯТИЧНОГО формата {word[i]}" +
                            $" не могут идти слова, если перед ним было слово формата СОТЕН {word[0]}";
                            //hundertFlag = false;
                            //decimalFlag = false;
                            //return -1;
                            return;
                            //break;
                        }
                        else if(word[i] == word[word.Length -1]){
                            if(unitsDict.ContainsKey(word[i-1])){
                                OutputTextBox.Text = $"Ошибка! Слово '{word[i]}' не может идти после слова ЕДИНИЧНОГО формата {word[i-1]}, если перед ним не было слова 'und'!";
                            }
                            //hundertFlag = false;
                            //decimalFlag = false;
                            //return 0;
                            return;
                            //break;
                        }
                    }
                    else if(gapDict.ContainsKey(word[i])){
                        //gapFlag = true;
                        // if(!hundertDict.ContainsKey(word[i-1])){
                        //     OutputTextBox.Text = $"Ошибка! Слово {word[i]} не может повторяться в трехзначном числе!";
                        //     return;
                        // }
                        if(word[i] != word[word.Length - 1]){
                            //gapFlag = true;
                            OutputTextBox.Text = $"Ошибка! После слова формата 10-19 ({word[i]}) " +
                            $"не могут идти слова, если перед ним было слово формата СОТЕН {word[i-1]}";
                            //hundertFlag = false;
                            //decimalFlag = false;
                            //return -1;
                            return;
                            //break;
                        }
                        else if(word[i] == word[word.Length - 1]){
                            if(unitsDict.ContainsKey(word[i-1])){
                                OutputTextBox.Text = $"Слово {word[i]} не может идти после слова ЕДИНИЧНОГО формата";
                                return;
                            }
                            else{
                                //hundertFlag = false;
                                //decimalFlag = false;
                                //return 0;
                                return;
                                //break;
                            }           
                        }
                    }
                    else if(unitsDict.ContainsKey(word[i])){
                        // if(!hundertDict.ContainsKey(word[i-1]) && word[i] == word[i-1]){
                        //     OutputTextBox.Text = $"Ошибка! Слово {word[i]} не может повторяться в трехзначном числе!";
                        //     return;
                        // }
                        if(word[i] == word[word.Length - 1]){
                            if(word.Length <=2){
                                if(word[i] == "ein"){
                                    OutputTextBox.Text = $"Ошибка! Единица '{word[i]}' после СОТЕН должна быть 'einS'!";
                                    //return -1;
                                    return;
                                }
                                //return 0;
                                return;
                            }
                            else if(word[i-1] == "eins" || word[i - 1] == "ein"){
                                OutputTextBox.Text = $"Ошибка! Единица '{word[i]}' не может идти после единицы '{word[i - 1]}'";
                                return;
                            }
                            else if(word[i-1] == "und"){
                                OutputTextBox.Text = "Слово 'und' не должно стоять здесь!";
                                return;
                            }
                            else if(!hundertDict.ContainsKey(word[i-1])){
                                OutputTextBox.Text = $"Ошибка! Слово ЕДИНИЧНОГО формата '{word[i]}' не должно идти после слова ЕДИНИЧНОГО формата!";
                                return;
                            }


                        }
                        else if(word[i + 1] == word[word.Length - 1] && word[i + 1]=="und"){
                            if(inputText.EndsWith(" ") || word[word.Length - 1] == "und"){
                                OutputTextBox.Text = $"Ошибка! Слово {word[i+1]} не может быть ПОСЛЕДНИМ!";
                                return;
                            }
                            else{
                                return;
                            }
                            //return -1;
                        }
                        else if(word[i + 1] == "und" && word[i + 1] != word[word.Length -1]){
                            //undFlag = true;
                            if(decimalsDict.ContainsKey(word[i+2])){
                                //hundertFlag = false;
                                //return 0;
                                return;
                                //break;
                            }
                            else if(unitsDict.ContainsKey(word[i + 2])){
                                // OutputTextBox.Text = $"Ошибка! После слова ЕДИНИЧНОГО формата '{word[i]}', если передним было слово формата СОТЕН '{word[i - 1]}', " +
                                // $"не может стоять снова слово ЕДИНИЧНОГО формата {word[i+2]}";
                                OutputTextBox.Text = "Ошибка! Слово 'und' не должно здесь стоять!";
                                return;
                            }
                            else if(!decimalsDict.ContainsKey(word[i+2])){
                                OutputTextBox.Text = $"Ошибка! После слова {word[i+1]} могут идти слова только ДЕСЯТИЧНОГО формата!";
                                // hundertFlag = false;
                                //return -1;
                                return;
                                //break;
                            }
                        }
                        else if(word[i] != word[word.Length - 1] && inputText.EndsWith(" ")){
                            // hundertFlag = false;
                            OutputTextBox.Text = $"Ошибка! После слова ЕДИНИЧНОГО фомата {word[i]}, если перед ним есть слово формата СОТЕН {word[i-1]}, " + 
                            "должно стоять слово 'und'!";
                            //return -1;
                            return;
                            //break;
                        }
                        // if(word[i] == "und"){
                            
                    }
                    else if(word[i] == "und" && hundertDict.ContainsKey(word[i-1]) && word[word.Length - 1] == "und"){
                        OutputTextBox.Text = "Ошибка! После слова формата СОТЕН не может быть слово 'und'!";
                        //return -1;
                        return;
                    }

                }
                else if(word[i] == "und"){
                    OutputTextBox.Text = "Слово 'und' не может быть ПЕРВЫМ";
                    //return -1;
                    return;
                }
                else if((decimalsDict.ContainsKey(word[i]) || gapDict.ContainsKey(word[i]) || unitsDict.ContainsKey(word[i])) && word[i] != word[word.Length - 1]){
                    if(hundertDict.ContainsKey(word[i+1]) && !unitsDict.ContainsKey(word[i])){
                        OutputTextBox.Text = $"Слово {word[i]} не может стоять перед СОТНЯМИ {word[i + 1]}";
                        return;
                    }
                    else if(hundertDict.ContainsKey(word[i+1]) && unitsDict.ContainsKey(word[i])){
                        OutputTextBox.Text = $"Измените ввод на {word[i] + word[i+1]}";
                        return;
                    }
                    return;
                }
            }
            //return 0;
            return;
        }

        int ConvertNumber(string[] input){
            int currentNumber = 0;      
            int result = 0;
            foreach (var word in input){
                if (hundertDict.ContainsKey(word)){
                    currentNumber += hundertDict[word];
                }
                if (decimalsDict.ContainsKey(word)){
                    currentNumber += decimalsDict[word];
                }
                if (gapDict.ContainsKey(word)){
                    currentNumber += gapDict[word];
                }
                if (unitsDict.ContainsKey(word)){
                    currentNumber += unitsDict[word];
                }
                if (word == "und"){
                    // if(input[input.Length - 1] == "und"){
                    //     return 0;
                    // }
                }

                result = currentNumber;
            
            }
            return result;
        }
        // private void ErrorWord(string inputText)
        // {
        //     string[] words = inputText.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        //     foreach (var word in words)
        //     {
        //         if (!hundertDict.ContainsKey(word) && !decimalsDict.ContainsKey(word) &&
        //             !gapDict.ContainsKey(word) && !unitsDict.ContainsKey(word))
        //         {
        //             // Слово не найдено в словаре, выводим сообщение об ошибке
        //             OutputTextBox.Text += $"\nОшибка в слове: {word}, предположительно с ошибкой";
        //         }
        //     }
        // }
        void ErrorWord(string inputText)
        {
            string[] words = inputText.ToLower().Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            if(words.Length != 0){
                // Флаг, указывающий, что текущее слово содержит ошибку
                bool currentError;

                foreach (var word in words)
                {
                    currentError = !hundertDict.ContainsKey(word) && !decimalsDict.ContainsKey(word) &&
                                !gapDict.ContainsKey(word) && !unitsDict.ContainsKey(word) && word != "und";

                    if (currentError)
                    {
                        // Слово не найдено в словаре, выводим сообщение об ошибке
                        OutputTextBox.Text = $"\nСлово '{word}' не найдено в словаре!";
                        currentError = false;
                    }
                    // else if(currentError && inputText.EndsWith(word)){
                    //     OutputTextBox.Text = $"\nСлово '{word}' не найдено в словаре!";
                    //     currentError = false;
                    // }
                }
                currentError = !hundertDict.ContainsKey(words[words.Length - 1]) && !decimalsDict.ContainsKey(words[words.Length - 1]) &&
                            !gapDict.ContainsKey(words[words.Length - 1]) && !unitsDict.ContainsKey(words[words.Length - 1]) && words[words.Length - 1] != "und";

                if(currentError && inputText.EndsWith(words[words.Length - 1]) && words.Length > 3){
                    OutputTextBox.Text = $"\nСлово '{words[words.Length - 1]}' не найдено в словаре!";
                }   
            }
        }

        void RepeatError(string inputText){
            string[] words = inputText.ToLower().Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, int> wordCount = new Dictionary<string, int>();

            foreach (string word in words)
            {
                if (wordCount.ContainsKey(word))
                {
                    wordCount[word]++;
                    if (wordCount[word] == 2)
                    {
                        OutputTextBox.Text = $"Ошибка! Слово {word} не может повторяться в трёхзначном числе!";
                        return;
                    }
                }
                else
                {
                    wordCount[word] = 1;
                }
            }

        }
        
            


        

            // // Если текущее слово содержит ошибку, но не является последним,
            // // или если предыдущее слово содержало ошибку, добавляем пробел перед ошибкой
            // if (currentError && !inputText.EndsWith(" ") && OutputTextBox.Text.Length > 0)
            // {
            //     OutputTextBox.Text += " ";
            // }

            // Добавим новую строку в конце вывода
    }

        // void CheckAndDisplayError(string inputText)
        // {
        //     string[] words = inputText.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        //     // Проверяем, есть ли ошибки в словах
        //     bool hasError = words.Any(word =>
        //         !hundertDict.ContainsKey(word) || !decimalsDict.ContainsKey(word) ||
        //         !gapDict.ContainsKey(word) || !unitsDict.ContainsKey(word));

        //     // Очищаем поле вывода
        //     OutputTextBox.Text = string.Empty;

        //     // Если есть ошибка и ввод не завершен, не отображаем ошибку
        //     if (hasError && !inputText.EndsWith(" "))
        //     {
        //         isInputValid = false;
        //         return;
        //     }

        //     // Если ошибок нет или ввод завершен, отображаем результат
        //     isInputValid = true;
            
        //     OutputTextBox.Text += Convert.ToString(ConvertNumber(words));
        //     //OutputTextBox.Text += "\n";
        //     inputTimer.Stop();
        // }

}

