using System;
using System.Collections.Generic;
using System.Data.Common;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Zadanie1New
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool hundertFlag;
        bool unitFlag;
        bool gapFlag;
        bool decimalFlag;
        //bool undFlag;
        string[] hunderts = new string[2];
        Dictionary<string, int> unitDict = new Dictionary<string, int>{
            {"null",0},
            {"ein",1},
            {"eins",1},
            {"zwei",2},
            {"drei",3},
            {"vier",4},
            {"funf",5},
            {"sechs",6},
            {"sieben",7},
            {"acht",8},
            {"neun",9}
        };

        Dictionary<string, int> gapDict = new Dictionary<string, int>{
            {"elf", 11},
            {"zwolf",12},
            {"dreizehn",13},
            {"vierzehn",14},
            {"funfzehn",15},
            {"sechzehn", 16},
            {"siebzehn",17},
            {"achtzehn",18},
            {"neunzehn",19}
        };

        Dictionary<string, int> decimalDict = new Dictionary<string, int>{
            {"zehn", 10},
            {"zwanzig",20},
            {"dreizig",30},
            {"vierzig",40},
            {"funfzig",50},
            {"sechzig",60},
            {"siebzig",70},
            {"achtzig",80},
            {"neunzig",90}
        };

        public MainWindow()
        {
            InitializeComponent();

            InputTextBox.PreviewKeyDown += InputTextBox_PreviewKeyDown;

        }

        void InputTextBox_PreviewKeyDown(object sender, KeyEventArgs e){

            string input = InputTextBox.Text;
            if(e.Key == Key.Enter){
                ErrorWord(input);

                if(OutputTextBox.Text == ""){

                    CorrectHundert(input);
                    if(OutputTextBox.Text == ""){
                        AfterHundert(input);
                        unitFlag = false;
                        gapFlag = false;
                        decimalFlag = false;
                        hundertFlag = false;
                        if(OutputTextBox.Text == ""){
                            OutputTextBox.Text = Convert.ToString(ConvertNumber(input)); 
                        }
                    }
                }
                
            }
            else{
                OutputTextBox.Text = "";
            }
            

        }

        void CorrectHundert(string str){
            string[] word = str.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int count = 0;
            if(word.Length >= 2){
                if(word.Contains("hundert")){
                    foreach(string words in word){
                        if(words == "hundert"){
                            count++;
                        }
                    }
                    if(count > 1){
                        OutputTextBox.Text = "Ошибка! Слово 'hundert' не может повторяться в трёхзначном числе!";
                        hundertFlag = false;
                        return;
                    }
                    else if(unitDict.ContainsKey(word[0]) && word[1] == "hundert"){
                        if(word[0] == "eins"){
                            OutputTextBox.Text = $"Ошибка! Вместо слова '{word[0] + " " + word[1]}' введите 'hundert' или 'ein hundert'!";
                            return;
                        }
                        else{
                            hundertFlag = true;
                            hunderts[0] = word[0];
                            hunderts[1] = word[1];
                            return;
                        }
                    }
                    else if(word[0] == "hundert"){
                        hunderts[0] = word[0];
                        hundertFlag = true;
                        return;
                    }
                    else if(unitDict.ContainsKey(word[0]) && word[1] == "und"){
                        OutputTextBox.Text = $"Ошибка! Слово 'und' не может идти после слова {word[0]}! Ожидается слово 'hundert'!";
                        hundertFlag = false;
                        return;
                    }
                        // else if(word[0] == "hundert" && word[1] == "hundert"){
                        //     OutputTextBox.Text = "Ошибка! Слово 'hundert' не может повторяться в трёхзначном числе!";
                        //     hundertFlag = false;
                        //     return;
                        // }
                    else if(word[0] == "und"){
                        OutputTextBox.Text = "Ошибка! Слово 'und' не может быть первым!";
                        hundertFlag = false;
                        return;
                    }
                    else{
                        OutputTextBox.Text = "Ошибка! Трёхзначное число может начинаться только с СОТЕН! Пример: 'drei hundert'";
                        hundertFlag = false;
                        return;
                    }
                }
                else{
                    OutputTextBox.Text = "Ошибка! Трёхзначное число должно содержать слово 'hundert'!";
                    return;
                }
            }
            else if(word[0] == "hundert"){
                hunderts[0] = word[0];
                hundertFlag = true;
                return;
            }
            else if(word[0] == "und"){
                OutputTextBox.Text = "Ошибка! Слово 'und' не может быть первым!";
                hundertFlag=false;
                return;
            }
            else if(word[0] != "hundert"){
                OutputTextBox.Text = "Ошибка! Первым словом может быть только слово формата СОТЕН";
                hundertFlag = false;
                return;
            }
        
                // if(word[0] == "hundert" && word[1] == "hundert"){
                //     OutputTextBox.Text = "Ошибка! Слово 'hundert' не может повторяться в трёхзначном числе!";
                //     hundertFlag = false;
                //     return;
                // }
        }

        void AfterHundert(string str){
            //string[] words = str.ToLower().Split(new[] {' '},StringSplitOptions.RemoveEmptyEntries);

            string[] diff = ExceptWords(str);

            if(hundertFlag == true){
                for(int i = 0; i<diff.Length; i++){
                    if(diff[i] == "und"){
                        if(diff[0] == diff[i]){
                            OutputTextBox.Text = "Ошибка! Слово 'und' не может идти после слова формата СОТЕН!";
                            hundertFlag = false;
                            return;
                        }
                        else if(gapDict.ContainsKey(diff[i-1])){
                            OutputTextBox.Text = "Ошибка! После слов формата 11-19 не может идти слово 'und'!";
                            gapFlag = false;
                            return;
                        }
                        else if(decimalDict.ContainsKey(diff[i-1])){
                            OutputTextBox.Text = "Ошибка! После слов ДЕСЯТИЧНОГО формата не может идти слово 'und"!;
                            decimalFlag = false;
                            return;
                        }
                        else if(diff[i-1] == "und"){
                            OutputTextBox.Text = "Ошибка! Вместо слова 'und' ожидается число ДЕСЯТИЧНОГО формата!";
                            gapFlag = false;
                            unitFlag = false;
                            return;
                        }
                        else if(diff[i] == diff[diff.Length-1]){
                            OutputTextBox.Text = "Ошибка! Слово 'und' не может быть последним!";
                            unitFlag = false;
                            gapFlag = false;
                            decimalFlag = false;
                            return;
                        }
                    }
                    else if(diff[i] == "hundert"){
                        OutputTextBox.Text = "Ошибка! Слово формата СОТЕН c 'hundert' не может повторяться!";
                        return;
                    }
                    else if(unitDict.ContainsKey(diff[i])){
                        // if(unitFlag != true){
                        //     unitFlag = true;
                        //     continue;
                        // }
                        // if(diff[0] == diff[i]){

                            // continue;
                        // }
                        if(unitFlag == true || gapFlag == true || decimalFlag == true){
                            if(gapDict.ContainsKey(diff[i-1])){
                                OutputTextBox.Text = $"Ошибка! Слово формата ЕДИНИЦ '{diff[i]}' не может идти после слов формата 11-19 '{diff[i-1]}'!";
                                unitFlag = false;
                                return;
                            }
                            else if(decimalDict.ContainsKey(diff[i-1])){
                                OutputTextBox.Text = $"Ошибка! Слово формата ЕДИНИЦ '{diff[i]}' не может идти после слов ДЕСЯТИЧНОГО формата '{diff[i-1]}'!";
                                unitFlag = false;
                                return;
                            }
                            else if(unitDict.ContainsKey(diff[i-1])){
                                OutputTextBox.Text = $"Ошибка! Вместо слова ЕДИНИЧНОГО формата '{diff[i]}' ожидается слово 'und'!";
                                unitFlag = false;
                                return;
                            }
                            else if(diff[i-1] == "und"){
                                OutputTextBox.Text = $"Ошибка! Вместо слова ЕДИНИЧНОГО формата {diff[i]} ожидается число ДЕСЯТИЧНОГО формата!";
                                unitFlag = false;
                                return;
                            }
                        } 
                        else{
                            if(diff[i] == diff[diff.Length - 1] && diff[i] == "ein"){
                                OutputTextBox.Text = "Ошибка! Последнее слово должно быть 'eins' вместо 'ein'!";
                                unitFlag = false;
                                return;
                            }
                            else{
                                unitFlag = true;
                                continue;
                            }
                        }   
                    }
                    else if(gapDict.ContainsKey(diff[i])){
                        // if(gapFlag != true){
                        //     gapFlag = true;
                        //     continue;
                        // }
                        // if(diff[0] == diff[i]){
                        //     continue;
                        // }
                        if(unitFlag == true || gapFlag == true || decimalFlag == true){
                            if(decimalDict.ContainsKey(diff[i-1])){
                                OutputTextBox.Text = $"Ошибка! После слова ДЕСЯТИЧНОГО формата '{diff[i-1]}'" +
                                $" не может идти слово формата 11-19 {diff[i]}";
                                gapFlag = false;
                                return;
                            }
                            else if(gapDict.ContainsKey(diff[i-1])){
                                OutputTextBox.Text = $"Ошибка! После слова формата 11-19 '{diff[i-1]}'" +
                                $" не может идти снова слово формата 11-19 '{diff[i]}'!";
                                gapFlag = false;
                                return;
                            }
                            else if(unitDict.ContainsKey(diff[i-1])){
                                OutputTextBox.Text = $"Ошибка! Вместо слова формата 11-19 '{diff[i]}' ожидается слово 'und'";
                                gapFlag = false;
                                return;
                            }
                            else if(diff[i-1] == "und"){
                                OutputTextBox.Text = $"Ошибка! Вместо слова формата 11-19 '{diff[i]}' ожидается слово" +
                                " ДЕСЯТИЧНОГО формата!";
                                gapFlag = false;
                                return;
                            }
                        }
                        else{
                            gapFlag = true;
                            continue;
                        }    
                    }
                    else if(decimalDict.ContainsKey(diff[i])){
                        
                        // else if(decimalFlag != true){
                        //     decimalFlag = true;
                        //     continue;
                        // }
                        // if(diff[0] == diff[i]){
                        //     continue;
                        // }
                        if(unitFlag == true || gapFlag == true || decimalFlag == true){
                        
                            if(gapDict.ContainsKey(diff[i-1])){
                                OutputTextBox.Text = $"Ошибка! После слова формата 11-19 '{diff[i-1]}'"+
                                $" не может идти слово ДЕСЯТИЧНОГО формата {diff[i]}";
                                decimalFlag = false;
                                return;
                            }
                            else if(decimalDict.ContainsKey(diff[i-1])){
                                OutputTextBox.Text = $"Ошибка! После слова ДЕСЯТИЧНОГО формата '{diff[i-1]}' "+
                                $"не может идти снова слово ДЕСЯТИЧНОГО формата'{diff[i]}'";
                                decimalFlag = false;
                                return;
                            }
                            else if(unitDict.ContainsKey(diff[i-1])){
                                OutputTextBox.Text = $"Ошибка! Вместо слова ДЕСЯТИЧНОГО формата '{diff[i]}' ожидается слово 'und'";
                                decimalFlag = false;
                                return;
                            }
                            else if(diff[i-1] == "und" && diff[i] == "zehn"){
                                OutputTextBox.Text = "Исключение! Слово ДЕСЯТИЧНОГО формата 'zehn' не может стоять после 'und'!";
                                decimalFlag = false;
                                return;
                            }
                        }
                        else{
                            decimalFlag = true;
                            continue;
                        }
                    }

                }
            }
            return;
        }

        void ErrorWord(string inputText)
        {
            string[] words = inputText.ToLower().Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            if(words.Length != 0){
                // Флаг, указывающий, что текущее слово содержит ошибку
                bool currentError;

                foreach (var word in words)
                {
                    currentError = word != "hundert" && !decimalDict.ContainsKey(word) &&
                                !gapDict.ContainsKey(word) && !unitDict.ContainsKey(word) && word != "und";

                    if (currentError)
                    {
                        // Слово не найдено в словаре, выводим сообщение об ошибке
                        OutputTextBox.Text = $"\nСлово '{word}' не найдено в словаре!";
                        currentError = false;
                        break;
                    }
                    // else if(currentError && inputText.EndsWith(word)){
                    //     OutputTextBox.Text = $"\nСлово '{word}' не найдено в словаре!";
                    //     currentError = false;
                    // }
                }
                // currentError = words[words.Length - 1] != "hundert" && !decimalDict.ContainsKey(words[words.Length - 1]) &&
                //             !gapDict.ContainsKey(words[words.Length - 1]) && !unitDict.ContainsKey(words[words.Length - 1]) && words[words.Length - 1] != "und";

                // if(currentError && inputText.EndsWith(words[words.Length - 1]) && words.Length > 3){
                //     OutputTextBox.Text = $"\nСлово '{words[words.Length - 1]}' не найдено в словаре!";
                // }   
            }
        }

        int ConvertNumber(string str){
            string[] words = str.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string[] diff1 = ExceptWords(str);

            int result = 0;
            int hundRes = 0;
            foreach(var word in diff1){
                
                if(unitDict.ContainsKey(word)){
                    result += unitDict[word];
                }
                else if(gapDict.ContainsKey(word)){
                    result += gapDict[word];
                } 
                else if(decimalDict.ContainsKey(word)){
                    result += decimalDict[word];
                }
                else if(word == "und"){
                    continue;
                }
            }
            

            foreach(var word in hunderts){
                
                if(unitDict.ContainsKey(word)){
                    hundRes = unitDict[word]*100;
                    break;
                }
                else if(word == "hundert"){
                    hundRes = 100;
                    break;
                }
            }
            result += hundRes;
            return result;    
        }

        string[] ExceptWords(string str){
            string[] words = str.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            
            if(words[0] == "hundert"){
                string[] zap = new string[words.Length - 1];
                for(int i = 1; i < words.Length; i++){
                    zap[i-1] = words[i];
                }
                return zap; 
            }
            else{
                string[] zap = new string[words.Length - 2];
                for(int i = 2; i < words.Length; i++){
                    zap[i-2] = words[i];
                }
                return zap;
            } 
        }

    }
}

