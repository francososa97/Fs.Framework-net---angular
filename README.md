# FS.FullStackChallenge 🚀

Plataforma de gestión de productos desarrollada como parte de un challenge técnico fullstack.  
Incluye back-end en .NET 8 + EF Core + AutoMapper + MySQL, y front-end en Angular + NgRx + PrimeNG.

---

## 🧱 Tecnologías utilizadas

### 🔙 Backend (.NET 8)
- ASP.NET Core Web API
- Entity Framework Core (MySQL / InMemory)
- AutoMapper
- FluentValidation
- MediatR (CQRS)
- Onion Architecture
- Autenticación por API Key y JWT
- Docker y Swagger integrados

### 🌐 Frontend (Angular 15)
- Angular CLI
- NgRx (store + effects + reducers)
- PrimeNG + PrimeIcons
- RxJS
- Responsive UI con PrimeFlex

---

## 🔁 Funcionalidades

- 🔐 Autenticación por API Key
- 📦 CRUD completo de productos (nombre, precio, stock)
- ⚡️ Tabla con búsqueda, ordenamiento y filtros
- 🔄 State Management con NgRx
- 💬 Modal de edición con validaciones y toasts (UX)
- 🔍 API documentada con Swagger

---

## 🚀 ¿Cómo levantar el proyecto?

### Backend

```bash
cd Back/src/FS.Framework.Product.Api
dotnet run
```

O usar Docker:

```bash
docker compose up --build
```

Backend disponible en: `https://localhost:7069`  
Swagger: `https://localhost:7069/swagger`

### Frontend

```bash
cd Front/fullstack-challenge
npm install
npm start
```

Frontend disponible en: `http://localhost:4200`

---

## 🔑 API Key

Para acceder a la API, agregar el siguiente header:

```
X-API-KEY: super-secret-key
```

---

## 📷 Capturas de pantalla

*(Agregá aquí imágenes del CRUD funcionando y del diseño limpio)*

---

## 🧠 Consideraciones

- El back está estructurado con CQRS y Unit of Work
- Las respuestas están normalizadas `{ data, message, success }`
- Se puede extender fácilmente para ABM de Producto, login, etc.

---

## 👨‍💻 Autor

Desarrollado por **Franco Damián Sosa**  
[GitHub](https://github.com/francososa97)

---

## 📄 Licencia

MIT