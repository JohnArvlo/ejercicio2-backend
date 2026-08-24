# Backend — API en .NET / C# (Clean Architecture)
 
API construida en ASP.NET Core (.NET) con Clean Architecture (Domain → Application → Infrastructure → API), usando MediatR para CQRS, EF Core para persistencia en SQL Server, y autenticación con JWT.
 
## 1. Resumen
 
Expone endpoints para autenticación (registro/login con JWT) y para el CRUD de proveedores (Suppliers), protegidos con [Authorize]. La lógica de negocio vive en Application a través de Commands/Queries y Handlers de MediatR; Infrastructure implementa el acceso a datos (EF Core) y los servicios técnicos (hashing de contraseñas, generación de JWT).
 
## 2. Dependencias (NuGet)
 
| Paquete | Para qué se usa | Capa donde va |
|---|---|---|
| Microsoft.EntityFrameworkCore | ORM | Infrastructure |
| Microsoft.EntityFrameworkCore.SqlServer | Proveedor de EF Core para SQL Server | Infrastructure |
| MediatR | Patrón CQRS (Commands, Queries, Handlers) | Application (interfaces) e Infrastructure/API (registro) |
| AutoMapper | Mapeo entre entidades y DTOs | Application/Infrastructure |
| Microsoft.AspNetCore.Authentication.JwtBearer | Middleware de autenticación JWT | API |
| System.IdentityModel.Tokens.Jwt | Generación del token JWT | Infrastructure |
| Microsoft.IdentityModel.Tokens | Firma del token (SymmetricSecurityKey) | Infrastructure (transitiva, a veces hay que instalarla explícita) |
| BCrypt.Net-Next | Hashing de contraseñas | Infrastructure |
| Swashbuckle.AspNetCore | Swagger / documentación interactiva | API |
 
Domain no depende de ningún paquete externo.
 
## 3. Estructura del proyecto (Clean Architecture)
 
Domain: entidades puras (User, Supplier), sin dependencias externas.
 
Application: interfaces (IUserRepository, ISupplierRepository, IPasswordHasher, IJwtTokenGenerator), Commands, Queries y Handlers de MediatR. No conoce EF Core, JWT ni BCrypt, solo abstracciones.
 
Infrastructure: implementaciones concretas — AppDbContext, configuraciones de EF Core (SupplierConfiguration, UserConfiguration), Repositories, PasswordHasher, JwtTokenGenerator.
 
API: Controllers (AuthController, SupplierController), Program.cs con la configuración de DI, JWT, Swagger y el pipeline HTTP.
 
## 4. Autenticación
 
- Contraseñas hasheadas con BCrypt antes de guardarse (nunca en texto plano).
- Login exitoso devuelve un JWT firmado con Jwt:Key.
- Los endpoints protegidos usan el atributo Authorize a nivel de controller o de acción; opcionalmente por rol.
- El middleware se registra en Program.cs con UseAuthentication antes de UseAuthorization.
## 5. Endpoints principales
 
Autenticación (AuthController, ruta base api/auth):
 
- POST api/auth/register — crea un usuario nuevo, devuelve su id.
- POST api/auth/login — valida usuario/contraseña, devuelve un token JWT.
Proveedores (SupplierController, ruta base api/suppliers, requiere token):
 
- GET api/suppliers — lista todos los proveedores.
- GET api/suppliers/{id} — obtiene un proveedor por id.
- POST api/suppliers — crea un proveedor.
- PUT api/suppliers/{id} — actualiza un proveedor.
- DELETE api/suppliers/{id} — elimina un proveedor (hard delete).
- PATCH api/suppliers/{id} — soft delete (marca IsDeleted = true).
## 6. Ejecución local
 
Requisitos: tener SQL Server corriendo (local, Docker o remoto) y accesible con la cadena de conexión configurada.
 
1. Completar ConnectionStrings:DefaultConnection y la sección Jwt en appsettings.Development.json.
3. Ejecutar el proyecto API (dotnet run, o desde Visual Studio/Rider).
4. La documentación interactiva de Swagger queda disponible en el entorno de desarrollo, en /swagger.
