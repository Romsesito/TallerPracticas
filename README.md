# Taller de Buenas Prácticas de Programación - Solución

Este documento describe la solución implementada para el taller de buenas prácticas, abordando los problemas del código inicial mediante el uso de patrones de diseño de software.

## 1. Identificación de Problemas

Se analizaron los siguientes problemas y restricciones en el escenario propuesto:

1.  **Persistencia de Datos:** El `IVehicleRepository` implementado no mantenía el estado de los vehículos entre las diferentes peticiones HTTP. Cada vez que se agregaba un vehículo, la colección se reiniciaba en la siguiente solicitud.

2.  **Dependencia de la Base de Datos:** El desarrollo estaba bloqueado por la ausencia de un esquema de base de datos. Se requería una solución de almacenamiento temporal que permitiera el desarrollo y las pruebas, y que pudiera ser reemplazada fácilmente por una implementación de base de datos real.
3.  **Creación de Objetos Complejos:** La solicitud de negocio de agregar el año actual y más de 20 propiedades futuras al objeto `Vehicle` haría que el constructor de la clase `Car` fuera extremadamente grande y difícil de manejar, violando los principios de código limpio.
4.  **Extensibilidad para Nuevos Modelos:** El sistema necesitaba una forma flexible y escalable de agregar nuevos modelos de vehículos (como el Ford Escape) sin tener que modificar el código existente que consume estos objetos, evitando así múltiples sentencias `if` o `switch` en el controlador.

---

## 2. Metodologías y Patrones de Diseño Seleccionados

Para solucionar los problemas identificados, se seleccionaron e implementaron los siguientes patrones de diseño:

### a. Patrón Singleton 

Para resolver estos problemas, utilicé el patrón Singleton en la clase MemoryCollection. Este patrón asegura que exista una única instancia de la colección de vehículos en toda la aplicación. De esta manera, todos los controladores y repositorios acceden a la misma lista de objetos, simulando una capa de persistencia de datos que mantiene su estado durante el ciclo de vida de la aplicación. Esto desacopla la lógica de negocio de la implementación de la base de datos, permitiendo un fácil reemplazo en el futuro.

### b. Patrón Builder 

Se implemento el patrón Builder con la clase CarModelBuilder. Este patrón es ideal para construir objetos complejos paso a paso. Me permite establecer valores por defecto (como brand="Ford" o year=DateTime.Now.Year) y solo especificar las propiedades que cambian.

### c. Patrón Factory Method 

Se utilizo el patrón Factory Method. Este patrón es como tener diferentes máquinas especializadas que saben cómo construir cada tipo de auto. En lugar de que el programa principal sepa todos los detalles de cómo se hace un Mustang, un Explorer o un Escape, simplemente le pedimos a la factory correspondiente que lo cree. Esto significa que si en el futuro necesito añadir un nuevo modelo de auto, solo tengo que crear una nueva factory con el modelo de ese auto