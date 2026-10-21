# Gestor de Catálogo en Consola

Solución de referencia para una actividad del bootcamp de Devlights, orientada a acompañar a desarrolladores juniors en sus primeros pasos con C#, Programación Orientada a Objetos (POO), interfaces, colecciones y LINQ.

La aplicación representa el catálogo de una tienda: carga cuatro productos de ejemplo, muestra el listado completo, busca un producto por ID y filtra por categoría. Todos los nombres del código están en inglés; la documentación está en español.

## Objetivos de la actividad

- Modelar un producto mediante una clase con propiedades automáticas.
- Definir las operaciones del catálogo mediante una interfaz.
- Implementar un servicio con una `List<Product>` privada en memoria.
- Consultar los productos con `FirstOrDefault`, `Where` y `ToList`.
- Separar la presentación, las operaciones del catálogo y los datos de ejemplo.
- Instanciar el servicio desde `Program.cs` y demostrar su funcionamiento en consola.

## Tecnologías y requisitos

- C# y .NET Framework 4.7.2.
- Windows para compilar y ejecutar esta versión del proyecto.
- Visual Studio con las herramientas de desarrollo de escritorio de .NET y el Developer Pack de .NET Framework 4.7.2, o MSBuild con las herramientas equivalentes.
- Git para clonar el repositorio.

El proyecto utiliza bibliotecas incluidas en .NET Framework y no requiere paquetes NuGet adicionales ni una base de datos.

## Cómo ejecutar

### Desde Visual Studio

1. Clonar el repositorio:

   ```powershell
   git clone https://github.com/MarcosArielFontenla/devlights-catalog-manager.git
   cd devlights-catalog-manager
   ```

2. Abrir `CatalogManager.slnx`. Si tu versión de Visual Studio no admite ese formato de solución, abrir directamente `CatalogManager/CatalogManager.csproj`.
3. Compilar el proyecto y ejecutar con **Ctrl + F5** para mantener visible la consola al finalizar.

### Desde la terminal

En una **Developer PowerShell for Visual Studio**, ubicada en la raíz del repositorio:

```powershell
msbuild .\CatalogManager\CatalogManager.csproj /t:Build /p:Configuration=Debug
.\CatalogManager\bin\Debug\CatalogManager.exe
```

La demostración se ejecuta automáticamente y termina al imprimir los resultados. No necesita ingresar opciones ni datos por teclado.

## Estructura del proyecto

```text
devlights-catalog-manager/
├── .gitignore
├── README.md
├── CatalogManager.slnx
└── CatalogManager/
    ├── App.config
    ├── CatalogManager.csproj
    ├── Program.cs
    ├── Helpers/
    │   └── ProductHelper.cs
    ├── Interfaces/
    │   └── IProductService.cs
    ├── Models/
    │   └── Product.cs
    ├── Presentation/
    │   └── CatalogConsoleApplication.cs
    ├── Properties/
    │   └── AssemblyInfo.cs
    └── Services/
        └── ProductService.cs
```

### Responsabilidades

| Componente | Responsabilidad |
| --- | --- |
| `Models/Product.cs` | Representa los datos de un producto. |
| `Interfaces/IProductService.cs` | Define el contrato para agregar y consultar productos. |
| `Services/ProductService.cs` | Implementa el contrato, almacena los productos y realiza las consultas con LINQ. |
| `Helpers/ProductHelper.cs` | Crea los productos de ejemplo para reutilizarlos en distintas demostraciones. |
| `Presentation/CatalogConsoleApplication.cs` | Ejecuta las consultas de la demostración y presenta los resultados por consola. |
| `Program.cs` | Crea los objetos, carga los productos y pone en marcha la aplicación. |

### Arquitectura y desacoplamiento

La solución es un **monolito organizado por responsabilidades**: se compila como un único ejecutable y sus componentes se ejecutan en el mismo proceso.

La presentación recibe un `IProductService` por constructor. Así, depende del contrato y puede utilizar otra implementación del servicio sin cambiar las llamadas que realiza. `ProductService` se ocupa del catálogo y no escribe en consola.

`Program.cs` conecta los componentes mediante inyección de dependencias manual:

```csharp
var productService = new ProductService();

foreach (var product in ProductHelper.CreateSampleProducts())
{
    productService.Add(product);
}

var application = new CatalogConsoleApplication(productService);
application.Run();
```

Las carpetas organizan el código; el contrato y la dependencia por constructor aportan el desacoplamiento. En este ejercicio, el servicio conserva la lista privada en memoria según la consigna.

## Modelo y contrato

Los nombres de la consigna se traducen al inglés de la siguiente manera:

| Consigna | Implementación | Tipo |
| --- | --- | --- |
| Producto | `Product` | Clase |
| Id | `Id` | `int` |
| Nombre | `Name` | `string` |
| Precio | `Price` | `decimal` |
| Categoria | `Category` | `string` |
| Activo | `IsActive` | `bool` |
| IProductoService | `IProductService` | Interfaz |
| ProductoService | `ProductService` | Clase |

| Método | Comportamiento |
| --- | --- |
| `void Add(Product product)` | Agrega el producto a la lista. Si se recibe `null`, lanza `ArgumentNullException`. |
| `List<Product> GetAll()` | Devuelve una copia de la lista mediante `ToList()`. Incluye productos activos e inactivos. |
| `Product GetById(int id)` | Busca con `FirstOrDefault`. Devuelve el primer producto coincidente o `null` si no existe. |
| `List<Product> FindByCategory(string category)` | Filtra con `Where` y materializa con `ToList`. Ignora diferencias entre mayúsculas y minúsculas. Si no hay coincidencias, devuelve una lista vacía. |

Por ejemplo, tanto `"Electronics"` como `"electronics"` encuentran los mismos productos. La categoría debe coincidir completa; no se realiza una búsqueda parcial ni se eliminan espacios.

`GetAll()` devuelve otra lista, pero contiene referencias a los mismos objetos `Product`: agregar o quitar elementos de esa copia no modifica la colección interna; modificar una propiedad de un producto sí modifica ese objeto compartido.

## Datos de ejemplo

`ProductHelper.CreateSampleProducts()` devuelve una lista nueva con objetos nuevos en cada llamada. Esto permite reutilizar los datos sin compartir sus modificaciones entre demostraciones.

| ID | Nombre | Precio | Categoría | Activo |
| --- | --- | --- | --- | --- |
| 1 | Laptop | 1200.00 | Electronics | Sí |
| 2 | Mouse | 25.50 | Electronics | Sí |
| 3 | Notebook | 4.75 | Stationery | Sí |
| 4 | Desk chair | 150.00 | Furniture | No |

Para cambiar los productos iniciales, editar `Helpers/ProductHelper.cs`. Para modificar el ID o la categoría consultados en la demostración, editar las variables `productId` y `category` en `Presentation/CatalogConsoleApplication.cs`.

## Salida de ejemplo

```text
=== All products ===
ID: 1 | Name: Laptop | Price: 1200.00 | Category: Electronics | Active: True
ID: 2 | Name: Mouse | Price: 25.50 | Category: Electronics | Active: True
ID: 3 | Name: Notebook | Price: 4.75 | Category: Stationery | Active: True
ID: 4 | Name: Desk chair | Price: 150.00 | Category: Furniture | Active: False

=== Product with ID 2 ===
ID: 2 | Name: Mouse | Price: 25.50 | Category: Electronics | Active: True

=== Products in category: Electronics ===
ID: 1 | Name: Laptop | Price: 1200.00 | Category: Electronics | Active: True
ID: 2 | Name: Mouse | Price: 25.50 | Category: Electronics | Active: True
```

El separador decimal puede variar según la configuración regional del equipo.

## Verificación manual

1. Ejecutar el proyecto y comprobar que el listado completo contiene los cuatro productos, incluido el inactivo.
2. Comprobar que la búsqueda por ID `2` devuelve `Mouse`.
3. Comprobar que la categoría `Electronics` devuelve `Laptop` y `Mouse`.
4. Cambiar `productId` por `999` y volver a ejecutar: debe aparecer `Product not found.`.
5. Cambiar `category` por `"Unknown"`: debe aparecer `No products found.`.
6. Cambiar `category` por `"electronics"`: debe devolver los mismos dos productos de esa categoría.

## Alcance del ejemplo

Los datos se guardan únicamente en memoria y se pierden al cerrar la aplicación. Al reiniciarla se cargan otra vez los productos del helper.

El ejercicio cubre el alta y las consultas solicitadas. No incluye edición, eliminación, generación automática de IDs ni validaciones de IDs duplicados o precios negativos. `IsActive` forma parte del modelo y se muestra en pantalla; no excluye productos de las consultas.

Como continuación del aprendizaje, se puede agregar validación de datos, pruebas automatizadas, persistencia o una Web API que reutilice el contrato del servicio.

## Archivos excluidos de Git

El `.gitignore` excluye las carpetas `bin`, `obj` y `.vs`, las preferencias locales del editor, los resultados de pruebas, los paquetes restaurables y archivos habituales de configuración privada. Los archivos fuente y la configuración necesaria para compilar forman parte del repositorio.
