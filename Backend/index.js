const express = require('express')
const bodyParser = require('body-parser');
const cors = require('cors');

const db = require("./db/db.js");

const app = express();
const port = 80;

app.use(cors());

app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());
app.use(express.static(__dirname + "/test/test.html"));


// register via username, password, email
app.post('/register', (req, res) => {
    let { username, password, email } = req.body;

    if (username == "" || username == undefined || password == "" || password == undefined)
        res.status(409).send("Enter valid values");
    if (email != undefined)
        email = email.trim();

    db.register(username.trim(), password.trim(), email,
        (msg) => {
            res.status(201).send(msg);
        },
        (msg) => {
            res.status(409).send(msg);
        }
    );
});


// login via username, password
app.post("/login", (req, res) => {
    let { username, password } = req.body;
    console.log(`Received login request from ${username}`);
    db.login(username, password,
        (msg) => {
            res.status(200).send(msg);
        },
        (msg) => {
            res.status(401).send(msg);
        });
});


// upload score via username, game type and score
app.post("/uploadscore", (req, res) => {
    let { username, gameType, score } = req.body;

    if (username.length == "" || gameType == null || score == null) {
        res.status(500).send("Error with POST data");
        return;
    }

    score = parseFloat(score);

    db.uploadScore(username.trim(), gameType, score,
        (msg) => {
            res.status(200).send(msg);
        },
        (err) => {
            res.status(500).send(err);
        });
});


// get all scores from table, for debugging purposes
app.get("/allscores", (req, res) => {
    db.allScores((scores) => {
        res.status(200).json(scores);
    });
});


// get top 5 for a specified game type
app.post("/leaderboards", (req, res) => {
    let { gameType } = req.body;
    db.getLeaderboards(gameType, (scores) => {
        res.status(200).send(scores);
    });
});


// just to ensure server is live
app.get("/", (req, res) => {
    res.send(`If you can see this, it means port ${port} is open and server is working`);
});


// to access debug page
app.get("/test", (req, res) => {
    res.sendFile(__dirname + "/test/test.html");
});


// starts the server
app.listen(port, () => {
    require('dns').lookup(require('os').hostname(), function (err, addr, fam) {
        console.log(`CZ3002 REST endpoint listening to: ${addr}:${port}`);
    })
});