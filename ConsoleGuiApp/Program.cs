using System;

class Program
{
    static void Main(string[] args)
    {
        // 簡易的なGUI風メニュー（コンソール版）
        string[] options = { "1. ゲームをはじめる", "2. せってい", "3. おわる" };
        int selectedIndex = 0;

        ConsoleKey keyPressed;

        do
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("       AI駆動開発：コンソールGUI        ");
            Console.WriteLine("========================================");
            Console.WriteLine("上下の矢印キーで選んで、Enterキーで決定してね。\n");

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

            // キー入力を受け取る
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            keyPressed = keyInfo.Key;

            if (keyPressed == ConsoleKey.UpArrow)
            {
                selectedIndex--;
                if (selectedIndex < 0) selectedIndex = options.Length - 1;
            }
            else if (keyPressed == ConsoleKey.DownArrow)
            {
                selectedIndex++;
                if (selectedIndex >= options.Length) selectedIndex = 0;
            }

        } while (keyPressed != ConsoleKey.Enter);

        Console.Clear();
        Console.WriteLine("========================================");
        if (selectedIndex == 0)
        {
            Console.WriteLine(" 「ゲームをはじめる」 が選ばれました！");
        }
        else if (selectedIndex == 1)
        {
            Console.WriteLine(" 「せってい」 が選ばれました！");
        }
        else
        {
            Console.WriteLine(" アプリを終了します。またね！");
        }
        Console.WriteLine("========================================");
    }
}
