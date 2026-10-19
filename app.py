from flask import Flask, redirect, render_template, request, session, url_for
import random

app = Flask(__name__)
app.config["SECRET_KEY"] = "local-number-game-key"


def start_new_game(max_value=50):
    session["max_value"] = max_value
    session["target"] = random.randint(1, max_value)
    session["attempts"] = 0
    session["message"] = f"1〜{max_value}の数字を当ててください。"
    session["message_type"] = "info"


@app.route("/", methods=["GET"])
def index():
    if "target" not in session:
        start_new_game()
    return render_template(
        "index.html",
        max_value=session.get("max_value", 50),
        attempts=session.get("attempts", 0),
        message=session.get("message", ""),
        message_type=session.get("message_type", "info"),
        finished=session.get("finished", False),
    )


@app.route("/guess", methods=["POST"])
def guess():
    if "target" not in session:
        start_new_game()

    raw_guess = request.form.get("guess", "").strip()
    max_value = session.get("max_value", 50)

    try:
        number = int(raw_guess)
    except (TypeError, ValueError):
        session["message"] = "整数を入力してください。"
        session["message_type"] = "error"
        return redirect(url_for("index"))

    if not 1 <= number <= max_value:
        session["message"] = f"1〜{max_value}の範囲で入力してください。"
        session["message_type"] = "error"
        return redirect(url_for("index"))

    if session.get("finished", False):
        session["message"] = "ゲーム終了です。「もう一度遊ぶ」を押してください。"
        session["message_type"] = "error"
        return redirect(url_for("index"))

    session["attempts"] = session.get("attempts", 0) + 1
    target = session["target"]

    if number < target:
        session["message"] = "もっと大きい数字です！"
        session["message_type"] = "info"
    elif number > target:
        session["message"] = "もっと小さい数字です！"
        session["message_type"] = "info"
    else:
        session["message"] = f"正解！{session['attempts']}回で当たりました。"
        session["message_type"] = "success"
        session["finished"] = True

    return redirect(url_for("index"))


@app.route("/settings", methods=["POST"])
def settings():
    difficulty = request.form.get("difficulty", "50")
    values = {"10": 10, "50": 50, "100": 100}
    if difficulty not in values:
        if "target" not in session:
            start_new_game()
        session["message"] = "難易度を選択してください。"
        session["message_type"] = "error"
        return redirect(url_for("index"))

    start_new_game(values[difficulty])
    session.pop("finished", None)
    session["message"] = f"難易度を1〜{values[difficulty]}に変更しました。"
    session["message_type"] = "success"
    return redirect(url_for("index"))


@app.route("/reset", methods=["POST"])
def reset():
    max_value = session.get("max_value", 50)
    start_new_game(max_value)
    session.pop("finished", None)
    session["message"] = "新しいゲームを始めます！"
    session["message_type"] = "success"
    return redirect(url_for("index"))


if __name__ == "__main__":
    app.run(debug=True)
