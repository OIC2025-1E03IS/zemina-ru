using ConsoleGuiApp;

class Program
{
    private static int _maxValue = 50;

    static void Main(string[] args)
    {
        string[] options = { "1. ゲームをはじめる", "2. せってい", "3. おわる" };
        int selectedIndex = 0;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("       数字当てゲーム");
            Console.WriteLine("========================================");
            Console.WriteLine("上下の矢印キーで選んで、Enterキーで決定してね。");
            Console.WriteLine($"現在の設定：1 ～ {_maxValue}\n");

            for (int i = 0; i < options.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine($"> {options[i]} <");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {options[i]}");
                }
            }

            ConsoleKey key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.UpArrow)
                selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
            else if (key == ConsoleKey.DownArrow)
                selectedIndex = (selectedIndex + 1) % options.Length;
            else if (key == ConsoleKey.Enter)
            {
                if (selectedIndex == 0) PlayGame();
                else if (selectedIndex == 1) ChangeSettings();
                else
                {
                    Console.WriteLine("\nアプリを終了します。またね！");
                    return;
                }
            }
        }
    }

    private static void PlayGame()
    {
        int target = GameLogic.CreateTarget(_maxValue);
        int attempts = 0;

        Console.Clear();
        Console.WriteLine($"1 ～ {_maxValue} の数字を当ててね！");
        Console.WriteLine("数字を入力して Enter キーを押してください。");

        while (true)
        {
            Console.Write("予想した数字：");
            string? input = Console.ReadLine();

            if (!int.TryParse(input, out int guess) || !GameLogic.IsValidGuess(guess, _maxValue))
            {
                Console.WriteLine($"1 ～ {_maxValue} の整数を入力してください。");
                continue;
            }

            attempts++;
            switch (GameLogic.EvaluateGuess(guess, target))
            {
                case GuessResult.TooLow:
                    Console.WriteLine("もっと大きい数字です！");
                    break;
                case GuessResult.TooHigh:
                    Console.WriteLine("もっと小さい数字です！");
                    break;
                case GuessResult.Correct:
                    Console.WriteLine($"正解！ {attempts} 回で当たりました。");
                    Console.WriteLine("Enter キーでメニューに戻ります。");
                    Console.ReadLine();
                    return;
            }
        }
    }

    private static void ChangeSettings()
    {
        Console.Clear();
        Console.WriteLine("=== せってい ===");
        Console.WriteLine("難易度を選んでください。");
        Console.WriteLine("1. かんたん（1 ～ 10）");
        Console.WriteLine("2. ふつう（1 ～ 50）");
        Console.WriteLine("3. むずかしい（1 ～ 100）");
        Console.Write("番号：");

        string? input = Console.ReadLine();
        _maxValue = input switch
        {
            "1" => 10,
            "2" => 50,
            "3" => 100,
            _ => _maxValue
        };

        Console.WriteLine(input is "1" or "2" or "3"
            ? $"設定を {_maxValue} までに変更しました。"
            : "入力が正しくないため、設定は変更していません。");
        Console.WriteLine("Enter キーでメニューに戻ります。");
        Console.ReadLine();
    }
}