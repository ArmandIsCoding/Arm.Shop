# Arm.Shop

**Arm.Shop** es una base de comercio electrónico minimalista, modular y de código abierto, desarrollada con .NET 8, Blazor Server y SQL Server. Diseñada para ser clara, extensible y simbólicamente tuya. Sin ruido, solo el código que importa.

---

## ✨ ¿Por qué Arm.Shop?

Muchos frameworks como nopCommerce vienen con capas innecesarias, abstracciones complejas y funcionalidades que no siempre se usan. **Arm.Shop** propone otra cosa:

- 🧱 **Arquitectura minimalista** — fácil de entender, mantener y extender.
- 🧠 **Propiedad simbólica** — creada desde cero para reflejar decisiones de arquitectura minimalista.
- 🧩 **Componentes modulares** — separación clara entre interfaz, lógica y datos.
- 🧰 **Código abierto con atribución** — podés usarlo libremente, solo mencioná al autor (that's me).

---

## 🧱 Estructura del proyecto

```
Arm.Shop/
│
├── Arm.Shop.Web         ← Proyecto Blazor Server (interfaz)
│   ├── Pages/           ← Listado de productos, carrito, checkout
│   ├── Shared/          ← Navbar, footer, layouts
│   ├── Services/        ← CarritoService, ProductoService, OrdenService
│   ├── Models/          ← Producto, ItemCarrito, Orden, Usuario
│   └── Program.cs       ← Configuración de servicios y enrutamiento
│
├── Arm.Shop.Data        ← Acceso a datos con EF Core
│   ├── ArmShopDbContext.cs
│   ├── Repositories/    ← (Opcional) lógica de acceso por entidad
│   └── Migrations/      ← Migraciones de EF Core
│
├── Arm.Shop.Core        ← (Opcional) lógica de negocio y contratos
│   ├── Interfaces/      ← IProductoService, IOrdenService, etc.
│   └── DTOs/            ← Objetos de transferencia
│
└── Arm.Shop.Tests       ← Pruebas unitarias (xUnit o MSTest)
```

---

## 🧪 Tecnologías utilizadas

- **.NET 8** — moderno, rápido y con soporte a largo plazo
- **Blazor Server** — interfaz interactiva con C# en cliente y servidor
- **Entity Framework Core** — ORM para SQL Server
- **SQL Server** — base de datos relacional
- **Inyección de dependencias** — servicios registrados de forma limpia
- **Minimal APIs (opcional)** — para futuras integraciones externas. Quizás nunca la cree, veremos.

---

## 🚀 Cómo empezar

1. **Cloná el repositorio**

   ```bash
   git clone https://github.com/tuusuario/Arm.Shop.git
   cd Arm.Shop
   ```

2. **Configurá la base de datos**

   - Actualizá la cadena de conexión en `appsettings.json`
   - Ejecutá las migraciones:

     ```bash
     dotnet ef database update --project Arm.Shop.Data
     ```

3. **Ejecutá la aplicación**

   ```bash
   dotnet run --project Arm.Shop.Web
   ```

4. **Explorá las rutas**

   - `/productos` — ver productos
   - `/carrito` — gestionar el carrito
   - `/checkout` — realizar una compra

---

## 🛠️ Próximos pasos

- [ ] Panel de administración para productos y órdenes
- [ ] Autenticación y perfiles de usuario
- [ ] Integración con pasarelas de pago (MercadoPago, Stripe)
- [ ] Diseño responsive
- [ ] Versión Blazor WebAssembly (opcional)

---

## 📄 Licencia

Este proyecto está bajo la **Licencia MIT** — libre para usar, modificar y distribuir, siempre que se mencione al autor.  
Ver el archivo [LICENSE](./LICENSE) para más detalles.

---

## ✍️ Autor

Creado por [Armando Andrés Meabe](https://www.linkedin.com/in/armandomeabe/) — AI Architect | Software Architect | Dev Manager | Tech Leader | 18+ years in C++, .NET and GO | Former Microsoft Academic R&D | Cloud Specialist | Researcher & Academic Speaker.

---

## 🧭 Filosofía

Arm.Shop no es solo código — es una **declaración de rebeldía ante la sobre-edificación de las arquitecturas modernas**.  

