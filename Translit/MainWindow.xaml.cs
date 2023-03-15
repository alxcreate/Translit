using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Transliteration
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Устанавливаем фокус на текстовое поле ввода при запуске приложения
            InputTextBox.Focus();
        }

        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Получаем входной текст
            string inputText = InputTextBox.Text;

            // Транслитерируем текст
            string outputText = Transliterate(inputText);

            // Отображаем транслитерированный текст
            OutputTextBox.Text = outputText;
        }

        private string Transliterate(string input)
        {
            // Список соответствий русских букв и латинских заменителей
            var translitMap = new Dictionary<string, string>
    {
        {"а", "a"}, {"б", "b"}, {"в", "v"}, {"г", "g"}, {"д", "d"}, {"е", "e"}, {"ё", "e"},
        {"ж", "zh"}, {"з", "z"}, {"и", "i"}, {"й", "y"}, {"к", "k"}, {"л", "l"}, {"м", "m"},
        {"н", "n"}, {"о", "o"}, {"п", "p"}, {"р", "r"}, {"с", "s"}, {"т", "t"}, {"у", "u"},
        {"ф", "f"}, {"х", "kh"}, {"ц", "ts"}, {"ч", "ch"}, {"ш", "sh"}, {"щ", "shch"}, {"ъ", ""},
        {"ы", "y"}, {"ь", ""}, {"э", "e"}, {"ю", "yu"}, {"я", "ya"}
    };

            // Создаем объект StringBuilder для накопления результата
            StringBuilder output = new StringBuilder();

            // Транслитерируем каждый символ входной строки
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];

                // Если символ есть в списке соответствий, добавляем заменитель
                if (translitMap.ContainsKey(char.ToLower(c).ToString()))
                {
                    string translit = translitMap[char.ToLower(c).ToString()];

                    // Если символ "e" и предыдущий символ - "ь" или "ъ", заменяем на "ye"
                    if (translit == "e" && i > 0 && (input[i - 1] == 'ь' || input[i - 1] == 'ъ' || input[i - 1] == 'Ь' || input[i - 1] == 'Ъ'))
                    {
                        translit = "ye";
                    }

                    // Если символ был в верхнем регистре, делаем заменитель также в верхнем регистре
                    if (char.IsUpper(c))
                    {
                        if (input[i] == 'Ь' || input[i] == 'Ъ')
                        {
                            continue;
                        }
                        else
                        {
                            translit = char.ToUpper(translit[0]) + translit.Substring(1);
                        }
                    }

                    output.Append(translit);
                }
                else
                {
                    // Иначе добавляем исходный символ
                    output.Append(c);
                }
            }

            return output.ToString();
        }
    }
}
