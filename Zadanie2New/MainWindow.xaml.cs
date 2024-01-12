using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Zadanie2New
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
            string[] input2 = InputTextBox2.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string input1 = InputTextBox1.Text;
            if (input2.Length != 2 || !int.TryParse(input2[0], out int startIndex) || !int.TryParse(input2[1], out int endIndex))
            {
                // Вывод ошибки при некорректном вводе промежутка
                OutputTextBox.Text = "Некорректный ввод промежутка";
            }
            else if (startIndex > endIndex)
            {
                // Вывод ошибки при некорректном порядке чисел в промежутке
                OutputTextBox.Text = "Первое число промежутка не может быть больше второго";
            }
            else if(startIndex == endIndex){
                OutputTextBox.Text = "Первое число промежутка не может быть равно второму";
            }
            else
            {
                string result = MoveWordsToEnd(input1, input2);
                // Вывод результата в OutputTextBox
                OutputTextBox.Text = result;
            }
        }

        private void InputTextBox2_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            // Проверяем, является ли введенный символ числом
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Если не является, отменяем ввод
            }
        }

        private string MoveWordsToEnd(string input, string[] indices)
        {
            // Обработка исключений и проверка корректности ввода
            if (string.IsNullOrWhiteSpace(input))
            {
                return "Некорректный ввод строки";
            }
            else if (indices.Length != 2) {
                return "Промежуток может состоять только из 2-ух цифр";

            }

            if (!int.TryParse(indices[0], out int startIndex) || !int.TryParse(indices[1], out int endIndex))
            {
                return "Некорректный ввод чисел";
            }

            if (startIndex < 1 || endIndex >= input.Split(' ').Length || startIndex > endIndex)
            {
                return "Некорректный промежуток";
            }

            string[] words = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Выбранный промежуток слов
            string[] selectedWords = words.Skip(startIndex - 1).Take(endIndex - startIndex + 1).ToArray();

            // Удаление выбранного промежутка из массива
            words = words.Except(selectedWords).ToArray();

            // Перемещение выбранного промежутка в конец массива
            words = words.Concat(selectedWords).ToArray();

            // Сборка строки из массива слов
            return string.Join(" ", words);
        }
    }
}
