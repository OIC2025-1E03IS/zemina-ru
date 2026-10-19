# 数字当てゲーム

このリポジトリには、元のC#コンソール版と、Python + Flaskで動くWeb版の数字当てゲームがあります。

## Web版の起動方法（初心者向け）

### 1. Pythonを用意する

Python 3.10以降をインストールしてください。

### 2. リポジトリのフォルダーを開く

ターミナル（WindowsならPowerShellまたはVisual Studio Codeのターミナル）で、リポジトリのルートフォルダーに移動します。

### 3. 必要なライブラリをインストールする

```bash
python -m pip install -r requirements.txt
```

Windowsで `python` が使えない場合は、`py -m pip install -r requirements.txt` を試してください。

### 4. Webサーバーを起動する

```bash
python app.py
```

Windowsで `python` が使えない場合は、`py app.py` を試してください。

ターミナルに表示されたローカルアドレス（通常は `http://127.0.0.1:5000`）をChromeやEdgeで開くとゲームを遊べます。終了するときはターミナルで `Ctrl + C` を押します。

## Web版の遊び方

- 難易度を選び「変更」を押します（1〜10、1〜50、1〜100）。
- 数字を入力して「判定する」を押します。
- 正解より小さいと「もっと大きい数字」、大きいと「もっと小さい数字」と表示されます。
- 正解すると挑戦回数が表示されます。
- 「もう一度遊ぶ」で同じ難易度の新しいゲームを始められます。

## テスト

```bash
python -m pip install -r requirements.txt
python -m pytest
```

テストでは、画面表示、不正な入力、範囲外の入力、正解時の回数、難易度変更、ゲームのリセットを確認します。

## 元のC#コンソール版

.NET 8 SDKがある場合、次のコマンドで起動できます。

```bash
dotnet run --project ConsoleGuiApp/ConsoleGuiApp.csproj
```

C#版のテストは次のコマンドで実行できます。

```bash
dotnet test tests/ConsoleGuiApp.Tests/ConsoleGuiApp.Tests.csproj
```
