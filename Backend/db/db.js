const bcrypt = require("bcrypt");
const Pool = require("pg").Pool;

const ADDITION = '0', SUBTRACTION = '1', MULTIPLICATION = '2', DIVISION = '3';

const saltRounds = 10;

const pool = new Pool({
    user: "postgres",
    host: "localhost",
    database: "api",
    password: "password",
    port: 5432,
});


/**
 * 
 * @param {String}   username 
 * @param {String}   password 
 * @param {String}   email 
 * @param {Function} onSuccess  callback function for a successful registration
 * @param {Function} onError    callback function for an unsuccessful registration
 */
let register = (username, password, email, onSuccess, onError) => {
    bcrypt.genSalt(saltRounds, (err, salt) => {
        bcrypt.hash(password, salt, (err, passwordHash) => {
            pool.query("INSERT INTO users (username, password_hash, salt) VALUES ($1, $2, $3)",
                [username, passwordHash, salt],
                (err, result) => {
                    if (err) {
                        console.error(err);
                        onError(err.message);
                    }

                    else {
                        pool.query("INSERT INTO progress (user_id, progress) \
                                VALUES ((SELECT id FROM users WHERE username=$1), '00000000')",
                            [username], (err, results) => {
                                if (err)
                                    console.error(err);
                                console.log(results)
                            }
                        );

                        onSuccess("User successfully registered!");
                    }
                }
            );
        });
    });
};


/**
 * 
 * @param {String} username 
 * @param {String} password 
 * @param {Function} onSuccess callback function for successful login
 * @param {Function} onError   callback function for unsuccessful login
 */
let login = (username, password, onSuccess, onError) => {
    pool.query("SELECT password_hash, salt FROM users WHERE username=$1", [username], (err, results) => {
        if (err || results.rowCount == 0) {
            onError("Login failed! Either username or password is wrong.");
        }

        else {
            let passwordHash = results.rows[0].password_hash,
                salt = results.rows[0].salt;

            bcrypt.compare(password, passwordHash, (err, result) => {
                if (result == false)
                    onError("Login failed! Either username or password is wrong.");
                else
                    fetchProgress(username, onSuccess);
            });
        }
    });
};


/**
 * Retrieves current game progress of the player upon login
 * 
 * @param {String} username 
 * @param {Function} onSuccess 
 */
let fetchProgress = (username, onSuccess) => {
    pool.query("SELECT progress FROM progress WHERE user_id=(\
            SELECT id FROM users WHERE username=$1)", [username],
        (err, results) => {
            if (err)
                console.error(err);
            else {
                onSuccess(JSON.stringify({
                    progress: results.rows[0].progress
                }));
            }
        }
    );
}


/**
 * upsert score into scores table
 * 
 * @param {String} username 
 * @param {Integer} gameType   0: add 1: sub, 2: mult, 3: div
 * @param {String} score       a string representation of the float value
 * @param {Function} onSuccess callback function for successful upload
 * @param {Function} onError   callback function for unsuccessful upload
 */
let uploadScore = (username, gameType, score, onSuccess, onError) => {
    let prioLoScore = [ADDITION],
        prioHiScore = [SUBTRACTION, MULTIPLICATION, DIVISION];

    gameType = gameType.toString();

    pool.query("INSERT INTO scores (user_id, game_type, score_value) \
                VALUES ((SELECT id FROM users WHERE username=$1), $2, $3)",
        [username, gameType, score], (err, results) => {
            // if score for username and gametype already exists, update the row
            // but only update if new score is better than the old one
            if (err) {
                pool.query("SELECT score_value\
                            FROM users, scores\
                            WHERE id=user_id\
                            AND username=$1\
                            AND game_type=$2",
                    [username, gameType],
                    (err, results) => {
                        oldScore = parseFloat(results.rows[0].score_value);

                        // higher scores means better value
                        if (prioHiScore.includes(gameType) && score > oldScore)
                            updateScore(username, gameType, score, onSuccess, onError);

                        // lower scores mean better value
                        else if (prioLoScore.includes(gameType) && score < oldScore)
                            updateScore(username, gameType, score, onSuccess, onError);

                        else
                            onError("Score not updated, new score is worse than the score in the leaderboard");
                    }
                );
            }
            else if (err && results === undefined)
                onError("Error with score uploading, probably because of the username entry");
            else
                onSuccess("Score uploaded");
        }
    );
}


/**
 * update score in scores table
 * 
 * @param {String} username 
 * @param {Integer} gameType 
 * @param {Integer} score 
 * @param {Function} onSuccess callback function for successful update
 * @param {Function} onError   callback function for successful update
 */
let updateScore = (username, gameType, score, onSuccess, onError) => {
    console.log(`Updating ${username} for type: ${gameType} and score: ${score}`)
    pool.query("UPDATE scores SET score_value=$1\
                WHERE user_id=(SELECT id FROM users WHERE username=$2)\
                AND game_type=$3",
        [score, username, gameType],
        (err, results) => {
            console.log(results);
            if (err) {
                console.error(err);
                onError(err);
            }
            else
                onSuccess("Score uploaded");
        }
    );
}


// just a visual check if the database is working
let allScores = (cb) => {
    pool.query("SELECT * FROM scores", (err, results) => {
        cb(results.rows);
    });
}

/**
 * Returns top scorers of a certain game type
 * 
 * @param {Integer} gameType 0: add 1: sub, 2: mult, 3: div
 * @param {Function} callback callback function for retrieval of top scorers
 */
let getLeaderboards = (gameType, callback) => {
    let order = (gameType == 0) ? "asc" : "desc"
    pool.query(`SELECT username, score_value\
                FROM users, scores \
                WHERE id=user_id\
                AND game_type=$1\
                order by score_value ${order}\
                limit 5`,
        [gameType],
        (err, results) => {
            if (err) {
                console.error(err);
                callback([]);
            } else {
                callback(results.rows);
            }
        });
}


module.exports = {
    register, login, uploadScore, allScores, getLeaderboards
}