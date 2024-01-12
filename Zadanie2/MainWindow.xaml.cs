using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Zadanie2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InputTextBox1.TextChanged += InputTextBox1_TextChanged;
        }

        private void InputTextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Очищаем сообщение об ошибке при изменении текста в InputTextBox1
            OutputTextBox.Text = string.Empty;
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            string[] input2 = InputTextBox2.Text.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            string input1 = InputTextBox1.Text;

            ReplaceWords(input1, input2);
        }

        private void InputTextBox2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Проверяем, является ли введенный символ числом
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Если не является, отменяем ввод
            }
        }

        void ReplaceWords(string str, string[] numb){
            int l = 0;
            while(l != numb.Length){
                for(int i = l+1; i < numb.Length; i++){
                    if(numb[l] == numb[i]){
                        OutputTextBox.Text = "Номера слов не могут повторяться!";
                        return;
                    }
                }
            l++;
            }

            string[] stroka = str.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            int res = 0;
            int prevres = 0;
            if(numb.Length == 0){
                OutputTextBox.Text = "Вы не ввели номера слов для перестановки!";
                return;
            }
            else if(numb.Length >= 1){
                int count = 0;
                prevres = Convert.ToInt32(numb[0]);
                for(int i=0; i<numb.Length; i++){
                    res = Convert.ToInt32(numb[i]);
                    res = res - count;
                    if(res > stroka.Length){
                        OutputTextBox.Text = "Номер слова больше количества слов в строке";
                        return;
                    }
                    else if(Convert.ToInt32(numb[i]) <= 0){
                        OutputTextBox.Text = "Номер слова не может быть меньше или равен 0!";
                        return;
                    }
                    else if(res == stroka.Length){
                        OutputTextBox.Text = $"Слово {stroka[res-1]} уже является последним!";
                        return;
                    }
                    if(prevres <= res){
                        for(int j=0; j<stroka.Length; j++){
                            if(j==res-1 || res<=0){
                                string rep = String.Empty;
                                while(j+1 != stroka.Length-1){
                                    rep = stroka[j];
                                    stroka[j] = stroka[j+1];
                                    stroka[j+1] = rep;
                                    j++;
                                }
                                rep = stroka[j];
                                stroka[j] = stroka[j+1];
                                stroka[j+1] = rep;
                                break;
                            }
                        }
                        count++;
                        prevres = res + count;
                    }
                    else if(res !=0){
                        for(int j=0; j<stroka.Length; j++){
                            if(j==res || res<=0){
                                string rep = String.Empty;
                                while(j+1 != stroka.Length-1){
                                    rep = stroka[j];
                                    stroka[j] = stroka[j+1];
                                    stroka[j+1] = rep;
                                    j++;
                                }
                                rep = stroka[j];
                                stroka[j] = stroka[j+1];
                                stroka[j+1] = rep;
                                break;
                            }                            
                        }
                        prevres = res + count;
                    }
                    else{
                        for(int j=0; j<stroka.Length; j++){
                            if(j==res+1){
                                string rep = String.Empty;
                                while(j+1 != stroka.Length-1){
                                    rep = stroka[j];
                                    stroka[j] = stroka[j+1];
                                    stroka[j+1] = rep;
                                    j++;
                                }
                                rep = stroka[j];
                                stroka[j] = stroka[j+1];
                                stroka[j+1] = rep;
                                break;
                            }                            
                        }

                    }
                    // string rep = stroka[res - 1];
                    // for (int j = res - 1; j < stroka.Length - 1; j++){
                    //     stroka[j] = stroka[j + 1];
                    // }
                    // stroka[stroka.Length - 1] = rep;

                    // count++;
                }
                VivodArray(stroka);
                return;
            }

        }

        void VivodArray(string[] arr){
            string str = String.Empty;
            for(int i=0; i<arr.Length; i++){
                str += arr[i]+" ";
            }
            OutputTextBox.Text = str;
            return;
        }
    }
}
