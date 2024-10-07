const express = require('express');
const app = express();
const PORT = 3000;

app.get('/api/data', (req, res) => {
  res.json({ message: 'Hello from Express!' });
});

app.post('/api/login', (req, res) => {
  console.log(req.body);
  res.redirect('http://localhost:4200/home');
});

app.listen(PORT, () => {
  console.log(`Server is running on port ${PORT}`);
});


