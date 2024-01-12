// Перестановка слов в конец по введённым номерам
System.Console.WriteLine("Введите строку: ");
string str = Console.ReadLine();
System.Console.WriteLine("Введите номер слов, которые хотите переставить в конец");
string[] numb = Console.ReadLine().Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

ReplaceWords(str, numb);

void ReplaceWords(string str, string[] numb){
    string[] stroka = str.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
    int res = 0;
    if(numb.Length == 0){
        System.Console.WriteLine("Вы не ввели номера слов для перестановки!");
        return;
    }
    else if(numb.Length >= 1){
        int count = 0;
        for(int i=0; i<numb.Length; i++){
            res = Convert.ToInt32(numb[i]);
            res = res - count;
            if(res > stroka.Length){
                System.Console.WriteLine("Номер слова больше количества слов в строке");
                return;
            }
            else if(res <= 0){
                System.Console.WriteLine("Номер слова не может быть меньше или равен 0!");
                return;
            }
            else if(res == stroka.Length){
                System.Console.WriteLine($"Слово {stroka[res-1]} уже является последним!");
                return;
            }
            for(int j=0; j<stroka.Length; j++){
                                if(j == res-1 || res<=0){
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
        }
        VivodArray(stroka);
        return;
    }

}

void VivodArray(string[] arr){
    for(int i=0; i<arr.Length; i++){
        System.Console.Write(arr[i] + " ");
    }
}

