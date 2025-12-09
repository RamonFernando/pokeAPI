# PokeAPI — Proyecto Full-Stack (Node.js + C#)

Este proyecto combina una **API creada con Node.js** y una **aplicación de consola en C#** que consume dicha API.
Permite realizar búsquedas de Pokémon, filtrarlos por nombre, tipo, movimientos, ID,
gestionar favoritos y cargar/guardar datos en JSON.

El proyecto sigue una arquitectura organizada en capas:

- **API (Node.js)**
- **Aplicación C#**
- **Controllers**
- **Services**
- **Models**
- **Views**
- **Helpers**
- **JSON Local**

Para ejecutar el proyecto correctamente es necesario usar **dos consolas de forma simultánea**.

---

## 🚀 Ejecución del proyecto

## 1️⃣ Iniciar la API en Node.js

1. Abre una consola y navega a:

C:\Users\Ramon\Ramon Dropbox\Ramon Perez\PC\Desktop\PokeAPI\pokeAPI\src\api>
2. Ejecuta: node pokeAPI.js
3. Si todo está funcionando, aparecerá:
Servidor escuchando en http://localhost:4000/pokemons
La API ya está disponible y lista para que la aplicación en C# realice peticiones HTTP.

---

## 2️⃣ Ejecutar la aplicación en CSharp

1. Abrir una **segunda consola**.
2. Navegar a la ruta principal del proyecto:
C:\Users\Ramon\Ramon Dropbox\Ramon Perez\PC\Desktop\PokeAPI\pokeAPI>
3. Ejecutar: dotnet run

La aplicación se iniciará, mostrará el menú principal por consola y comenzará a interactuar con tu API en Node.js.

````C#
            Console.WriteLine("**=======================================**");
            Console.WriteLine($"  Bienvenido a la API de Pokemons");
            Console.WriteLine("===========================================");
            Console.WriteLine("         MENU PRINCIPAL");
            Console.WriteLine("===========================================");
            Console.WriteLine("1. Mostrar API");
            Console.WriteLine("2. Buscar (Id)");
            Console.WriteLine("3. Buscar (Nombre y Add a Favoritos)");
            Console.WriteLine("4. Mostrar Tipo");
            Console.WriteLine("5. Mostrar Altura");
            Console.WriteLine("6. Mostrar Peso");
            Console.WriteLine("7. Mostrar Movimientos");
            Console.WriteLine("8. Borrar Pokemon de Lista Favoritos");
            Console.WriteLine("9. Mostrar Lista API");
            Console.WriteLine("10. Actualizar Pokemon (PUT)");
            Console.WriteLine("11. Delete Pokemon (DELETE)");
            Console.WriteLine("0. Salir");
            Console.WriteLine("**=======================================**");
            Console.Write("Introduce una opcion: ");

````

🧩 1. Estructura general del proyecto
pokeAPI/
│
├── Program.cs
├── pokeAPI.csproj
├── pokeAPI.sln
│
├── .vscode/
│   └── settings.json
│
├── src/
│   ├── app/
│   │   └── App.cs
│   │
│   ├── Controllers/
│   │   ├── APIAddFavoriteList.cs
│   │   ├── APIControllers.cs
│   │   ├── APIDeletePokemonDELETE.cs
│   │   ├── APIRemoveFavoriteList.cs
│   │   ├── APIUpdatePokemonPUT.cs
│   │   ├── SearchByHeight.cs
│   │   ├── SearchById.cs
│   │   ├── SearchByMass.cs
│   │   ├── SearchByMoves.cs
│   │   ├── SearchByName.cs
│   │   └── SearchByType.cs
│   │
│   ├── Helpers/
│   │   ├── Helpers.cs
│   │   └── APIValidatorInputs.cs
│   │
│   ├── Models/
│   │   └── Models.cs
│   │
│   ├── Services/
│   │   ├── APILoadJson.cs
│   │   ├── APISaveJson.cs
│   │   ├── HttpClientService.cs
│   │   ├── SearchByHeight.cs
│   │   ├── SearchById.cs
│   │   ├── SearchByMass.cs
│   │   ├── SearchByMoves.cs
│   │   ├── SearchByName.cs
│   │   └── SearchByType.cs
│   │
│   └── Views/
│       ├── GetRequestAPI.cs
│       └── Views.cs
│
└── obj/
