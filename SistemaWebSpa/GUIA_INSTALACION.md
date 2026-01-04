# 🚀 GUÍA DE INSTALACIÓN - SISTEMA SPA SIMPLIFICADO

## ✅ REQUISITOS
- .NET 8.0 SDK
- SQL Server (servidor: GOMEZ-JARAMILLO)
- Visual Studio 2022 o VS Code

---

## 📝 PASO 1: CREAR BASE DE DATOS

Ejecuta en SQL Server Management Studio:

```sql
USE Spa
GO

CREATE TABLE Usuarios
(
    UsuarioID INT IDENTITY(1,1) PRIMARY KEY,
    NombreUsuario NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(50) NOT NULL
)
GO

INSERT INTO Usuarios (NombreUsuario, Password) VALUES ('admin', 'admin')
GO
```

**Usuario de prueba: admin / admin**

---

## 📂 PASO 2: COPIAR LOS ARCHIVOS

Copia toda la carpeta `SpaWebMVC_Final` a tu PC.

---

## ⚙️ PASO 3: ABRIR EL PROYECTO

1. Abre Visual Studio 2022
2. File → Open → Project/Solution
3. Selecciona `SpaWebMVC.csproj`

---

## 📦 PASO 4: RESTAURAR PAQUETES

En la consola de Visual Studio o terminal:

```bash
dotnet restore
```

---

## 🔧 PASO 5: VERIFICAR CADENA DE CONEXIÓN

Abre `appsettings.json` y verifica:

```json
{
  "ConnectionStrings": {
    "SpaConnection": "Data Source=GOMEZ-JARAMILLO;Initial Catalog=Spa;Integrated Security=True;TrustServerCertificate=True"
  }
}
```

---

## 🏃 PASO 6: EJECUTAR EL PROYECTO

Presiona **F5** o ejecuta:

```bash
dotnet run
```

---

## 🌐 PASO 7: PROBAR EL SISTEMA

1. Abre el navegador en: `https://localhost:5001`
2. Inicia sesión con: **admin** / **admin**
3. ¡Listo! Ya puedes usar el sistema

---

## 📋 ESTRUCTURA DEL PROYECTO

```
SpaWebMVC_Final/
├── Data/
│   └── ApplicationDbContext.cs        ← DbContext con EF Core
├── Models/
│   ├── Usuario.cs                     ← Usuario (simple)
│   ├── Cliente.cs                     ← Copia tus modelos aquí
│   ├── Empleado.cs
│   └── ...
├── Controllers/
│   ├── AccountController.cs           ← Login/Register
│   ├── HomeController.cs              ← Dashboard
│   └── ClientesController.cs          ← CRUD de Clientes
├── Views/
│   ├── Account/
│   │   ├── Login.cshtml
│   │   └── Register.cshtml
│   ├── Home/
│   │   └── Index.cshtml
│   ├── Clientes/
│   │   └── Index.cshtml
│   └── Shared/
│       └── _Layout.cshtml
├── Program.cs                         ← Configuración principal
├── appsettings.json                   ← Connection String
└── SpaWebMVC.csproj                   ← Paquetes NuGet
```

---

## 🎯 PRÓXIMOS PASOS

### 1. Copiar tus Modelos existentes
Copia todos tus modelos (Cliente, Empleado, Servicio, etc.) a la carpeta `Models/`

### 2. Crear más Controladores
Copia el patrón de `ClientesController.cs` para crear:
- EmpleadosController
- ServiciosController
- ProductosController
- etc.

Solo cambia:
- El nombre del controlador
- `_context.Clientes` por `_context.Empleados` (o la entidad correspondiente)
- Los Bind attributes según los campos del modelo

### 3. Crear más Vistas
Copia el patrón de `Views/Clientes/Index.cshtml` para otras entidades.

---

## 🔑 CARACTERÍSTICAS DEL SISTEMA

✅ **Login/Registro simple** (sin hash, solo para desarrollo)
✅ **Entity Framework Core** (Code-First)
✅ **Autenticación con Cookies**
✅ **Patrón CRUD completo**
✅ **Bootstrap 5** para diseño
✅ **Mensajes con TempData**
✅ **Autorización en controladores**

---

## 🛠️ COMANDOS ÚTILES

### Compilar:
```bash
dotnet build
```

### Ejecutar:
```bash
dotnet run
```

### Limpiar:
```bash
dotnet clean
```

---

## ❓ SOLUCIÓN DE PROBLEMAS

### Error: "A connection was not established"
- Verifica que SQL Server esté corriendo
- Verifica el nombre del servidor en appsettings.json

### Error: "Table 'Usuarios' doesn't exist"
- Ejecuta el script SQL de creación de tabla

### Error: "Package not found"
- Ejecuta `dotnet restore`

---

## 📞 NOTAS IMPORTANTES

⚠️ **SIN ENCRIPTACIÓN**: Las contraseñas NO están encriptadas (solo para desarrollo)
⚠️ **TABLA SIMPLE**: Solo Usuario y Password en tabla Usuarios
⚠️ **ENTITY FRAMEWORK**: Usa EF Core, NO procedimientos almacenados

---

¡SISTEMA LISTO! 🎉
Usuario de prueba: **admin** / **admin**
