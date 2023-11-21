const express = require('express')
const cors = require("cors")

const app = express()
const port = 3000

app.use(cors());
app.use(express.json());

let data = [
    {Id:0, Title: "title", Description: "desc1", Author: "name1"},
    {Id:1, Title: "title", Description: "desc2", Author: "author"},
    {Id:2, Title: "title", Description: "desc3", Author: "author"},
];
let user_id_gen = 3;

app.get('/', (req, res) => {
  if(req.query.name != undefined) {
    res.json(data.find(e => e.Author == req.query.name));
    return;
  }
  console.log(req.query.name);
    res.json(data);
})

app.post('/', (req, res) => {
    data.push(req.body);
    data[data.length-1].Id = user_id_gen++;
    res.json(data[data.length - 1]);
})

app.put('/', (req, res) => {
    const idx = data.findIndex(e => e.Id == req.query.id);
    if(idx == -1) { return res.json(req.body); }
    data[idx] = req.body;
    res.json(data[idx]);
})

app.delete("/", (req, res) => {
    console.log("delete: ", req.query, req.body); 
    const user = data.find(e => e.Id == parseInt(req.query.id));
    data = data.filter(e => e.Id != req.query.id);
    res.json(user)
});

app.listen(port, () => {
  console.log(`Example app listening on port ${port}`)
})