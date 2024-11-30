# API Tareas

## Descripción

API-Tareas es un proyecto de API en .NET Core que gestiona usuarios y tareas. Demuestra cómo configurar una relación uno a muchos entre usuarios y tareas utilizando Entity Framework Core y SQLite.


## Tabla de Contenidos

- [Instalación](#instalación)
- [Uso](#uso)
- [Desafíos](#desafíos)
- [Consideraciones](#consideraciones)
- [Contribuir](#contribuir)
- [Contacto](#contacto)

![Diagrama de flujo](https://www.figma.com/board/qNwzGacpnyHgVFlRH0Z7dI/flow-app-task?node-id=0-1&t=osDcnbmpx7yVK6c9-1)
 
## Instalación

1. **Clonar el repositorio**:

    ```sh
    git clone https://github.com/gabwebdesign/proxyReverse.git
    cd proyectoCursoDotNet
    ```

2. **Instalar dependencias**:

    ```sh
    dotnet restore
    ```

3. **Actualizar la base de datos**:

    ```sh
    dotnet ef database update
    ```

4. **Ejecutar la aplicación**:

    ```sh
    dotnet run
    ```

## Uso

### Endpoints

- **Obtener todos los usuarios**: `GET /api/users`
- **Obtener un usuario por ID**: `GET /api/users/{id}`
- **Crear un nuevo usuario**: `POST /api/users`
- **Actualizar un usuario**: `PUT /api/users/{id}`
- **Eliminar un usuario**: `DELETE /api/users/{id}`

- **Obtener todos las tareas**: `GET /api/tasks`
- **Obtener un usuario por ID**: `GET /api/tasks/{id}`
- **Crear un nuevo usuario**: `POST /api/tasks`
- **Actualizar un usuario**: `PUT /api/tasks/{id}`
- **Eliminar un usuario**: `DELETE /api/tasks/{id}`

### Ejemplo de Solicitud

**Crear un nuevo usuario**:

```sh
curl -X POST "https://localhost:5001/api/users" -H "Content-Type: application/json" -d '{
  "firstName": "John",
  "lastName": "Doe",
  "email": "john.doe@example.com",
  "password": "password123",
  "isActive": 1,
  "tasks": [
  ]
}'
```
### Desafíos

Inicialmente concebido como un proxy inverso para Hacienda de Costa Rica, este proyecto evolucionó hacia una aplicación de gestión de tareas para facilitar mi aprendizaje. A través de este proyecto, he podido explorar y aplicar conceptos clave como autenticación JWT, manejo de errores y middleware, creación de bases de datos SQLite, establecimiento de relaciones entre entidades, referencias entre proyectos, gestión de contextos y validación de datos.

Dentro de los desafios fue crear 2 projectos simultaneos para la separacion de responsabilidades. Un proyecto maneja la base de datos, controladores, validaciones. El otro se encargara de autenticacion y Manejo Errores.

### Consideraciones

Dentro de las consideraciones a tomar en cuenta en futuras actualizaciones es utilizar la aplicación APIconDB solo para el manejo de base de datos y migrar los controladores y Validaciones al proyecto principal, esto pensando en escabilidad y buenas prácticas.
### Contribuir

Las contribuciones son bienvenidas! Por favor, sigue estos pasos para contribuir:

Haz un fork del repositorio.
Crea una nueva rama (git checkout -b feature/tu-feature).
Realiza tus cambios (git commit -am 'Añadir alguna característica').
Empuja la rama (git push origin feature/tu-feature).
Crea un nuevo Pull Request.

### Contacto

Nombre: Gabriel Aguilar
Email: gab.webdesign@gmail.com
GitHub: gabwebdesign

