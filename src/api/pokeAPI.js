const http = require('http');
const fs = require('fs');
const path = require('path');

// leer el archivo
// const filePath = path.join(__dirname, 'pokemons.json');
const filePath = path.join(__dirname, '..', 'json', 'pokemons.json');

const data = fs.readFileSync(filePath, 'utf-8');
const pokemons = JSON.parse(data);

const server = http.createServer((request, response) => {
    response.setHeader("Content-Type", "application/json");

    // limpiar caracteres invisibles
    const cleanUrl = request.url.replace(/\u200B/g, "").trim();
    console.log("URL recibida:", JSON.stringify(cleanUrl)); // depuración

    // endpoint de lista
    if ((cleanUrl === "/pokemons" || cleanUrl === "/pokemons/") && request.method === "GET") {
        response.statusCode = 200;
        response.end(JSON.stringify(pokemons));
        return;
    }

    // endpoint de búsqueda por ID
    if (cleanUrl.startsWith("/pokemons/") && request.method === "GET") {
        const id = parseInt(cleanUrl.split("/")[2]);

        const pokemon = pokemons.find(p => p.id === id);

        if (!pokemon) {
            response.statusCode = 404;
            response.end(JSON.stringify({ error: "Pokemon no encontrado" }));
            return;
        }

        response.statusCode = 200;
        response.end(JSON.stringify(pokemon));
        return;
    }

    response.statusCode = 404;
    response.end(JSON.stringify({ error: "Endpoint no encontrado"}));
});

server.listen(4000, () => {
    console.log("Servidor escuchando en http://localhost:4000/pokemons");
});
