import app as game_app


def setup_client():
    game_app.app.config.update(TESTING=True, SECRET_KEY="test-key")
    return game_app.app.test_client()


def test_home_page_shows_game():
    client = setup_client()
    response = client.get("/")
    assert response.status_code == 200
    assert "数字当てゲーム".encode() in response.data


def test_invalid_text_does_not_increase_attempts():
    client = setup_client()
    with client.session_transaction() as session:
        session["target"] = 25
        session["max_value"] = 50
        session["attempts"] = 0
    response = client.post("/guess", data={"guess": "abc"}, follow_redirects=True)
    assert response.status_code == 200
    assert "整数を入力してください".encode() in response.data
    with client.session_transaction() as session:
        assert session["attempts"] == 0


def test_out_of_range_value_is_rejected():
    client = setup_client()
    with client.session_transaction() as session:
        session["target"] = 25
        session["max_value"] = 50
        session["attempts"] = 0
    response = client.post("/guess", data={"guess": "51"}, follow_redirects=True)
    assert "1〜50の範囲".encode() in response.data
    with client.session_transaction() as session:
        assert session["attempts"] == 0


def test_correct_guess_finishes_game_and_counts_attempt():
    client = setup_client()
    with client.session_transaction() as session:
        session["target"] = 25
        session["max_value"] = 50
        session["attempts"] = 0
    response = client.post("/guess", data={"guess": "25"}, follow_redirects=True)
    assert "正解！1回".encode() in response.data
    with client.session_transaction() as session:
        assert session["attempts"] == 1
        assert session["finished"] is True


def test_difficulty_changes_range_and_resets_attempts():
    client = setup_client()
    with client.session_transaction() as session:
        session["target"] = 25
        session["max_value"] = 50
        session["attempts"] = 3
    response = client.post("/settings", data={"difficulty": "10"}, follow_redirects=True)
    assert "1〜10".encode() in response.data
    with client.session_transaction() as session:
        assert session["max_value"] == 10
        assert session["attempts"] == 0
        assert 1 <= session["target"] <= 10


def test_reset_starts_new_game_and_clears_finished_state():
    client = setup_client()
    with client.session_transaction() as session:
        session["target"] = 25
        session["max_value"] = 50
        session["attempts"] = 1
        session["finished"] = True
    response = client.post("/reset", follow_redirects=True)
    assert response.status_code == 200
    with client.session_transaction() as session:
        assert session["attempts"] == 0
        assert "finished" not in session
        assert 1 <= session["target"] <= 50
