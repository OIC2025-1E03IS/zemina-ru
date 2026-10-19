# 数字当てゲーム（ConsoleGuiApp）

.NET 8 で動作するコンソールアプリです。矢印キーでメニューを選択し、数字当てゲームを遊べます。

## 起動方法

.NET 8 SDK をインストールし、リポジトリのルートで実行します。

```bash
dotnet run --project ConsoleGuiApp/ConsoleGuiApp.csproj
```

## 操作方法

- 上下矢印キー：メニュー選択
- Enter：決定
- 「ゲームをはじめる」：1～設定された上限の数字を当てます
- 「せってい」：難易度を1～10、1～50、1～100から選びます
- 「おわる」：アプリを終了します

整数以外や範囲外の数字を入力した場合は再入力を求めます。

## テスト

```bash
dotnet test tests/ConsoleGuiApp.Tests/ConsoleGuiApp.Tests.csproj
```

ゲームロジックのテストでは、乱数の範囲、無効な上限、入力の境界値、大小・正解判定を確認します。

## CI

GitHub Actions は push と Pull Request で .NET 8 のビルドとテストを実行します。
